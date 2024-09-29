using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities.Concrete
{
	public class Address : BaseEntity
	{
		public string Street { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
		public string PostalCode { get; set; }

		// Foreign key to User
		public string? AppUserId { get; set; }


		// Navigation properties
		public AppUser AppUser { get; set; }
		public ICollection<Order>? Orders { get; set; }
	}
}
