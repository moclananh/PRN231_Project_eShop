using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.Sales;
using System.Threading.Tasks;

namespace eShopSolution.WebApp.ApiIntegration.Interface
{
    public interface IOrderApiClient
    {
        Task<Order> Create(CheckoutRequest request);
    }
}
