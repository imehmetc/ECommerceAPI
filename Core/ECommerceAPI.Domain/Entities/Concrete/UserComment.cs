using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities.Concrete
{
    public class UserComment : BaseEntity
    {
        public string AppUserId { get; set; }
        public Guid ProductId { get; set; }
        public string Content { get; set; }
        public int Rate { get; set; }

        // Relational Properties
        public AppUser AppUser { get; set; }
        public Product Product { get; set; }
        public ICollection<SellerReply> SellerReplies { get; set; }

    }
}
