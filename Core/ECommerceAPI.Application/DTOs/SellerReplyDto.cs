using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs
{
    public class SellerReplyDto : BaseDto
    {
        public string AppUserId { get; set; }
        public string ProductId { get; set; }
        public string ReplyText { get; set; }
        public string SellerId { get; set; }
        public string SellerName { get; set; }
        public string CommentId { get; set; }
    }
}
