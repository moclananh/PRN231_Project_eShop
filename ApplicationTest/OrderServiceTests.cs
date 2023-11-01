using eShopSolution.Application.Sales;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.Sales;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTest
{
    public class OrderServiceTests
    {
        private EShopDbContext _context;
        private IOrderService _orderService;

        public OrderServiceTests()
        {
            // Initialize a new context with the in-memory database for each test
            var options = new DbContextOptionsBuilder<EShopDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use a unique name for each test
                .Options;

            _context = new EShopDbContext(options);
            SeedInMemoryDatabase(); // Populate the database with test data
            _orderService = new OrderService(_context);
        }

        private void SeedInMemoryDatabase()
        {
            var orders = new List<Order>
        {
            new Order
            {
                Id = 1,
                ShipAddress = "123 Main St",
                ShipEmail = "customer@example.com",
                ShipName = "John Doe",
                ShipPhoneNumber = "123-456-7890",
                // Add other properties for order 1
            },
            new Order
            {
                Id = 2,
                ShipAddress = "456 Elm St",
                ShipEmail = "customer2@example.com",
                ShipName = "Jane Smith",
                ShipPhoneNumber = "987-654-3210",
                // Add other properties for order 2
            },
            // Add more test orders as needed
        };
            var products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Price = 10.0m,
                OriginalPrice = 9.0m,
                Stock = 100,
                ViewCount = 50,
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
                Price = 15.0m,
                OriginalPrice = 12.0m,
                Stock = 75,
                ViewCount = 60,
                DateCreated = DateTime.Now,
                IsFeatured = false,
                ProductInCategories = new List<ProductInCategory>(),
                OrderDetails = new List<OrderDetail>(),
                Carts = new List<Cart>(),
                ProductTranslations = new List<ProductTranslation>(),
                ProductImages = new List<ProductImage>()
            },
        // Add more mock product entries as needed
        };
            _context.Orders.AddRange(orders);
            _context.Products.AddRange(products);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetById_OrderExists_ReturnsOrder()
        {
            // Act
            var orderId = 1; // Replace with a valid order ID that exists in your test data
            var result = await _orderService.GetById(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orderId, result.Id);
        }
        [Fact]
        public async Task GetById_OrderDoesNotExist_ReturnsNull()
        {
            // Act and Assert
            var orderId = 999; // Replace with an order ID that does not exist in your test data
            var exception = await Assert.ThrowsAsync<Exception>(() => _orderService.GetById(orderId));
            Assert.Equal("Order with ID 999 not found.", exception.Message);
        }
        [Fact]
        public async Task Create_ValidOrder_ReturnsCreatedOrder()
        {
            // Arrange
            var userId = Guid.NewGuid(); // Generate a new GUID
            var request = new CheckoutRequest
            {
                UserId = userId,
                Name = "John Doe",
                Address = "123 Main St",
                Email = "john@example.com",
                PhoneNumber = "123-456-7890",
                OrderDetails = new List<OrderDetailVm>
        {
            new OrderDetailVm
            {
                ProductId = 1, // Replace with an existing product ID in your test data
                Quantity = 2
            },
            // Add more order details if needed
        }
            };

            // Act
            var createdOrder = await _orderService.Create(request);

            // Assert
            Assert.NotNull(createdOrder);
            Assert.Equal(userId, createdOrder.UserId);
            Assert.Equal("John Doe", createdOrder.ShipName);
            Assert.Equal("123 Main St", createdOrder.ShipAddress);
            Assert.Equal("john@example.com", createdOrder.ShipEmail);
            Assert.Equal("123-456-7890", createdOrder.ShipPhoneNumber);
            // Add more assertions based on your expectations
        }




        [Fact]
        public async Task Create_OrderWithNonExistentProduct_ThrowsException()
        {
            // Arrange
            var request = new CheckoutRequest
            {
                OrderDetails = new List<OrderDetailVm>
            {
                new OrderDetailVm
                {
                    ProductId = 999, // Replace with a product ID that does not exist
                    Quantity = 1
                }
            },
                // Set other valid checkout request properties
            };

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(() => _orderService.Create(request));
        }

        public void Dispose()
        {
            // Clean up and dispose of the context when the tests are done
            _context.Dispose();
        }
    }
}
