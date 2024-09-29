using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstractions
{
    public interface IOrderService
    {
        Task<bool> CreateOrderAsync(OrderDto orderDto);
        Task<IEnumerable<OrderDto>> GetPastOrdersByCustomerIdAsync(string customerId); // Tüm geçmiş siparişler 
        Task<OrderDto> GetOrderDetailsByIdAsync(Guid orderId); // Geçmiş sipariş detayları 

        Task<string> GetOrderStatusByIdAsync(Guid orderId); // Siparişin güncel durumu 

        Task<List<OrderReviewDto>> GetUnReviewedOrdersByCustomerIdAsync(string customerId); // Kullanıcının sipariş verdiği ama yorum yapmadığı siparişler.

    }
}
