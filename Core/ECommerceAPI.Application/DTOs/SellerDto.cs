using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs
{
    public class SellerDto:BaseDto
    {
        public string FirstName { get; set; }
        public string? SecondName { get; set; }
        public string LastName { get; set; }
        public string? SecondLastName { get; set; }
        public string Email { get; set; }
        public string? Photo { get; set; }
        public IFormFile? PhotoUrl { get; set; }
        public string CompanyName { get; set; }
        public string? ContactInformation { get; set; }
        public bool? IsApproved { get; set; } = false;
    }
}
