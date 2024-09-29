using ECommerceAPI.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs
{
    public class CategoryDto : BaseDto
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }

        // Her kategori bir üst kategorinin alt kategorisi olabilir
        public Guid? ParentCategoryId { get; set; }
        //public CategoryDto? ParentCategory { get; set; }
        public int? CategoryLevel { get; set; }

        // Bir kategorinin birden fazla alt kategorisi olabilir
        public ICollection<Category>? ChildCategories { get; set; } = new List<Category>();
    }
}
