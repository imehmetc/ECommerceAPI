using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.Abstractions.Comment;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Concrete;
using ECommerceAPI.Domain.Entities.Identity;
using ECommerceAPI.Domain.Enums;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Concretes
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<CartItem> _cartItemRepository;
        private readonly ICommentService _commentService;
        private readonly UserManager<AppUser> _userManager;

        public OrderService(IRepository<Order> orderRepository, ICommentService commentService, UserManager<AppUser> userManager, IRepository<Cart> cartRepository, IRepository<CartItem> cartItemRepository)
        {
            _orderRepository = orderRepository;
            _commentService = commentService;
            _userManager = userManager;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
        }

        public async Task<bool> CreateOrderAsync(OrderDto orderDto)
        {
            var customerId = orderDto.CustomerId;
            var cartList = await _cartRepository.GetAllAsync(c => c.IsActive == true && c.CustomerId == customerId, includes: new List<Expression<Func<Cart, object>>> { c => c.CartItems });
            var cart = cartList.FirstOrDefault();
            if (cart == null)
            {
                return false; // Handle the case where no cart is found
            }

            // Mark all cart items as ordered
            foreach (var item in cart.CartItems)
            {
                item.IsOrdered = true;
            }

            cart.IsActive = false;
            foreach(var item in cart.CartItems)
            {

                item.IsOrdered = true;
               
                await _cartItemRepository.UpdateAsync(item);
            }
            
            
            var order = orderDto.Adapt<Order>();
            order.CartId = cart.Id;
            var addedOrder = await _orderRepository.AddAsync(order);
            return true;
        }

        public async Task<OrderDto> GetOrderDetailsByIdAsync(Guid orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId.ToString());

            var orderDto = order.Adapt<OrderDto>();

            return orderDto;

        }

        public async Task<string> GetOrderStatusByIdAsync(Guid orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId.ToString());
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            return order.Status;
        }

        public async Task<IEnumerable<OrderDto>> GetPastOrdersByCustomerIdAsync(string customerId)
        {
            var orders = await _orderRepository.GetAllAsync(o => o.CustomerId == customerId && o.Status == "Delivered");
            return orders.Adapt<List<OrderDto>>();
        }

        public async Task<List<OrderReviewDto>> GetUnReviewedOrdersByCustomerIdAsync(string customerId)
        {
            var orders = await _orderRepository.GetAllAsync(x => x.CustomerId == customerId && !x.IsDeleted && !x.IsReviewed);

            var carItems = await _cartItemRepository.GetAllAsync(x => x.CustomerId == customerId);

            var cartItems2 = carItems.Where(a => orders.Any(x => x.CartId == a.CartId)).ToList();


            var orderReviews = new List<OrderReviewDto>();

            foreach (var order in orders)
            {
                var matchingCartItems = cartItems2.Where(cartItem => cartItem.CartId == order.CartId).ToList();
                
                foreach (var cartItem in matchingCartItems)
                {
                    OrderReviewDto orderReview = new OrderReviewDto()
                    {
                        OrderId = order.Id.ToString(),
                        ProductId = cartItem.ProductId.ToString(),
                        OrderDate = order.OrderDate,
                        ProductName = cartItem.ProductName,
                    };
                    orderReviews.Add(orderReview);
                }
            }

            return orderReviews;
        }
    }



}
