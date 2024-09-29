using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.Abstractions.Comment;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Concrete;
using ECommerceAPI.Domain.Entities.Identity;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Concretes
{
    public class AddressService:IAddressService
    {
        private readonly IRepository<Address> _addressRepository;
        private readonly UserManager<AppUser> _userManager;
        public AddressService(IRepository<Address> addressRepository, UserManager<AppUser> userManager) 
        {
            _addressRepository = addressRepository;
            _userManager = userManager;
           
        }
        public async Task<bool> AddAddressAsync(AddressDto addressDto, ClaimsPrincipal userPrincipal)
        {
            var user = await _userManager.GetUserAsync(userPrincipal);
            addressDto.AppUserId = user.Id;
                         
            var mappedAddress = addressDto.Adapt<Address>();
            mappedAddress.AppUser = user;
            
            
            var addedAddress = await _addressRepository.AddAsync(mappedAddress);
            return true;
        }
    }
}
