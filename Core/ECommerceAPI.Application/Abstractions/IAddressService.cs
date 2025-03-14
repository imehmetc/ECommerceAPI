﻿using ECommerceAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstractions
{
    public interface IAddressService
    {
        
        Task<bool> AddAddressAsync(AddressDto addressDto, ClaimsPrincipal userPrincipal);
    }
}
