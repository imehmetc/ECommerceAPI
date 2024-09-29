using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities.Concrete
{
    public class CartItem:BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal? Discount { get; set; }
        public bool IsInStock { get; set; }
        public string? Photo { get; set; }
        public bool IsOrdered { get; set; } = false;
        
        public Cart Cart { get; set; }
        public Guid CartId { get; set; }
        public AppUser Customer { get; set; }
        public string CustomerId { get; set; }
    }
}
