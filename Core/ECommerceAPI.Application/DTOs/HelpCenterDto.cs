using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs
{
    public class HelpCenterDto:BaseDto
    {
        public string PopularQuestion { get; set; }
        public string PopularAnswer { get; set; }
    }
}
