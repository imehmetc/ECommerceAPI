using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities.Concrete
{
    public class Cart:BaseEntity
    {
        public string CustomerId { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
        public AppUser Customer { get; set; }
        public decimal TotalPrice { get; set; } = 0;
        public int TotalQuantity { get; set; } = 0;
        public bool IsActive { get; set; }
        public Order? Order { get; set; }

    }
}
