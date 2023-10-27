using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.Catalog.Orders;
using eShopSolution.ViewModels.Sales;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace eShopSolution.WebApp.ApiIntegration.Interface
{
    public interface IOrderApiClient
    {
        Task<Order> Create(CheckoutRequest request);
        Task<Order> GetLastestOrder();
        Task<List<BillHistoryVM>> GetBillHistory(Guid id);
        Task<BillHistoryDetailVM> BillDetail(int id);
    }
}
