using eShopSolution.Application.Catalog.Products;
using eShopSolution.Application.Common;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTest
{
    public class ProductServiceTest : IDisposable
    {
        private EShopDbContext _context;
        private IProductService _productService;
        private IStorageService _storageService;

        public ProductServiceTest()
        {
            var options = new DbContextOptionsBuilder<EShopDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new EShopDbContext(options);
            SeedInMemoryDatabase();
            _productService = new ProductService(_context, _storageService);
        }

        private void SeedInMemoryDatabase()
        {
            var images = new List<ProductImage>
        {
            new ProductImage
            {
                Id = 1,
                Caption = "Image 1",
                DateCreated = DateTime.Now,
                FileSize = 1024,
                ImagePath = "image1.jpg",
                IsDefault = true,
                ProductId = 1,
                SortOrder = 1
            },
            new ProductImage
            {
                Id = 2,
                Caption = "Image 2",
                DateCreated = DateTime.Now,
                FileSize = 2048,
                ImagePath = "image2.jpg",
                IsDefault = false,
                ProductId = 1,
                SortOrder = 2
            }
        };
            var products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Price = 100.00m,
                // Add other properties for product 1
            },
            new Product
            {
                Id = 2,
                Price = 200.00m,
                // Add other properties for product 2
            }
        };

            _context.ProductImages.AddRange(images);
            _context.Products.AddRange(products);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetImageById_ImageExists_ReturnsImage()
        {
            // Arrange
            var imageId = 1; // Replace with a valid image ID that exists in your test data

            // Act
            var result = await _productService.GetImageById(imageId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(imageId, result.Id);
        }

        [Fact]
        public async Task GetImageById_ImageDoesNotExist_ThrowsException()
        {
            // Arrange
            var imageId = 999; // Replace with an image ID that does not exist in your test data

            // Act and Assert
            var exception = await Assert.ThrowsAsync<EShopException>(() => _productService.GetImageById(imageId));
            Assert.Equal($"Cannot find an image with id {imageId}", exception.Message);
        }
        [Fact]
        public async Task UpdatePrice_ProductExists_ReturnsTrue()
        {
            // Arrange
            var productId = 1; // Replace with a valid product ID that exists in your test data
            var newPrice = 150.00m; // Replace with the new price value

            // Act
            var result = await _productService.UpdatePrice(productId, newPrice);

            // Assert
            Assert.True(result);
            var updatedProduct = await _context.Products.FindAsync(productId);
            Assert.Equal(newPrice, updatedProduct.Price);
        }

        [Fact]
        public async Task UpdatePrice_ProductDoesNotExist_ThrowsException()
        {
            // Arrange
            var productId = 999; // Replace with a product ID that does not exist in your test data
            var newPrice = 150.00m; // Replace with the new price value

            // Act and Assert
            var exception = await Assert.ThrowsAsync<EShopException>(() => _productService.UpdatePrice(productId, newPrice));
            Assert.Equal($"Cannot find a product with id: {productId}", exception.Message);
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
