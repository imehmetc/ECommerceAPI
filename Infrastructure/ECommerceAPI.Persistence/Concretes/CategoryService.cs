using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Concrete;
using ECommerceAPI.Persistence.Migrations;
using Mapster;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerceAPI.Persistence.Concretes
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> AddCategoryAsync(CategoryDto categoryDto)
        {
            var category = categoryDto.Adapt<Category>();

            if (category.ParentCategoryId.HasValue)
            {
                var parentCategory = await _categoryRepository.GetByIdAsync(category.ParentCategoryId.Value.ToString());

                if (parentCategory != null)
                {
                    category.CategoryLevel = parentCategory.CategoryLevel + 1;

                    if (parentCategory.ChildCategories == null)
                    {
                        parentCategory.ChildCategories = new List<Category>();
                    }

                    parentCategory.ChildCategories.Add(category);
                    var addedCategory = await _categoryRepository.AddAsync(category);
                    await _categoryRepository.UpdateAsync(parentCategory);
                    return addedCategory != null;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                category.CategoryLevel = 1;
            }

            var addedParentCategory = await _categoryRepository.AddAsync(category);
            return addedParentCategory != null;

        }

        public async Task UpdateCategoryAsync(CategoryDto categoryDto)
        {
            var updatedCategory = categoryDto.Adapt<Category>();

            var existingCategory = await _categoryRepository.GetByIdAsync(updatedCategory.Id.ToString(), includes: new List<Expression<Func<Category, object>>> { c => c.ChildCategories, c => c.ParentCategoryId });

            if (existingCategory == null)
            {
                throw new ArgumentException("Category not found");
            }

            if (updatedCategory.ParentCategoryId.HasValue)
            {
                var parentCategory = await _categoryRepository.GetByIdAsync(updatedCategory.ParentCategoryId.Value.ToString());

                if (parentCategory != null)
                {
                    updatedCategory.CategoryLevel = parentCategory.CategoryLevel + 1;
                }
                else
                {
                    throw new ArgumentException("Parent category not found");
                }
            }
            else
            {
                updatedCategory.CategoryLevel = 1;
            }

            if (existingCategory.ParentCategoryId != updatedCategory.ParentCategoryId)
            {
                if (existingCategory.ParentCategoryId.HasValue)
                {
                    var oldParentCategory = await _categoryRepository.GetByIdAsync(existingCategory.ParentCategoryId.Value.ToString());
                    oldParentCategory.ChildCategories.Remove(existingCategory);
                    await _categoryRepository.UpdateAsync(oldParentCategory);
                }

                if (updatedCategory.ParentCategoryId.HasValue)
                {
                    var newParentCategory = await _categoryRepository.GetByIdAsync(updatedCategory.ParentCategoryId.Value.ToString());
                    if (newParentCategory != null)
                    {
                        if (newParentCategory.ChildCategories == null)
                        {
                            newParentCategory.ChildCategories = new List<Category>();
                        }
                        newParentCategory.ChildCategories.Add(updatedCategory);
                        await _categoryRepository.UpdateAsync(newParentCategory);
                    }
                }
            }

            await _categoryRepository.UpdateAsync(updatedCategory);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            var categoryToDelete = await _categoryRepository.GetByIdAsync(id.ToString());

            if (categoryToDelete == null)
            {
                throw new ArgumentException("Category not found");
            }

            var childCategories = await _categoryRepository.GetAllAsync(c => c.ParentCategoryId == id && !c.IsDeleted);

            if (childCategories.Any())
            {
                foreach (var childCategory in childCategories)
                {
                    await _categoryRepository.RemoveAsync(childCategory);
                }
            }
            await _categoryRepository.RemoveAsync(categoryToDelete);
        }

        public async Task<List<CategoryDto>> GetAllCategoriesWithHierarchyAsync()
        {
            var allCategories = await _categoryRepository.GetAllAsync(c => !c.IsDeleted, includes: new List<Expression<Func<Category, object>>> { c => c.ChildCategories });
            return allCategories.Adapt<List<CategoryDto>>();
        }

        public async Task<List<CategoryDto>> GetAllMainCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync(c => c.ParentCategoryId == null && !c.IsDeleted);
            var mappedMainCategories = categories.Adapt<List<CategoryDto>>();
            return mappedMainCategories;
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id.ToString(), includes: null);
            var mappedCategory = category.Adapt<CategoryDto>();
            return mappedCategory;
        }

        public async Task<List<CategoryDto>> GetSubCategoriesAsync(Guid parentCategoryId)
        {
            var subCategories = await _categoryRepository.GetAllAsync(c => c.ParentCategoryId == parentCategoryId && !c.IsDeleted);
            var mappedSubCategories = subCategories.Adapt<List<CategoryDto>>();
            return mappedSubCategories;
        }

        public async Task<CategoryDto> GetParentCategoryByChildId(Guid childCategoryId)
        {
            var childCategory = await _categoryRepository.GetByIdAsync(childCategoryId.ToString());
            if (childCategory == null) return null;

            var parentCategory = await _categoryRepository.GetByIdAsync(childCategory.ParentCategoryId.ToString());
            return parentCategory.Adapt<CategoryDto>();
        }
    }
}
