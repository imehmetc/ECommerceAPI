using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities.Concrete
{
    public class SellerReply : BaseEntity
    {
        public string AppUserId { get; set; }
        public string SellerId { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserCommentId { get; set; }
        public string ReplyText { get; set; }

        public AppUser AppUser { get; set; }
        public AppUser Seller { get; set; }
        public Product Product { get; set; }
        public UserComment UserComment { get; set; }
    }
}
