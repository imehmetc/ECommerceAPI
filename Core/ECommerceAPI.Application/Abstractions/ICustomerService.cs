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
    public interface ICustomerService
    {
        Task<CustomerDto> GetProfileAsync(ClaimsPrincipal userPrincipal);
        Task<IdentityResult> UpdateProfileAsync(CustomerDto customerDto, ClaimsPrincipal userPrincipal);
        Task<ICollection<AddressDto>> GetCustomerAddresses(ClaimsPrincipal userPrincipal);
        Task<ICollection<OrderDto>> GetCustomerOrders(ClaimsPrincipal userPrincipal);

    }
}
