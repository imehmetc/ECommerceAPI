using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Concrete;
using ECommerceAPI.Domain.Entities.Identity;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Concretes
{
    public class CustomerService : ICustomerService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Product> _productRepository;

        public CustomerService(UserManager<AppUser> userManager, IRepository<Order> orderRepository, IRepository<Product> productRepository)
        {
            _userManager = userManager;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }
        public async Task<CustomerDto> GetProfileAsync(ClaimsPrincipal userPrincipal)
        {
            var customerId = _userManager.GetUserId(userPrincipal);

            var customer = await _userManager.Users
                .Include(u => u.Addresses) // Addresses ilişkili tabloyu dahil et
                .FirstOrDefaultAsync(u => u.Id == customerId);
            if (customer == null)
            {
                return null;
            }
            var customerDto = customer.Adapt<CustomerDto>();
            customerDto.Addresses = customer.Addresses.Adapt<List<AddressDto>>();
            return customerDto;
        }

        public async Task<IdentityResult> UpdateProfileAsync(CustomerDto customerDto, ClaimsPrincipal userPrincipal)
        {
            if (customerDto == null)
            {
                throw new ArgumentException("Invalid data", nameof(customerDto));
            }

            var customerId = _userManager.GetUserId(userPrincipal);

            var customer = await _userManager.Users
                .Include(u => u.Addresses) // Addresses ilişkili tabloyu dahil et
                .FirstOrDefaultAsync(u => u.Id == customerId);

            if (customer == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Customer bulunamadı." });
            }
            customerDto.Adapt(customer);
            customer.Addresses = customerDto.Addresses.Adapt<List<Address>>();

            var result = await _userManager.UpdateAsync(customer);

            return result;
        }
        public async Task<ICollection<AddressDto>> GetCustomerAddresses(ClaimsPrincipal userPrincipal)
        {
            var customerId = _userManager.GetUserId(userPrincipal);

            var customer = await _userManager.Users
                .Include(u => u.Addresses) // Addresses ilişkili tabloyu dahil et
                .FirstOrDefaultAsync(u => u.Id == customerId);


            var customerDto = customer.Adapt<CustomerDto>();
            customerDto.Addresses = customer.Addresses.Adapt<ICollection<AddressDto>>();

            return customerDto.Addresses;
        }
        public async Task<ICollection<OrderDto>> GetCustomerOrders(ClaimsPrincipal userPrincipal)
        {
            var customerId = _userManager.GetUserId(userPrincipal);

            var customer = await _userManager.Users
                .Include(u => u.Orders) // Addresses ilişkili tabloyu dahil et
                .FirstOrDefaultAsync(u => u.Id == customerId);


            var customerDto = customer.Adapt<CustomerDto>();
            customerDto.Orders = customer.Orders.Adapt<ICollection<OrderDto>>();

            return customerDto.Orders;
        }


    }
}
