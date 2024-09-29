using ECommerceAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstractions
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<IEnumerable<ProductDto>> GetAllProductsAsync(string sellerId);
        Task<ProductDto> GetProductByIdAsync(string id);
        Task<ProductDto> ProductDetailAsync(string id);
        Task<bool> AddProductAsync(ProductDto productDto);
        Task UpdateProductAsync(ProductDto productDto);
        Task DeleteProductAsync(Guid id);
        //Task<bool> ProductExistsAsync(Guid id);
        Task<List<ProductDto>> GetProductsWithSearch(string query);
        Task<List<ProductDto>> GetProductsWithCategoryId(Guid categoryId);
        Task<List<ProductDto>> GetProductsWithSellerId(Guid sellerId); // Marka olarak listelemek için
    }
}
