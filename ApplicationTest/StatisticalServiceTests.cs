using eShopSolution.Application.Statistical;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Data.Enums;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.Statistical;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTest
{
    public class StatisticalServiceTests : IDisposable
    {
        private EShopDbContext _context;
        private IStatisticalService _statisticalService;

        public StatisticalServiceTests()
        {
            var options = new DbContextOptionsBuilder<EShopDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new EShopDbContext(options);
            SeedInMemoryDatabase();
            _statisticalService = new StatisticalServie(_context);
        }

        private void SeedInMemoryDatabase()
        {
            var orders = new List<Order>
        {
            new Order
            {
                Id = 1,
                OrderDate = DateTime.Now,
                UserId = Guid.NewGuid(),
                ShipName = "John Doe",
                ShipAddress = "123 Main St",
                ShipEmail = "john@example.com",
                ShipPhoneNumber = "123-456-7890",
                Status = OrderStatus.InProgress,
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail
                    {
                        OrderId = 1,
                        ProductId = 1,
                        Quantity = 2,
                        Price = 10.0m,
                        Product = new Product
                        {
                            Id = 1,
                            Price = 100
                        }
                    }
                },
                AppUser = new AppUser
                {
                    Id = Guid.NewGuid(),
                    UserName = "user1",
                    FirstName= "Test",
                    LastName = "Test",
                    Email= "Test",

                }
            },
            new Order
            {
                Id = 2,
                OrderDate = DateTime.Now,
                UserId = Guid.NewGuid(),
                ShipName = "Jane Smith",
                ShipAddress = "456 Elm St",
                ShipEmail = "jane@example.com",
                ShipPhoneNumber = "987-654-3210",
                Status = OrderStatus.Canceled,
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail
                    {
                        OrderId = 2,
                        ProductId = 2,
                        Quantity = 3,
                        Price = 15.0m,
                        Product = new Product
                        {
                            Id = 2,
                          
                            Price = 150
                        }
                    }
                },
                AppUser = new AppUser
                {
                    Id = Guid.NewGuid(),
                    UserName = "user2",
                    FirstName= "Test2",
                    LastName = "Test2",
                    Email= "Test2",
                }
            }
        };

            _context.Orders.AddRange(orders);
            _context.SaveChanges();
        }

        // ... Your test methods

        [Fact]
        public async Task Statistical_ReturnsStatisticalData()
        {
            // Arrange
            var request = new StatisticalRequest
            {
                StartDate = DateTime.Now.AddYears(-1), // Adjust as needed
                EndDate = DateTime.Now.AddYears(1),
                PageIndex = 1,
                PageSize = 10,
            };

            // Act
            var result = await _statisticalService.Statistical(request);


            // Assert
            Assert.NotNull(result);
            Assert.IsType<PagedResult<StatisticalVm>>(result);

            // Add more specific assertions based on the expected data
            // For example, check the TotalRecords, PageSize, PageIndex, and Items properties.
         
            Assert.Equal(10, result.PageSize);
            Assert.Equal(1, result.PageIndex);
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
