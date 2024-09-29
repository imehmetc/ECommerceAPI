using ECommerceAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities.Concrete
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }

        // Her kategori bir üst kategorinin alt kategorisi olabilir
        public Guid? ParentCategoryId { get; set; }
        //public Category ParentCategory { get; set; }
        public int CategoryLevel { get; set; }

        // Bir kategorinin birden fazla alt kategorisi olabilir
        public ICollection<Category> ChildCategories { get; set; } = new List<Category>();

        // Navigation property
        public ICollection<Product> Products { get; set; }
    }
}
