using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities.Concrete
{
    public class Report : BaseEntity
    {
        public string ReportType { get; set; } // Satış, Sipariş, Kullanıcı Aktivitesi vb.
        public string ReportContent { get; set; } // Verilerin özetlenmiş hali
        public string SellerId { get; set; }

        // Navigation property
        public AppUser Seller { get; set; }
    }
}
