using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs
{
    public class CartItemDto : BaseDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal? Discount { get; set; }
        public bool IsInStock { get; set; } = true;
        public string? Photo { get; set; }
        public bool IsOrdered { get; set; } = false;
        public string CustomerId { get; set; }
        public Guid CartId { get; set; }
        //public CartDto Cart { get; set; }
        //public string? PaymentMethod { get; set; }       // Ödeme yöntemi
        //public DateTime? OrderDate { get; set; }         // Sipariş tarihi
        //public decimal? ShippingCost { get; set; }       // Kargo ücreti
        //public bool? IsPaid { get; set; }                // Ödeme durumu
        //public DateTime? EstimatedDeliveryDate { get; set; }  // Tahmini teslimat tarihi
        //public string? Notes { get; set; }
        //public Guid? AddressId { get; set; } // Foreign key for delivery address
        //public string? Status { get; set; }
        //public string? TrackingNumber { get; set; }
    }
}
