using ECommerceAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstractions
{
    public interface ICartItemService
    {
        Task<bool> AddProductToCart(CartItemDto cartItemDto, ClaimsPrincipal userPrincipal); 
        //Task<CartDto> GetCartItems(Guid cartId, ClaimsPrincipal userPrincipal);
        //Task<CartDto> UpdateCartItem(CartItemDto cartItem,ClaimsPrincipal userPrincipal);
        //Task<CartDto> RemoveCartItem(Guid cartItemId, ClaimsPrincipal userPrincipal);
    }
}
