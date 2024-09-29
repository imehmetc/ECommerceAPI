using ECommerceAPI.Application.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstractions
{
    public interface ISellerService
    {
        Task<SellerDto> GetProfileAsync(ClaimsPrincipal userPrincipal);
        Task<IdentityResult> UpdateProfileAsync(SellerDto sellerDto, ClaimsPrincipal userPrincipal);
        Task<List<ProductDto>> GetSellerProducts(string sellerId);
        Task<List<OrderDto>> GetOrdersAsync(ClaimsPrincipal userPrincipal);
        Task<List<OrderedCartItemDto>> GetCartItemsAsync(ClaimsPrincipal userPrincipal);

        Task<bool> AddSellerReplyAsync(SellerReplyDto sellerReplyDto);
        Task<List<SellerReplyDto>> GetSellerRepliesByIdAsync(string sellerId);
        Task<SellerReportDto> ReportsAsync(ClaimsPrincipal claimsPrincipal);
        Task<List<ProductStockDto>> GetStockLevelsAsync(ClaimsPrincipal claimsPrincipal);
        Task<List<SellerForBrandDto>> GetAllSellers();
    }
}
