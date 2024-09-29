using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs
{
    public class ProductStockDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int StockQuantity { get; set; }
        public int CriticalStockLevel { get; set; }
    }
}
