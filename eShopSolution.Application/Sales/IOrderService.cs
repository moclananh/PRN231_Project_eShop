using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Sales
{
    public interface IOrderService
    {
        Task<Order> Create(CheckoutRequest request);

        Task<Order> GetById(int id);
        Task<int> Create2(CheckoutRequest request);
    }
}
