using eShopSolution.AdminApp.ApiIntegration.Interface;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.Sales;
using eShopSolution.ViewModels.Statistical;
using System.Globalization;

namespace eShopSolution.AdminApp.ApiIntegration.Services
{
    public class OrderApiClient : BaseApiClient, IOrderApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public OrderApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PagedResult<OrderDetailView>> GetOrderDetailPagings(OrderDetailPagingRequest request)
        {
            var data = await GetAsync<PagedResult<OrderDetailView>>(
               $"/api/orders/GetOrderDetailPagingRequest?" +
               $"OrderId={request.OrderId}" +
               $"&pageSize={request.PageSize}" +
               $"&orderId={request.OrderId}");

            return data;
        }

        public async Task<PagedResult<OrderVm>> GetPagings(OrderPagingRequest request)
        {
            var data = await GetAsync<PagedResult<OrderVm>>(
                $"/api/orders/paging?" +
                $"pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}" +
                $"&keyword={request.Keyword}");

            return data;
        }

    }
}
