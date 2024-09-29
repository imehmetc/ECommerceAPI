using ECommerceAPI.Domain.Entities.Concrete;
using ECommerceAPI.Domain.Entities.Identity;
using ECommerceAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs
{
    public class OrderDto:BaseDto
    {
        public Guid CartId { get; set; }
        public string PaymentMethod { get; set; }       // Ödeme yöntemi
        public DateTime OrderDate { get; set; }         // Sipariş tarihi
        public decimal ShippingCost { get; set; }       // Kargo ücreti
        public bool IsPaid { get; set; }                // Ödeme durumu
        public DateTime EstimatedDeliveryDate { get; set; }  // Tahmini teslimat tarihi
        public string? Notes { get; set; }
        public string CustomerId { get; set; }
        public Guid? AddressId { get; set; } // Foreign key for delivery address
        public string? Status { get; set; } 
        public string? TrackingNumber { get; set; }

    }
}
