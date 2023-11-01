using eShopSolution.Application.Sales;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
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

            _context.Orders.AddRange(orders);
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

        public void Dispose()
        {
            // Clean up and dispose of the context when the tests are done
            _context.Dispose();
        }
    }
}
