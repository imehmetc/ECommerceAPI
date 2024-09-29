using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstractions
{
    public interface IAdminService
    {
        Task<List<PendingSellerDto>> GetPendingSellersAsync();
        Task<List<PendingSellerDto>> GetApprovedSellersAsync();
        Task<IdentityResult> ApproveSellerAsync(string sellerId);
        Task<IdentityResult> UnapproveSellerAsync(string sellerId);
        Task<IdentityResult> RejectSellerAsync(string sellerId);
        Task<IdentityResult> RemoveSellerAsync(string sellerId);
        Task<AdminDto> GetProfileAsync(ClaimsPrincipal userPrincipal);
        Task<IdentityResult> UpdateProfileAsync(AdminDto adminDto, ClaimsPrincipal userPrincipal);

        Task<List<HelpCenterDto>> GetHelpCentersAsync();
        
        Task<bool> AddHelpCenterAsync(HelpCenterDto helpCenterDto);
        Task<HelpCenterDto> GetHelpCenterAsync(string helpCenterId);
        Task<bool> UpdateHelpCenterAsync(string helpCenterId,HelpCenterDto updatedHelpCenterDto);
        Task<bool> RemoveHelpCenterAsync(string helpCenterId, HelpCenterDto helpCenterDto);
        Task<List<AboutUsDto>> GetAboutUsAsync();
        Task<bool> UpdateAboutUsAsync(string aboutUsId, AboutUsDto updatedAboutUsDto);
        Task<List<ContactDto>> GetContactAsync();
        Task<bool> UpdateContactAsync(string contactId, ContactDto updatedContactDto);
        
        // Admin Raporları
        Task<List<MonthlyUserRegistrationDto>> MonthlyCountOfUsersAsync();
        Task<List<CategoryProductCountDto>> GetCategoryProductCountsAsync();
        Task<List<ProductStockDto>> GetStockLevelsAsync();
        Task<AdminReportDto> ReportsAsync(ClaimsPrincipal claimsPrincipal);
    }
}
