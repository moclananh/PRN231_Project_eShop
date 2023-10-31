using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Catalog.Orders;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
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

        Task<Order> GetLastestOrderId();

        Task<PagedResult<OrderVm>> GetAllPaging(OrderPagingRequest request);
        Task<PagedResult<OrderDetailView>> GetOrderDetailPagingRequest(OrderDetailPagingRequest request);
        Task<BillHistoryDetailVM> GetBillById(int id);
        Task<List<BillHistoryVM>> BillHistory(Guid id);

        Task<int> UpdateStatus(UpdateStatusRequest request);

    }
}
