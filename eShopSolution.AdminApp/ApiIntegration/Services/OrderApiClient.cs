using eShopSolution.AdminApp.ApiIntegration.Interface;
using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Constants;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.Sales;
using eShopSolution.ViewModels.Statistical;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Net.Http.Headers;

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

        public async Task<Order> GetById(int id)
        {
            var data = await GetAsync<Order>($"/api/orders/{id}");

            return data;
        }

        public async Task<bool> UpdateStatus(UpdateStatusRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();


            //requestContent.Add(new StringContent(request.Id.ToString()), "id");
            //requestContent.Add(new StringContent(request.OrderId.ToString()), "OrderId");
            requestContent.Add(new StringContent(request.Status.ToString()), "Status");

            var response = await client.PutAsync($"/api/orders/" + request.OrderId, requestContent);
            return response.IsSuccessStatusCode;
        }
    }
}
