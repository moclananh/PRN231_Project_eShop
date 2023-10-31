using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.Sales;
using eShopSolution.ViewModels.Statistical;

namespace eShopSolution.AdminApp.ApiIntegration.Interface
{
    public interface IOrderApiClient
    {
        Task<PagedResult<OrderVm>> GetPagings(OrderPagingRequest request);
        Task<PagedResult<OrderDetailView>> GetOrderDetailPagings(OrderDetailPagingRequest request);

        Task<bool> UpdateStatus(UpdateStatusRequest request);

        Task<Order> GetById(int id);
    }
}
