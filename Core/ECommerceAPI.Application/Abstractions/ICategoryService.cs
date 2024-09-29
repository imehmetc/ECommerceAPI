using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstractions
{
    public interface ICategoryService
    {
        Task<bool> AddCategoryAsync(CategoryDto categoryDto);

        Task UpdateCategoryAsync(CategoryDto categoryDto);

        Task DeleteCategoryAsync(Guid id);

        Task<CategoryDto> GetCategoryByIdAsync(Guid id);


        // Tüm ana kategorileri (üst kategorisi olmayanlar) listelemek için
        Task<List<CategoryDto>> GetAllMainCategoriesAsync();

        // Bir üst kategoriye bağlı tüm alt kategorileri listelemek için
        Task<List<CategoryDto>> GetSubCategoriesAsync(Guid parentCategoryId);

        // Tüm kategorileri, alt kategorileri ile birlikte listelemek için
        Task<List<CategoryDto>> GetAllCategoriesWithHierarchyAsync();

        Task<CategoryDto> GetParentCategoryByChildId(Guid childCategoryId);
    }
}
