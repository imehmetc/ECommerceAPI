using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<string>
    {
		public string FirstName { get; set; }
        public string? SecondName { get; set; }
        public string LastName { get; set; }
        public string? SecondLastName { get; set; }
        public string? Photo { get; set; }
        public string? CompanyName { get; set; }
        public string? ContactInformation { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool? IsApproved { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? DeletedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        // Navigation properties
        public ICollection<Address> Addresses { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Cart>? Carts { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }

        public ICollection<Product>? Products { get; set; }
        public ICollection<Report>? Reports { get; set; }
        public ICollection<UserComment> UserComments { get; set; }
        public ICollection<SellerReply> RepliesAsSeller { get; set; }  // Seller olarak gönderdiği cevaplar
        public ICollection<SellerReply> RepliesAsUser { get; set; }    // Kullanıcı olarak aldığı cevaplar

    }
}
