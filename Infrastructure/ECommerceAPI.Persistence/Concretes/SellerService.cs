using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Concrete;
using ECommerceAPI.Domain.Entities.Identity;
using Mapster;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Concretes
{
    public class SellerService : ISellerService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<SellerReply> _sellerReplyRepository;
        private readonly IRepository<UserComment> _userCommentRepository;
        private readonly IRepository<CartItem> _cartItemRepository;
        private readonly IRepository<Address> _addressRepository;



        public SellerService(UserManager<AppUser> userManager, IRepository<Order> orderRepository, IRepository<Product> productRepository, IRepository<SellerReply> sellerReplyRepository, IRepository<CartItem> cartItemRepository, IRepository<UserComment> userCommentRepository, IRepository<Address> addressRepository)
        {
            _userManager = userManager;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _sellerReplyRepository = sellerReplyRepository;
            _cartItemRepository = cartItemRepository;
            _userCommentRepository = userCommentRepository;
            _addressRepository = addressRepository;
        }
        public async Task<SellerDto> GetProfileAsync(ClaimsPrincipal userPrincipal)
        {
            var seller = await _userManager.GetUserAsync(userPrincipal);
            if (seller == null)
            {
                return null;
            }
            return seller.Adapt<SellerDto>();
        }

        public async Task<IdentityResult> UpdateProfileAsync(SellerDto sellerDto, ClaimsPrincipal userPrincipal)
        {
            if (sellerDto == null)
            {
                throw new ArgumentException("Invalid data", nameof(sellerDto));
            }

            var seller = await _userManager.GetUserAsync(userPrincipal);
            if (seller == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Seller bulunamadı." });
            }
            sellerDto.Adapt(seller);
            var result = await _userManager.UpdateAsync(seller);

            return result;
        }
        public async Task<List<OrderDto>> GetOrdersAsync(ClaimsPrincipal userPrincipal)
        {
            var seller = await _userManager.GetUserAsync(userPrincipal);


            var sellerProducts = await _productRepository.GetAllAsync(p => !p.IsDeleted && p.Seller == seller);
            var productIds = sellerProducts.Select(p => p.Id).ToList();

            // Get all orders where the cart items have products belonging to the seller and IsOrdered is true
            var orders = await _orderRepository.GetAllAsync(order =>
                !order.IsDeleted &&
                order.Cart.CartItems.Any(ci => productIds.Contains(ci.ProductId) && ci.IsOrdered));

            // Map the result to List<OrderDto> and return
            return orders.Adapt<List<OrderDto>>();
        }
        public async Task<List<OrderedCartItemDto>> GetCartItemsAsync(ClaimsPrincipal userPrincipal)
        {
            // Get the current seller based on the ClaimsPrincipal
            var seller = await _userManager.GetUserAsync(userPrincipal);

            // Fetch the seller's products (ensure this method is async)
            var sellerProducts = await _productRepository.GetAllAsync(p => !p.IsDeleted && p.Seller == seller);
            var sellerProductsList = sellerProducts.ToList();
            // Get product IDs from the fetched products
            var productIds = sellerProducts.Select(p => p.Id).ToList();

            // Fetch the ordered cart items related to those products
            var cartItemsOrdered = await _cartItemRepository.GetAllAsync(
                c => !c.IsDeleted && c.IsOrdered && productIds.Contains(c.ProductId));
            var cartItemsOrderedList = cartItemsOrdered.ToList();

            // Fetch the orders (ensure this method is async and handles includes properly)
            var orders = await _orderRepository.GetAllAsync(
                x => !x.IsDeleted);
            var ordersList = orders.ToList();

            var orderedCartItems = new List<OrderedCartItemDto>();
            var addresses = await _addressRepository.GetAllAsync();
            var addressList = addresses.ToList();

            // Loop through each ordered cart item
            foreach (var cartItem in cartItemsOrderedList)
            {
                // Find the matching order for the cart item
                var matchingOrder = ordersList.Where(order => order.CartId == cartItem.CartId).FirstOrDefault();
                
                var address = addressList.Where(a => a.Id == matchingOrder.AddressId).FirstOrDefault();

                // Ensure the matching order and its address are not null
                if (matchingOrder != null && address != null)
                {
                    // Create a new OrderedCartItemDto and populate the fields
                    var orderedCartItemDto = new OrderedCartItemDto
                    {
                        UnitPrice = cartItem.UnitPrice,
                        TotalPrice = cartItem.TotalPrice,
                        OrderDate = matchingOrder.OrderDate,
                        ProductName = cartItem.ProductName,
                        AddressName = $"{address.Street}, {address.City}, {address.Country}, {address.PostalCode}",
                        Quantity = cartItem.Quantity,
                        Discount = cartItem.Discount,
                        CustomerId = cartItem.CustomerId,
                        ShippingCost = matchingOrder.ShippingCost,
                        EstimatedDeliveryDate = matchingOrder.EstimatedDeliveryDate,
                        Notes = matchingOrder.Notes,
                        Status = matchingOrder.Status,
                        TrackingNumber = matchingOrder.TrackingNumber,
                    };

                    // Add the DTO to the result list
                    orderedCartItems.Add(orderedCartItemDto);
                }
            }

            // Return the final list of ordered cart items
            return orderedCartItems;
        }



        public async Task<List<ProductDto>> GetSellerProducts(string sellerId)
        {
            var products = await _productRepository.GetAllAsync(x => !x.IsDeleted && x.SellerId == sellerId);

            var productDtos = products.Adapt<List<ProductDto>>();

            return productDtos;
        }

        public async Task<bool> AddSellerReplyAsync(SellerReplyDto sellerReplyDto)
        {
            if (!Guid.TryParse(sellerReplyDto.SellerId, out var sellerId))
                return false; // SellerId geçersiz

            if (!Guid.TryParse(sellerReplyDto.ProductId, out var productId))
                return false; // ProductId geçersiz

            if (!Guid.TryParse(sellerReplyDto.CommentId, out var commentId))
                return false; // CommentId geçersiz

            var newSellerReply = new SellerReply()
            {
                Id = sellerReplyDto.Id,
                AppUserId = sellerReplyDto.AppUserId,
                SellerId = sellerReplyDto.SellerId,
                UserCommentId = commentId,
                ProductId = productId,
                ReplyText = sellerReplyDto.ReplyText,
                CreatedDate = sellerReplyDto.CreatedDate,
                DeletedDate = sellerReplyDto.DeletedDate,
                ModifiedDate = sellerReplyDto.ModifiedDate,
                IsDeleted = sellerReplyDto.IsDeleted
            };

            var result = await _sellerReplyRepository.AddAsync(newSellerReply);

            return result != null;

        }

        public async Task<List<SellerReplyDto>> GetSellerRepliesByIdAsync(string sellerId)
        {
            var sellerReplies = await _sellerReplyRepository.GetAllAsync(x => !x.IsDeleted && x.SellerId == sellerId);

            if (sellerReplies == null) return new List<SellerReplyDto>();

            var sellerReplyDtos = new List<SellerReplyDto>();

            foreach (var item in sellerReplies)
            {
                var newSellerReply = new SellerReplyDto()
                {
                    Id = item.Id,
                    AppUserId = item.AppUserId,
                    SellerId = item.SellerId,
                    CommentId = item.UserCommentId.ToString(),
                    ProductId = item.ProductId.ToString(),
                    ReplyText = item.ReplyText,
                    CreatedDate = item.CreatedDate,
                    DeletedDate = item.DeletedDate,
                    ModifiedDate = item.ModifiedDate,
                    IsDeleted = item.IsDeleted
                };

                sellerReplyDtos.Add(newSellerReply);
            }

            return sellerReplyDtos;
        }

        public async Task<SellerReportDto> ReportsAsync(ClaimsPrincipal claimsPrincipal)
        {
            var seller = await _userManager.GetUserAsync(claimsPrincipal);
            var roles = await _userManager.GetRolesAsync(seller);

            if (seller != null && roles.Contains("Seller"))
            {
                var sellerProducts = await _productRepository.GetAllAsync(x => x.SellerId == seller.Id && !x.IsDeleted);
                var sellerProductsList = sellerProducts.ToList();

                int sellerProductCount = sellerProductsList.Count(); // Satıcı ürün sayısı.

                var productCartItems = await _cartItemRepository.GetAllAsync(x => !x.IsDeleted);
                var productCartItemsList = productCartItems.ToList();

                List<CartItem> cartItems = new List<CartItem>();

                decimal sellerTotalSalesRevenue = 0; // Satıcının toplam satış geliri.

                foreach (CartItem item in productCartItemsList)
                {
                    if (sellerProductsList.Any(x => x.Id == item.ProductId && x.SellerId == seller.Id))
                    {
                        cartItems.Add(item);
                        sellerTotalSalesRevenue += item.TotalPrice;
                    }
                }

                int sellerTotalOrderCount = cartItems.Count; // Satıcının Toplam satış miktarı.

                var userComments = await _userCommentRepository.GetAllAsync(x => !x.IsDeleted);
                var userCommentsList = userComments.ToList();

                List<UserComment> sellerProductComments = new List<UserComment>();

                foreach (UserComment item in userCommentsList)
                {
                    if (sellerProductsList.Any(x => x.Id == item.ProductId && x.SellerId == seller.Id))
                    {
                        sellerProductComments.Add(item);
                    }
                }

                int sellerProductCommentCount = sellerProductComments.Count(); // Satıcının ürünlerine yapılan yorum sayısı.

                return new SellerReportDto
                {
                    TotalProductCount = sellerProductCount, // Satıcı ürün sayısı.
                    TotalSalesRevenue = sellerTotalSalesRevenue, // Satıcının toplam satış geliri.
                    TotalOrderCount = sellerTotalOrderCount, // Satıcının Toplam satış miktarı.
                    SellerProductCommentCount = sellerProductCommentCount // Satıcının ürünlerine yapılan yorum sayısı.
                };
            }

            return new SellerReportDto(); // Satıcı bulunamazsa veya rolü yoksa boş liste döndür.

        }
        public async Task<List<ProductStockDto>> GetStockLevelsAsync(ClaimsPrincipal claimsPrincipal)
        {
            var seller = await _userManager.GetUserAsync(claimsPrincipal);
            var roles = await _userManager.GetRolesAsync(seller);

            if (seller != null && roles.Contains("Seller"))
            {
                var products = await _productRepository.GetAllAsync(x => !x.IsDeleted && x.SellerId == seller.Id);
                var productList = products.ToList();

                var productStockDtos = productList
                .Select(p => new ProductStockDto
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    StockQuantity = p.StockQuantity,
                    CriticalStockLevel = 50
                });

                return productStockDtos.ToList();
            }
            return new List<ProductStockDto>();
        }

        public async Task<List<SellerForBrandDto>> GetAllSellers()
        {
            var sellers = await _userManager.GetUsersInRoleAsync("Seller");
            var mappedSellers = sellers.Adapt<List<SellerForBrandDto>>();
            return mappedSellers;
        }

        public Task<List<OrderDto>> GetOrders(ClaimsPrincipal userPrincipal)
        {
            throw new NotImplementedException();
        }
    }
}
