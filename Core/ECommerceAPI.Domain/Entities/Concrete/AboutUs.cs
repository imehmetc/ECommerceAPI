using ECommerceAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities.Concrete
{
    public class AboutUs:BaseEntity
    {
        public string BusinessInfo { get; set; }
        public string WhatWeDo { get; set; }
    }
}
