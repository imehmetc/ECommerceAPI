using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs
{
    public class AboutUsDto:BaseDto
    {
        public string BusinessInfo { get; set; }
        public string WhatWeDo { get; set; }
    }
}
