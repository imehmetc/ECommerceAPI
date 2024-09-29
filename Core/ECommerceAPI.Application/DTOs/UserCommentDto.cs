using ECommerceAPI.Domain.Entities.Concrete;
using ECommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs
{
    public class UserCommentDto : BaseDto
    {
        public string AppUserId { get; set; }
        public string ProductId { get; set; }
        public string OrderId { get; set; }
        public string Content { get; set; }
        public int Rate { get; set; }
        public string UserName { get; set; }

    }
}
