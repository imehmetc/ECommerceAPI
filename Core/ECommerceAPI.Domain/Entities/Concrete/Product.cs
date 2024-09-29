using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities.Concrete
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int StockQuantity { get; set; }
        public string? Photo { get; set; }
        public Guid CategoryId { get; set; }
        public string SellerId { get; set; }


        // Navigation properties
        public Category Category { get; set; }

        public AppUser Seller { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<UserComment> UserComments { get; set; }
        public ICollection<SellerReply> SellerReplies { get; set; }
    }
}
