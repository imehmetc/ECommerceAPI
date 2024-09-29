using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
    
        // POST: api/category/add-category
        [HttpPost("add-category")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto categoryDto)
        {
            var result = await _categoryService.AddCategoryAsync(categoryDto);
            if (result)
            {
                return Ok(new { success = true, message = "Category added successfully." });
            }
            else
            {
                return BadRequest(new { success = false, message = "Failed to add category." });
            }
        }

        // POST: api/category/update-category
        [HttpPost("update-category")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDto categoryDto)
        {
            await _categoryService.UpdateCategoryAsync(categoryDto);
            return Ok();
        }

        // DELETE: api/category/delete-{id}
        [HttpDelete("delete-{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Ok();
        }

        // GET: api/category/get-by-{id}
        [HttpGet("get-by-{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // GET: api/category/hierarchy
        [HttpGet("hierarchy")]
        public async Task<IActionResult> GetAllCategoriesWithHierarchy()
        {
            var categories = await _categoryService.GetAllCategoriesWithHierarchyAsync();
            return Ok(categories);
        }

        // GET: api/category/main-categories
        [HttpGet("main-categories")]
        public async Task<IActionResult> GetAllMainCategories()
        {
            var categories = await _categoryService.GetAllMainCategoriesAsync();
            return Ok(categories);
        }

        // GET: api/category/parent/{parentId}
        [HttpGet("parent/{parentId}")]
        public async Task<IActionResult> GetAllSubCategories(Guid parentId)
        {
            var subCategories = await _categoryService.GetSubCategoriesAsync(parentId);
            return Ok(subCategories);
        }

        [HttpGet("child-{childId}")]
        public async Task<IActionResult> GetParentCategoryByChildId(Guid childId)
        {
            var subCategories = await _categoryService.GetParentCategoryByChildId(childId);
            return Ok(subCategories);
        }
    }
}
