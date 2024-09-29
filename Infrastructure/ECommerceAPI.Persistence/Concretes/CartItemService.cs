using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Concrete;
using ECommerceAPI.Domain.Entities.Identity;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
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
    public class CartItemService:ICartItemService
    {
        
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<CartItem> _cartItemRepository;
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<Product> _productRepository;

        public CartItemService(UserManager<AppUser> userManager, IRepository<Cart> cartRepository, IRepository<CartItem> cartItemRepository, IRepository<Product> productRepository)
        {
            _userManager = userManager;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
        }

        public async Task<bool> AddProductToCart(CartItemDto cartItemDto, ClaimsPrincipal userPrincipal)
        {
            // Kullanıcı kimliğini al
            var customer = await _userManager.GetUserAsync(userPrincipal);
            if (customer == null)
            {
                return false;
            }
       
            

            // Kullanıcının sepetini al
            var cartList = await _cartRepository.GetAllAsync(c => c.CustomerId == customer.Id&& c.IsActive== true, includes: new List<Expression<Func<Cart, object>>> { c => c.CartItems });
            var cart = cartList.FirstOrDefault();


            // Eğer sepet yoksa yeni bir sepet oluştur
            if (cart == null)
            {
                cart = new Cart
                {
                    Customer =customer,
                    CustomerId = customer.Id,
                    IsActive = true,
                    Id = Guid.NewGuid(),
                    CartItems = new Collection<CartItem>(),
                };
                await _cartRepository.AddAsync(cart);
                cart.Adapt<CartDto>();

            }
            
            // Sepet içinde ürünü güncelle ya da ekle
            var existingItemList = await _cartItemRepository.GetAllAsync(ci => ci.ProductId == cartItemDto.ProductId && ci.IsOrdered == false&&ci.CustomerId==customer.Id);
            var existingItem = existingItemList.FirstOrDefault();
           


            if (existingItem != null)
            {
               
                existingItem.Quantity += cartItemDto.Quantity;
                existingItem.TotalPrice += cartItemDto.TotalPrice;
                await _cartItemRepository.UpdateAsync(existingItem);
                
            }
            else
            {
                
                
                var cartItem = cartItemDto.Adapt<CartItem>();
                cartItem.Customer = customer;
                cartItem.CartId = cart.Id;
                cart.CartItems.Add(cartItem);
                
                await _cartItemRepository.AddAsync(cartItem);
            }

            
            cart.TotalQuantity += cartItemDto.Quantity;
            cart.TotalPrice += cartItemDto.TotalPrice;
            
            cart.Adapt<CartDto>();

            await _cartRepository.UpdateAsync(cart); // Sepeti güncelle
            return true;

        }

        

        //public async Task<CartDto> UpdateCartItem(CartItemDto cartItemDto, ClaimsPrincipal userPrincipal)
        //{
        //    var userId = userPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    var cart = await _cartRepository.GetAll()
        //        .FirstOrDefaultAsync(c => c.UserId == userId);

        //    var existingItem = await _cartItemRepository.GetAll()
        //        .FirstOrDefaultAsync(ci => ci.ProductId == cartItemDto.ProductId && ci.CartId == cart.CartId);

        //    if (existingItem != null)
        //    {
        //        existingItem.Quantity = cartItemDto.Quantity;
        //        existingItem.TotalPrice = cartItemDto.TotalPrice;
        //        await _cartItemRepository.UpdateAsync(existingItem);
        //    }

        //    return await GetCartItems(cart.CartId, userPrincipal); // Güncellenmiş sepeti döndür
        //}

        //public async Task<CartDto> RemoveCartItem(Guid cartItemId, ClaimsPrincipal userPrincipal)
        //{
        //    var userId = userPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    var cart = await _cartRepository.GetAll()
        //        .FirstOrDefaultAsync(c => c.UserId == userId);

        //    var itemToRemove = await _cartItemRepository.GetAll()
        //        .FirstOrDefaultAsync(ci => ci.ProductId == cartItemId && ci.CartId == cart.CartId);

        //    if (itemToRemove != null)
        //    {
        //        await _cartItemRepository.RemoveAsync(itemToRemove);
        //        // Sepetin toplam fiyat ve miktarını güncelle
        //        cart.TotalQuantity -= itemToRemove.Quantity;
        //        cart.TotalPrice -= itemToRemove.TotalPrice;

        //        await _cartRepository.UpdateAsync(cart); // Sepeti güncelle
        //    }

        //    return await GetCartItems(cart.CartId, userPrincipal); // Güncellenmiş sepeti döndür
        //}
    }
}
