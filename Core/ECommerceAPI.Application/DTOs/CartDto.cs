using ECommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs
{
    public class CartDto : BaseDto
    {
        public string CustomerId { get; set; }
        public ICollection<CartItemDto>? CartItems { get; set; }
        public decimal TotalPrice { get; set; } = 0;
        public int TotalQuantity { get; set; } = 0;
        public bool IsActive { get; set; }
        //public OrderDto? Order { get; set; }
    }
}
