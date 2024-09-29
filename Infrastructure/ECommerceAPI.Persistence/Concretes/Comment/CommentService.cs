using ECommerceAPI.Application.Abstractions.Comment;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Concrete;
using ECommerceAPI.Domain.Entities.Identity;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Concretes.Comment
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<UserComment> _userCommentRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly UserManager<AppUser> _userManager;
        public CommentService(IRepository<UserComment> userCommentRepository, UserManager<AppUser> userManager, IRepository<Order> orderRepository)
        {
            _userCommentRepository = userCommentRepository;
            _userManager = userManager;
            _orderRepository = orderRepository;
        }
        public async Task<bool> AddCommentAsync(UserCommentDto userCommentDto)
        {
            UserComment comment = userCommentDto.Adapt<UserComment>();
            UserComment entity = await _userCommentRepository.AddAsync(comment);

            var order = await _orderRepository.GetByIdAsync(userCommentDto.OrderId);
            order.IsReviewed = true;
            await _orderRepository.UpdateAsync(order);

            if (entity != null) return true;

            return false;
        }

        public async Task DeleteCommentAsync(string id)
        {
            UserComment userComment = await _userCommentRepository.GetByIdAsync(id);
            if (userComment != null)
            {
                userComment.DeletedDate = DateTime.Now;
                await _userCommentRepository.DeleteAsync(userComment);
            }
        }

        public async Task<List<UserCommentDto>> GetAllUserCommentsAsync()
        {
            IQueryable<UserComment> query = await _userCommentRepository.GetAllAsync(x => !x.IsDeleted);
            List<UserComment> userComments = await query.ToListAsync();

            List<UserCommentDto> userCommentDtos = userComments.Adapt<List<UserCommentDto>>();

            return userCommentDtos;
        }

        public async Task<List<UserCommentDto>> GetCommentsByMemberIdAsync(string memberId)
        {
            var user = await _userManager.FindByIdAsync(memberId);

            if (user == null)
                return new List<UserCommentDto>(); // user bulunamazsa boş liste döndür.

            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Member"))
            {
                var comments = await _userCommentRepository.GetAllAsync(x => x.AppUserId == memberId && !x.IsDeleted);
                return comments.Adapt<List<UserCommentDto>>();
            }

            return new List<UserCommentDto>(); // member değilse boş liste döndür.

        }

        public async Task<List<UserCommentDto>> GetCommentsByProductIdAsync(string productId)
        {
            var includes = new List<Expression<Func<UserComment, object>>>()
            {
                x => x.AppUser,
            };
            IQueryable<UserComment> query = await _userCommentRepository.GetAllAsync(x => x.ProductId == Guid.Parse(productId) && !x.IsDeleted, includes: includes);
            List<UserComment> userComments = await query.ToListAsync();

            // Manuel Mapping
            List<UserCommentDto> userCommentDtos = userComments.Select(uc => new UserCommentDto
            {
                Id = uc.Id,
                AppUserId = uc.AppUserId,
                ProductId = uc.ProductId.ToString(),
                Content = uc.Content,
                Rate = uc.Rate,
                UserName = uc.AppUser?.UserName, // AppUser'dan UserName alınıyor
                CreatedDate = uc.CreatedDate,
                IsDeleted = uc.IsDeleted
                //AppUser = uc.AppUser,
                //Product = uc.Product.Adapt<ProductDto>(), // Eğer ProductDto'yu otomatik haritalıyorsanız
            }).ToList();


            return userCommentDtos;
        }

        public async Task<List<UserCommentDto>> GetCommentsBySellerIdAsync(string sellerId)
        {
            var user = await _userManager.FindByIdAsync(sellerId);

            if (user == null)
                return new List<UserCommentDto>(); // user bulunamazsa boş liste döndür.

            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Seller"))
            {
                var comments = await _userCommentRepository.GetAllAsync(x => x.AppUserId == sellerId); // Seller sildiği ürünleri de görebilsin.
                return comments.Adapt<List<UserCommentDto>>();
            }

            return new List<UserCommentDto>(); // seller değilse boş liste döndür.
        }
    }
}
