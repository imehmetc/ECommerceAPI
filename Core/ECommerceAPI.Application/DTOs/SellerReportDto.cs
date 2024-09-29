using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs
{
    public class SellerReportDto
    {
        public int TotalProductCount { get; set; }
        public decimal TotalSalesRevenue { get; set; }
        public int TotalOrderCount { get; set; }
        public int SellerProductCommentCount { get; set; }
    }
}
