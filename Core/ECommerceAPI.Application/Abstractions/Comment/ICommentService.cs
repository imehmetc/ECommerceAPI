using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstractions.Comment
{
    public interface ICommentService
    {
        Task<List<UserCommentDto>> GetAllUserCommentsAsync();
        Task<List<UserCommentDto>> GetCommentsByProductIdAsync(string productId);
        Task<List<UserCommentDto>> GetCommentsBySellerIdAsync(string sellerId);
        Task<List<UserCommentDto>> GetCommentsByMemberIdAsync(string memberId);
        Task<bool> AddCommentAsync(UserCommentDto userCommentDto);
        Task DeleteCommentAsync(string id);

    }
}
