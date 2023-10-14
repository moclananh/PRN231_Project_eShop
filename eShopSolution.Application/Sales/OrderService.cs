using eShopSolution.Application.Common;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Data.Enums;
using eShopSolution.Utilities.Constants;
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
                UserId = request.Id,
                ShipName = request.Name,
                ShipAddress = request.Address,
                ShipEmail = request.Email,
                ShipPhoneNumber = request.PhoneNumber,
                Status = OrderStatus.InProgress,
                OrderDetails = new List<OrderDetail>()
                {
                    new OrderDetail()
                    {
                       //ProductId = request.OrderDeta,
                       Quantity = 10,
                       Price = 23
                    }
                }
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }
        /*
                public async Task<ProductVm> GetById(int productId, string languageId)
                {
                    var product = await _context.Orders.FindAsync(productId);
                    var categories = await (from c in _context.Categories
                                            join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
                                            join pic in _context.ProductInCategories on c.Id equals pic.CategoryId
                                            where pic.ProductId == productId && ct.LanguageId == languageId
                                            select ct.Name).ToListAsync();

                    var image = await _context.ProductImages.Where(x => x.ProductId == productId && x.IsDefault == true).FirstOrDefaultAsync();

                    var order = new CheckoutRequest()
                    {
                        Name = product.ShipName,
                        Address = DateTime.Now,
                        UserId = request.Id,
                        ShipName = request.Name,
                        ShipAddress = request.Address,
                        ShipEmail = request.Email,
                        ShipPhoneNumber = request.PhoneNumber,
                        Status = OrderStatus.InProgress,
                    };

                    var productViewModel = new CheckoutRequest()
                    {
                        Id = product.Id,
                        OrderDate = product.OrderDate,

                    };
                    return productViewModel;
                }
        */
        Task<CheckoutRequest> IOrderService.GetById(int Id, string languageId)
        {
            throw new NotImplementedException();
        }
    }
}
