using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("all-products")] // Bütün ürünler
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            
            if (products != null) return Ok(products);

            return BadRequest();
        }

        [HttpGet("product-detail/{id}")] 
        public async Task<ActionResult<ProductDto>> GetProductDetail(string id)
        {
            var productDto = await _productService.ProductDetailAsync(id);

            if (productDto != null) return Ok(productDto);

            return BadRequest();
        }

        //[Authorize(Roles = "Seller")] // Yukarıdaki metodu kullanabilmem için Authorize kısmını End Point'lerin başına ekledim.
        [HttpGet("seller-products/{id}")] // Satıcının ürünleri
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(string id)
        {
            var products = await _productService.GetAllProductsAsync(id);
            
            if (products != null) return Ok(products);

            return BadRequest();
        }

        [Authorize(Roles = "Seller")]
        [HttpGet("product-{productId}")]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid productId)
        {
            var product = await _productService.GetProductByIdAsync(productId.ToString());
            if (product == null) return NotFound();

            return Ok(product);
        }

        [Authorize(Roles = "Seller")]
        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
        {
            var result = await _productService.AddProductAsync(productDto);
            if (result)
            {
                return Ok(new { success = true, message = "Product added successfully." });
            }
            else
            {
                return BadRequest(new { success = false, message = "Failed to add product." });
            }
        }

        [Authorize(Roles = "Seller")]
        [HttpPost("update-product")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto productDto)
        {
            await _productService.UpdateProductAsync(productDto);
            return Ok();
        }

        [Authorize(Roles = "Seller")]
        [HttpDelete("delete-{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok();
        }

        [HttpGet("get-products-with-query-{query}")]
        public async Task<ActionResult<List<ProductDto>>> GetProductsWithSearch(string query)
        {
            var products = await _productService.GetProductsWithSearch(query);
            if (products == null)
            {
                return NoContent();
            }
            return Ok(products);
        }

        [HttpGet("get-products-with-categoryid-{categoryId}")]
        public async Task<ActionResult<List<ProductDto>>> GetProductsWithCategoryId(Guid categoryId)
        {
            var products = await _productService.GetProductsWithCategoryId(categoryId);
            if (products == null)
            {
                return NoContent();
            }
            return Ok(products);
        }


        [HttpGet("get-products-with-sellerId-{sellerId}")]
        public async Task<ActionResult<List<ProductDto>>> GetProductsWithSellerId(Guid sellerId)
        {
            var products = await _productService.GetProductsWithSellerId(sellerId);
            if (products == null)
            {
                return NoContent();
            }
            return Ok(products);
        }
    }
}
