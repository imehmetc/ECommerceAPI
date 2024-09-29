using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Concrete;
using ECommerceAPI.Domain.Entities.Identity;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ECommerceAPI.Persistence.Concretes
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMailService _mailService;
        private readonly IRepository<HelpCenter> _helpCenterRepository;
        private readonly IRepository<AboutUs> _aboutUsRepository;
        private readonly IRepository<Contact> _contactRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<CartItem> _cartItemRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Product> _productRepository;


        public AdminService(UserManager<AppUser> userManager, 
                            IRepository<HelpCenter> helpCenter, 
                            IRepository<AboutUs> aboutUs, 
                            IRepository<Contact> contact, 
                            IMailService mailService, 
                            IRepository<Category> categoryRepository, 
                            IRepository<Product> productRepository, 
                            IRepository<Order> orderRepository, 
                            IRepository<CartItem> cartItemRepository)
        {
            _userManager = userManager;
            _helpCenterRepository = helpCenter;
            _aboutUsRepository = aboutUs;
            _contactRepository = contact;
            _mailService = mailService;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _cartItemRepository = cartItemRepository;
        }



        public async Task<IdentityResult> ApproveSellerAsync(string sellerId)
        {
            var seller = await _userManager.FindByIdAsync(sellerId);
            if (seller == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Satıcı bulunamadı." });
            }

            seller.IsApproved = true;

            await _userManager.UpdateAsync(seller);

            await _mailService.SendApproveSellerMailAsync(seller);

            return IdentityResult.Success;
        }

        public async Task<List<PendingSellerDto>> GetApprovedSellersAsync()
        {
            try
            {
                var sellers = await _userManager.GetUsersInRoleAsync("seller");
                var approvedSellers = sellers.Where(s => s.IsApproved.HasValue && s.IsApproved.Value).ToList();
                var mappedApprovedSellers = approvedSellers.Adapt<List<PendingSellerDto>>();
                return mappedApprovedSellers;
            }
            catch (Exception ex)
            {
                // Hata mesajını loglayın veya döndürün
                throw new Exception($"Error fetching approved sellers: {ex.Message}");
            }
        }

        public async Task<List<PendingSellerDto>> GetPendingSellersAsync()
        {
            try
            {
                var sellers = await _userManager.GetUsersInRoleAsync("seller");
                var unapprovedSellers = sellers.Where(s => !s.IsApproved.HasValue || !s.IsApproved.Value).ToList();
                var mappedUnapprovedSellers = unapprovedSellers.Adapt<List<PendingSellerDto>>();
                return mappedUnapprovedSellers;
            }
            catch (Exception ex)
            {
                // Hata mesajını loglayın veya döndürün
                throw new Exception($"Error fetching pending sellers: {ex.Message}");
            }
        }

        public async Task<AdminDto> GetProfileAsync(ClaimsPrincipal userPrincipal)
        {
            var admin = await _userManager.GetUserAsync(userPrincipal);
            if (admin == null)
            {
                return null;
            }
            return admin.Adapt<AdminDto>();
        }

        public async Task<IdentityResult> RemoveSellerAsync(string sellerId)
        {
            var deletedSeller = await _userManager.FindByIdAsync(sellerId);
            if (deletedSeller == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Satıcı bulunamadı." });
            }

            await _userManager.DeleteAsync(deletedSeller);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> RejectSellerAsync(string sellerId)
        {
            var rejectedSeller = await _userManager.FindByIdAsync(sellerId);
            if (rejectedSeller == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Satıcı bulunamadı." });
            }

            await _userManager.DeleteAsync(rejectedSeller);

            await _mailService.SendRejectSellerMailAsync(rejectedSeller);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UnapproveSellerAsync(string sellerId)
        {
            var seller = await _userManager.FindByIdAsync(sellerId);
            if (seller == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Satıcı bulunamadı." });
            }

            seller.IsApproved = false;

            await _userManager.UpdateAsync(seller);

            return IdentityResult.Success;
        }

        public async Task<List<HelpCenterDto>> GetHelpCentersAsync()
        {
            var helpCenters = await _helpCenterRepository.GetAllAsync();
            var helpCenterDtos = helpCenters.Adapt<List<HelpCenterDto>>();
            return helpCenterDtos;
        }
        public async Task<bool> AddHelpCenterAsync(HelpCenterDto helpCenterDto)
        {
            var helpCenter = helpCenterDto.Adapt<HelpCenter>();
            await _helpCenterRepository.AddAsync(helpCenter);
            return true;
        }
        public async Task<HelpCenterDto> GetHelpCenterAsync(string helpCenterId)
        {
            var existingHelpCenter = await _helpCenterRepository.GetByIdAsync(helpCenterId);
            if (existingHelpCenter == null)
            {

                throw new Exception($"HelpCenter not found with id: {helpCenterId}");
            }
            return existingHelpCenter.Adapt<HelpCenterDto>();
        }
        public async Task<bool> UpdateHelpCenterAsync(string helpCenterId, HelpCenterDto updatedHelpCenterDto)
        {
            var existingHelpCenter = await _helpCenterRepository.GetByIdAsync(helpCenterId);
            if (existingHelpCenter == null)
            {

                throw new Exception($"HelpCenter not found with id: {helpCenterId}");
            }

            // Mevcut kaydı yeni bilgilerle güncelle
            existingHelpCenter.PopularQuestion = updatedHelpCenterDto.PopularQuestion;
            existingHelpCenter.PopularAnswer = updatedHelpCenterDto.PopularAnswer;
            existingHelpCenter.ModifiedDate = DateTime.Now;

            var helpCenter = updatedHelpCenterDto.Adapt<HelpCenter>();
            await _helpCenterRepository.UpdateAsync(helpCenter);
            return true;
        }
        public async Task<bool> RemoveHelpCenterAsync(string helpCenterId, HelpCenterDto helpCenterDto)
        {
            var existingHelpCenter = await _helpCenterRepository.GetByIdAsync(helpCenterId);

            if (existingHelpCenter == null)
            {
                throw new Exception($"HelpCenter not found with id: {helpCenterId}");
            }

            await _helpCenterRepository.RemoveAsync(existingHelpCenter);

            return true;
        }


        public async Task<List<ContactDto>> GetContactAsync()
        {
            var contacts = await _contactRepository.GetAllAsync();
            var contactDtos = contacts.Adapt<List<ContactDto>>();
            return contactDtos;
        }
        public async Task<bool> UpdateContactAsync(string contactId, ContactDto updatedContactDto)
        {
            var existingContact = await _contactRepository.GetByIdAsync(contactId);
            if (existingContact == null)
            {

                throw new Exception($"Cıontact not found with id: {contactId}");
            }

            // Mevcut kaydı yeni bilgilerle güncelle
            existingContact.Title = updatedContactDto.Title;
            existingContact.Phone = updatedContactDto.Phone;
            existingContact.Address = updatedContactDto.Address;
            existingContact.ModifiedDate = DateTime.Now;

            var contact = updatedContactDto.Adapt<Contact>();
            await _contactRepository.UpdateAsync(contact);
            return true;
        }
        public async Task<List<AboutUsDto>> GetAboutUsAsync()
        {
            var aboutUs = await _aboutUsRepository.GetAllAsync();
            var aboutUsDtos = aboutUs.Adapt<List<AboutUsDto>>();
            return aboutUsDtos;
        }
        public async Task<bool> UpdateAboutUsAsync(string aboutUsId, AboutUsDto updatedAboutUsDto)
        {
            var existingAboutUs = await _aboutUsRepository.GetByIdAsync(aboutUsId);
            if (existingAboutUs == null)
            {

                throw new Exception($"AboutUs not found with id: {aboutUsId}");
            }

            // Mevcut kaydı yeni bilgilerle güncelle
            existingAboutUs.BusinessInfo = updatedAboutUsDto.BusinessInfo;
            existingAboutUs.WhatWeDo = updatedAboutUsDto.WhatWeDo;
            updatedAboutUsDto.ModifiedDate = DateTime.Now;
            existingAboutUs.ModifiedDate = updatedAboutUsDto.ModifiedDate;

            var aboutUs = updatedAboutUsDto.Adapt<AboutUs>();
            await _aboutUsRepository.UpdateAsync(aboutUs);
            return true;
        }


        public async Task<IdentityResult> UpdateProfileAsync(AdminDto adminDto, ClaimsPrincipal userPrincipal)
        {

            if (adminDto == null)
            {
                throw new ArgumentException("Invalid data", nameof(adminDto));
            }

            var admin = await _userManager.GetUserAsync(userPrincipal);
            if (admin == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Admin bulunamadı." });
            }


            adminDto.Adapt(admin);

            var result = await _userManager.UpdateAsync(admin);
            return result;
        }



        // Admin Raporları
        public async Task<List<MonthlyUserRegistrationDto>> MonthlyCountOfUsersAsync()
        {
            var monthlyRegistrations = await _userManager.Users.Where(x => x.IsDeleted == false)
        .GroupBy(u => new { Year = u.CreatedDate.Year, Month = u.CreatedDate.Month })
        .Select(g => new MonthlyUserRegistrationDto
        {
            Month = g.Key.Month,
            Year = g.Key.Year,
            Count = g.Count()
        })
        .OrderBy(r => r.Year)
        .ThenBy(r => r.Month)
        .ToListAsync();

            return monthlyRegistrations;
        }

        public async Task<List<CategoryProductCountDto>> GetCategoryProductCountsAsync()
        {
            var include = new List<Expression<Func<Product, object>>> { o => o.Category, };

            var products = await _productRepository.GetAllAsync(x => x.IsDeleted == false, includes: include);

            var categoryProductCountDtos = await products.GroupBy(u => new { CategoryIds = u.CategoryId })
                .Select(c => new CategoryProductCountDto
                {
                    CategoryName = c.First().Category.CategoryName,
                    ProductCount = c.Count()
                })
                .ToListAsync();

            return categoryProductCountDtos;
        }

        public async Task<List<ProductStockDto>> GetStockLevelsAsync()
        {
            var products = await _productRepository.GetAllAsync(x => !x.IsDeleted);

            var productStockDtos = await products
            .Select(p => new ProductStockDto
            {
                ProductId = p.Id,
                ProductName = p.Name,
                StockQuantity = p.StockQuantity,
                CriticalStockLevel = 50
            })
            .ToListAsync();

            return productStockDtos;
        }

        public async Task<AdminReportDto> ReportsAsync(ClaimsPrincipal claimsPrincipal)
        {
            var admin = await _userManager.GetUserAsync(claimsPrincipal);
            var roles = await _userManager.GetRolesAsync(admin);

            if (admin != null && roles.Contains("Admin"))
            {
                var users = _userManager.Users.Where(x => !x.IsDeleted).ToList();
                var userCount = users.Count; // Sitedeki toplam kullanıcı sayısı. (Adminler ve Satıcılar dahil)

                var sellers = _userManager.Users.Where(x => !x.IsDeleted && x.IsApproved == true).ToList();
                int sellerCount = sellers.Count; // Sitedeki toplam satıcı sayısı.

                var include = new List<Expression<Func<Order, object>>> { o => o.Customer };
                var orders = await _orderRepository.GetAllAsync(x => !x.IsDeleted, includes: include);
                var orderList = orders.ToList();
                int orderCount = orderList.Count; // Sitede yapılmış toplam sipariş sayısı

                var mostOrderedUser = orderList.GroupBy(x => new { x.CustomerId, CustomerName = x.Customer.Email })
                    .Select(x => new
                    {
                        CustomerId = x.Key.CustomerId,
                        CustomerName = x.Key.CustomerName,
                        OrderCount = x.Count()
                    }).OrderByDescending(x => x.OrderCount).FirstOrDefault(); // En çok sipariş veren kullanıcının Email'i ve yaptığı sipariş miktarı


                var cartItems = await _cartItemRepository.GetAllAsync(x => !x.IsDeleted);
                var cartItemList = cartItems.ToList();
                decimal totalRevenue = cartItemList.Sum(x => x.TotalPrice); // Toplam satış

                return new AdminReportDto()
                {
                    TotalUserCount = userCount, // Sitedeki toplam kullanıcı sayısı. (Adminler ve Satıcılar dahil)
                    TotalSellerCount = sellerCount, // Sitedeki toplam satıcı sayısı.
                    TotalOrderCount = orderCount, // Sitede yapılmış toplam sipariş sayısı
                    MostOrderedUser = mostOrderedUser.CustomerName, // En çok sipariş veren kullanıcının Email'i
                    MostOrderedUserOrderCount = mostOrderedUser.OrderCount, // En çok sipariş veren kullanıcının yaptığı sipariş miktarı
                    TotalRevenue = totalRevenue // Toplam satış
                };
            }

            return new AdminReportDto(); // Admin bulunamazsa veya rolü yoksa boş liste döndür.
        }
        
        
    }
}
