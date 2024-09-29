using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.Abstractions.Comment;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Concrete;
using Mapster;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Concretes
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly ICommentService _commentService;

        public ProductService(IRepository<Product> productRepository, ICommentService commentService)
        {
            _productRepository = productRepository;
            _commentService = commentService;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync() // Bütün ürünler
        {
            var includes = new List<Expression<Func<Product, object>>>()
            {
                x => x.Category,
                x => x.UserComments,
                x => x.Seller
            };

            var products = await _productRepository.GetAllAsync(x => !x.IsDeleted, includes: includes);

            var productDtos = products.Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                StockQuantity = product.StockQuantity,
                Photo = product.Photo,
                CategoryId = product.CategoryId,
                SellerId = product.SellerId,
                CategoryName = product.Category.CategoryName,
                SellerName = product.Seller.UserName
            }).ToList();

            return productDtos;
        }
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(string sellerId) // Satıcının ürünleri
        {
            var includes = new List<Expression<Func<Product, object>>>()
            {
                x => x.Category,
                x => x.UserComments,
            };

            var products = await _productRepository.GetAllAsync(x => !x.IsDeleted && x.SellerId == sellerId, includes: includes);

            var productDtos = products.Adapt<List<ProductDto>>();

            return productDtos;
        }

        public async Task<ProductDto> GetProductByIdAsync(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return null;

            var productDto = product.Adapt<ProductDto>();

            return productDto;
        }

        public async Task<ProductDto> ProductDetailAsync(string id)
        {
            var includes = new List<Expression<Func<Product, object>>>()
            {
                x => x.Category,
                x => x.UserComments,
                x => x.Seller
            };

            var product = await _productRepository.GetByIdAsync(id, includes: includes);
            if (product == null) return null;

            var userCommentsByProductId = await _commentService.GetCommentsByProductIdAsync(id);

            var productDto = new ProductDto
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                SellerId = product.SellerId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                Photo = product.Photo,
                SellerName = product.Seller.UserName, // Seller bilgisi
                CategoryName = product.Category.CategoryName, // Category bilgisi
                UserComments = userCommentsByProductId
            };

            return productDto;
        }


        public async Task<bool> AddProductAsync(ProductDto productDto)
        {
            var mappedProduct = productDto.Adapt<Product>();
            var addedProduct = await _productRepository.AddAsync(mappedProduct);
            return addedProduct != null;
        }

        public async Task UpdateProductAsync(ProductDto productDto)
        {
            var updateProduct = productDto.Adapt<Product>();

            await _productRepository.UpdateAsync(updateProduct);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var deleteProduct = await _productRepository.GetByIdAsync(id.ToString());

            if (deleteProduct == null)
            {
                throw new ArgumentException("Product not found");
            }

            await _productRepository.RemoveAsync(deleteProduct);
        }

        public async Task<List<ProductDto>> GetProductsWithSearch(string query)
        {
            var products = await _productRepository.GetAllAsync(
                p => !p.IsDeleted &&
                (p.Name.ToLower().Contains(query.ToLower()) ||
                 p.Description.ToLower().Contains(query.ToLower()) ||
                 p.Category.CategoryName.ToLower().Contains(query.ToLower())),
                new List<Expression<Func<Product, object>>> { p => p.Category });
            if (products == null)
            {
                throw new ArgumentException("Products not found");
            }
            var mappedProducts = products.Adapt<List<ProductDto>>();
            return mappedProducts;
        }

        public async Task<List<ProductDto>> GetProductsWithCategoryId(Guid categoryId)
        {
            var products = await _productRepository.GetAllAsync(p => !p.IsDeleted && p.CategoryId == categoryId);
            if (products == null)
            {
                throw new ArgumentException("Products not found");
            }
            var mappedProducts = products.Adapt<List<ProductDto>>();
            return mappedProducts;
        }

        public async Task<List<ProductDto>> GetProductsWithSellerId(Guid sellerId)
        {
            var products = await _productRepository.GetAllAsync(p => !p.IsDeleted && p.SellerId == sellerId.ToString());
            if (products == null)
            {
                throw new ArgumentException("Products not found");
            }
            var mappedProducts = products.Adapt<List<ProductDto>>();
            return mappedProducts;
        }
    }
}