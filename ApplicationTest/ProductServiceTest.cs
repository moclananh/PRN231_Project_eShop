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
                Price = 10.99m,
                OriginalPrice = 9.99m,
                Stock = 100,
                ViewCount = 500,
                DateCreated = DateTime.Now,
                IsFeatured = true,
                ProductInCategories = new List<ProductInCategory>(),
                OrderDetails = new List<OrderDetail>(),
                Carts = new List<Cart>(),
                ProductTranslations = new List<ProductTranslation>(),
                ProductImages = new List<ProductImage>()
            },
            new Product
            {
                Id = 2,
                Price = 19.99m,
                OriginalPrice = 18.99m,
                Stock = 200,
                ViewCount = 1000,
                DateCreated = DateTime.Now,
                IsFeatured = false,
                ProductInCategories = new List<ProductInCategory>(),
                OrderDetails = new List<OrderDetail>(),
                Carts = new List<Cart>(),
                ProductTranslations = new List<ProductTranslation>(),
                ProductImages = new List<ProductImage>()
            },
            // Add more test products as needed
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

        [Fact]
        public async Task Delete_ProductExists_ReturnsOne()// truong hop dung
        {
            // Arrange
            var productId = 2; // Replace with a valid product ID that exists in your test data

            // Act
            var result = await _productService.Delete(productId);

            // Assert
            Assert.Equal(1, result);
            var product = await _context.Products.FindAsync(productId);
            Assert.Null(product); // Product should be deleted
        }

        [Fact]
        public async Task Delete_ProductDoesNotExist_ThrowsException()//truong hop sai
        {
            // Arrange
            var productId = 999; // Replace with a product ID that does not exist in your test data

            // Act and Assert
            var exception = await Assert.ThrowsAsync<EShopException>(() => _productService.Delete(productId));
            Assert.Equal($"Cannot find a product: {productId}", exception.Message);
        }

        [Fact]
        public async Task UpdateStock_ProductExists_ReturnsTrue()
        {
            // Arrange
            var productId = 2; // Replace with a valid product ID that exists in your test data
            var addedQuantity = 10; // Replace with the quantity to add

            // Act
            var result = await _productService.UpdateStock(productId, addedQuantity);

            // Assert
            Assert.True(result);

            var product = await _context.Products.FindAsync(productId);
            Assert.NotNull(product);
            Assert.Equal(210, product.Stock); // Check if the stock has been updated correctly.
        }

        [Fact]
        public async Task UpdateStock_ProductDoesNotExist_ThrowsException()
        {
            // Arrange
            var productId = 999; // Replace with a product ID that does not exist in your test data
            var addedQuantity = 10; // Replace with the quantity to add

            // Act and Assert
            var exception = await Assert.ThrowsAsync<EShopException>(() => _productService.UpdateStock(productId, addedQuantity));
            Assert.Equal($"Cannot find a product with id: {productId}", exception.Message);
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
