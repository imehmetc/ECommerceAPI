using ECommerceAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities.Concrete
{
    public class Contact:BaseEntity
    {
        public string Address { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

    }
}
