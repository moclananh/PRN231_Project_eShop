using eShopSolution.Application.Common;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Data.Enums;
using eShopSolution.Utilities.Constants;
using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Catalog.Orders;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
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


        public async Task<Order> GetById(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
            {
                throw new Exception($"Order with ID {orderId} not found.");
            }

            return order;
        }

        public async Task<Order> GetLastestOrderId()
        {
            var latestOrder = await _context.Orders.OrderByDescending(order => order.Id).FirstOrDefaultAsync();

            if (latestOrder == null)
            {
                throw new Exception("No orders found in the database.");
            }

            return latestOrder;
        }

        public async Task<PagedResult<OrderVm>> GetAllPaging(OrderPagingRequest request)
        {
            //1. Select join
            var query = from o in _context.Orders
                        join od in _context.OrderDetails on o.Id equals od.OrderId
                        join p in _context.Products on od.ProductId equals p.Id
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        
                        select new OrderVm()
                        {
                            Id = o.Id,
                            OrderDate = o.OrderDate,
                            UserId = o.UserId,
                            ShipName = o.ShipName,
                            ShipAddress = o.ShipAddress,
                            ShipEmail = o.ShipEmail,
                            ShipPhoneNumber = o.ShipPhoneNumber,
                            Status = o.Status,
                            OrderDetails = new List<OrderDetail>()

                        };

            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.ShipPhoneNumber.Contains(request.Keyword));

            // Create a list to store distinct products
            List<OrderVm> distinctOrder = new List<OrderVm>();

            foreach (var orderVm in query)
            {
                // Check if the product with the same ID is already in the distinctProducts list
                if (!distinctOrder.Any(p => p.Id == orderVm.Id))
                {
                    distinctOrder.Add(orderVm);
                }
            }
            int totalRow = distinctOrder.Count();

            var data = distinctOrder
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var pagedResult = new PagedResult<OrderVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }

        public async Task<PagedResult<OrderDetailView>> GetOrderDetailPagingRequest(OrderDetailPagingRequest request)
        {
            //1. Select join
            var query = from o in _context.Orders
                        join od in _context.OrderDetails on o.Id equals od.OrderId
                        join p in _context.Products on od.ProductId equals p.Id
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join l in _context.Languages on pt.LanguageId equals l.Id
                        where o.Id == request.OrderId && l.Id == request.LanguageId
                        select new OrderDetailView()
                        {
                          ProductName = pt.Name,
                          Quatity = od.Quantity,
                          Price = od.Price
                        };

            // Create a list to store distinct products
            List<OrderDetailView> distinctOrder = new List<OrderDetailView>();

            foreach (var order in query)
            {
                // Check if the product with the same ID is already in the distinctProducts list
                if (!distinctOrder.Any(p => p.ProductName == "N/A"))
                {
                    distinctOrder.Add(order);
                }
            }

            int totalRow = distinctOrder.Count();

            var data = distinctOrder
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var pagedResult = new PagedResult<OrderDetailView>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }
        public async Task<BillHistoryDetailVM> GetBillById(int id)
        {
            var result = new BillHistoryDetailVM();

            var query = await (from o in _context.Orders
                               join a in _context.AppUsers on o.UserId equals a.Id
                               join od in _context.OrderDetails on o.Id equals od.OrderId
                               join p in _context.Products on od.ProductId equals p.Id
                               join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                               where o.Id == id
                               select new
                               {
                                   ID = o.Id,
                                   Name = pt.Name,
                                   Quantity = od.Quantity,
                                   Price = p.Price,
                                   Status = o.Status
                               }).ToListAsync();

            // Filter out entries with Name = "N/A"
            query = query.Where(item => item.Name != "N/A").ToList();

            if (query.Any())
            {
                result.ID = query.First().ID;
                result.status = query.First().Status;

                // Populate the lists
                result.name = query.Select(item => item.Name).ToList();
                result.quantity = query.Select(item => item.Quantity).ToList();
                result.price = query.Select(item => item.Price).ToList();
            }

            return result;
        }






        public async Task<List<BillHistoryVM>> BillHistory(Guid id)
        {
            var user = await _context.AppUsers.FindAsync(id);
            var result = new List<BillHistoryVM>();

            var query = await (from o in _context.Orders
                               join a in _context.AppUsers on o.UserId equals a.Id
                               join od in _context.OrderDetails on o.Id equals od.OrderId
                               join p in _context.Products on od.ProductId equals p.Id
                               where o.UserId == id
                               select new BillHistoryVM
                               {
                                   id = o.Id,
                                   email = a.Email,
                                   address = o.ShipAddress,
                                   orderDate = o.OrderDate,
                                   price = od.Price,
                                   Status = o.Status
                               }).ToListAsync();

            // Group the results by id and calculate the sum of price
            var groupedQuery = query
                .GroupBy(item => item.id)
                .Select(group => new BillHistoryVM
                {
                    id = group.Key,
                    email = group.First().email,
                    address = group.First().address,
                    orderDate = group.First().orderDate,
                    price = group.Sum(item => item.price),
                    Status = group.First().Status
                }).ToList();

            result.AddRange(groupedQuery);

            return result;
        }
    }
}
