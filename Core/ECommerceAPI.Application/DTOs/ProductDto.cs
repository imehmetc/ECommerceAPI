using ECommerceAPI.Domain.Entities.Concrete;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs
{
    public class ProductDto : BaseDto
    {
        //public string Name { get; set; }
        //public string Description { get; set; }
        //public decimal Price { get; set; }
        //public int Stock { get; set; }
        //public int StockQuantity { get; set; }
        //public string? Photo { get; set; }
        //public Guid CategoryId { get; set; }
        //public string SellerId { get; set; }


        //// Navigation properties
        //public CategoryDto CategoryDto { get; set; }

        //public AppUser Seller { get; set; }
        //public ICollection<OrderDto> OrderDtos { get; set; }
        //public ICollection<UserCommentDto> userCommentDtos { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int StockQuantity { get; set; }
        public string? Photo { get; set; }
        public Guid CategoryId { get; set; }
        public string SellerId { get; set; }
        public string? CategoryName { get; set; }  
        public string? SellerName { get; set; }

        public ICollection<UserCommentDto> UserComments { get; set; } 
        public ICollection<CartItemDto> CartItemDtos { get; set; }
        public AppUser Seller { get; set; }
    }
}
