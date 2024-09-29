using ECommerceAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities.Concrete
{
    public class HelpCenter:BaseEntity
    {
        public string PopularQuestion { get; set; }
        public string PopularAnswer { get; set; }
    }
}
