using ECommerceAPI.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs
{
    public class AdminReportDto
    {
        public int TotalSellerCount { get; set; }
        public int TotalUserCount { get; set; }
        public int TotalOrderCount { get; set; }
        public string MostOrderedUser { get; set; }
        public int MostOrderedUserOrderCount { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
