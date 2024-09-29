using ECommerceAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstractions
{
    public interface ICartService
    {
        Task<CartDto> GetActiveCartAsync(ClaimsPrincipal userPrincipal);
        Task<CartDto> UpdateCartAsync(CartItemDto cartItemDto);
        Task<CartDto> RemoveCartItemAsync(CartItemDto cartItemDto);
    }
}
