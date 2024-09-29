using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Concrete;
using ECommerceAPI.Domain.Entities.Identity;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class CartService: ICartService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<CartItem> _cartItemRepository;
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<Product> _productRepository;
        



        public CartService(UserManager<AppUser> userManager, IRepository<Cart> cartRepository, IRepository<CartItem> cartItemRepository, IRepository<Product> productRepository)
        {
            _userManager = userManager;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;

        }

        public async Task<CartDto> GetActiveCartAsync(ClaimsPrincipal userPrincipal)
        {
            var customer = await _userManager.GetUserAsync(userPrincipal);
            
            var cartList = await _cartRepository.GetAllAsync(x => !x.IsDeleted && x.CustomerId ==  customer.Id&& x.IsActive, includes: new List<Expression<Func<Cart, object>>> { c => c.CartItems });

            var cart = cartList.FirstOrDefault();

            var cartDto = cart.Adapt<CartDto>();
            cartDto.CartItems = cart.CartItems.Adapt<List<CartItemDto>>();

            return cartDto;
        }
        public async Task<CartDto> UpdateCartAsync(CartItemDto cartItemDto)
        {

            // Ürünün toplam fiyatını hesapla
            cartItemDto.TotalPrice = cartItemDto.UnitPrice * cartItemDto.Quantity;

            // Sepeti çek (Eager Loading ile CartItems dahil)
            var cartList = await _cartRepository.GetAllAsync(
                x => !x.IsDeleted && x.Id == cartItemDto.CartId,
                includes: new List<Expression<Func<Cart, object>>> { c => c.CartItems }
            );
            var cart = cartList.FirstOrDefault();

            if (cart == null)
            {
                return null; // Sepet yoksa hata veya null döndür
            }

            // Sepet içinde ilgili CartItem'ı bul
            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == cartItemDto.ProductId);

            if (existingItem != null)
            {
                // Mevcut item'ı güncelle
                existingItem.Quantity = cartItemDto.Quantity;
                existingItem.TotalPrice = cartItemDto.TotalPrice;

            }
            else
            {
                // Eğer item yoksa yeni bir CartItem ekle
                var cartItem = cartItemDto.Adapt<CartItem>();
                cart.CartItems.Add(cartItem);

                await _cartItemRepository.AddAsync(cartItem);
            }

            // Sepetin toplam miktar ve fiyatını güncelle
            cart.TotalQuantity = cart.CartItems.Sum(i => i.Quantity);
            cart.TotalPrice = cart.CartItems.Sum(i => i.TotalPrice);

            await _cartRepository.UpdateAsync(cart);

            // DTO'ya çevir ve döndür
            var cartDto = cart.Adapt<CartDto>();
            cartDto.CartItems = cart.CartItems.Adapt<List<CartItemDto>>();

            return cartDto;
        }
        public async Task<CartDto> RemoveCartItemAsync(CartItemDto cartItemDto)
        {
                        
            
            // Sepeti çek (Eager Loading ile CartItems dahil)
            var cartList = await _cartRepository.GetAllAsync(
                x => !x.IsDeleted && x.Id == cartItemDto.CartId,
                includes: new List<Expression<Func<Cart, object>>> { c => c.CartItems }
            );
            var cart = cartList.FirstOrDefault();

            if (cart == null)
            {
                return null; // Sepet yoksa hata veya null döndür
            }

            // Sepet içinde ilgili CartItem'ı bul
            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == cartItemDto.ProductId);

            if (existingItem != null)
            {

                 await _cartItemRepository.RemoveAsync( existingItem );
  
            }

            // Sepetin toplam miktar ve fiyatını güncelle
            cart.TotalQuantity = cart.CartItems.Sum(i => i.Quantity);
            cart.TotalPrice = cart.CartItems.Sum(i => i.TotalPrice);

            await _cartRepository.UpdateAsync(cart);

            // DTO'ya çevir ve döndür
            var cartDto = cart.Adapt<CartDto>();
            cartDto.CartItems = cart.CartItems.Adapt<List<CartItemDto>>();

            return cartDto;
        }
    }
}
