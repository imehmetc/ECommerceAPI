using ECommerceAPI.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs
{
    public class CustomerDto:BaseDto
    {
        public string FirstName { get; set; }
        public string? SecondName { get; set; }
        public string LastName { get; set; }
        public string? SecondLastName { get; set; }
        public string Email { get; set; }
        public ICollection<OrderDto>? Orders { get; set; }
        public ICollection<AddressDto>? Addresses { get; set; }
        public string PhoneNumber { get; set; }
        public string? Photo { get; set; }
        public IFormFile? PhotoUrl { get; set; }
    }
}
