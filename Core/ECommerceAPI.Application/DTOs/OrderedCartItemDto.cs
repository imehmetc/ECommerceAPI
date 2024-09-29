using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs
{
    public class OrderedCartItemDto : BaseDto
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal? Discount { get; set; }
        public string CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }         // Sipariş tarihi
        public decimal? ShippingCost { get; set; }       // Kargo ücreti
        public DateTime? EstimatedDeliveryDate { get; set; }  // Tahmini teslimat tarihi
        public string? Notes { get; set; }
        public string? AddressName { get; set; }
        public string? Status { get; set; }
        public string? TrackingNumber { get; set; }

    }
}

