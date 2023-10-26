using eShopSolution.Application.Common;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Data.Enums;
using eShopSolution.Utilities.Constants;
using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Sales;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Sales
{
    public class OrderService : IOrderService
    {
        private readonly EShopDbContext _context;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public OrderService(EShopDbContext context)
        {
            _context = context;
        }


        public async Task<Order> Create(CheckoutRequest request)
        {

            var order = new Order()
            {
                OrderDate = DateTime.Now,
                UserId = request.UserId,
                ShipName = request.Name,
                ShipAddress = request.Address,
                ShipEmail = request.Email,
                ShipPhoneNumber = request.PhoneNumber,
                Status = OrderStatus.InProgress,
                OrderDetails = new List<OrderDetail>() { }

            };

            foreach (var orderDetailVm in request.OrderDetails)
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == orderDetailVm.ProductId);

                if (product != null)
                {
                    var orderDetail = new OrderDetail()
                    {
                        ProductId = product.Id,
                        Quantity = orderDetailVm.Quantity,
                        Price = product.Price * orderDetailVm.Quantity  // Set the price based on the product's price
                    };

                    order.OrderDetails.Add(orderDetail);
                }
                else
                {
                    throw new Exception($"Product with ID {orderDetailVm.ProductId} not found.");
                }
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }



        public async Task<int> Create2(CheckoutRequest request)
        {
            var order = new Order()
            {
                OrderDate = DateTime.Now,
                UserId = request.UserId,
                ShipName = request.Name,
                ShipAddress = request.Address,
                ShipEmail = request.Email,
                ShipPhoneNumber = request.PhoneNumber,
                Status = OrderStatus.InProgress,
                OrderDetails = new List<OrderDetail>() { }

            };

            foreach (var orderDetailVm in request.OrderDetails)
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == orderDetailVm.ProductId);

                if (product != null)
                {
                    var orderDetail = new OrderDetail()
                    {
                        ProductId = product.Id,
                        Quantity = orderDetailVm.Quantity,
                        Price = product.Price * orderDetailVm.Quantity  // Set the price based on the product's price
                    };

                    order.OrderDetails.Add(orderDetail);
                }
                else
                {
                    throw new Exception($"Product with ID {orderDetailVm.ProductId} not found.");
                }
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order.Id;
        }

        public async Task<Order> GetById(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
            {
                throw new Exception($"Order with ID {orderId} not found.");
            }

            return order;
        }


    }
}
