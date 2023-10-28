using eShopSolution.AdminApp.ApiIntegration.Interface;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.Statistical;

namespace eShopSolution.AdminApp.ApiIntegration.Services
{
    public class StatisticalApiClient : BaseApiClient, IStatisticalApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public StatisticalApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }



        public async Task<PagedResult<StatisticalVm>> Statistical(StatisticalRequest request)
        {

            var data = await GetAsync<PagedResult<StatisticalVm>>(
              $"/api/Statistical?" +
              $"StartDate={request.StartDate}" +
              $"&EndDate={request.EndDate}" +
              $"&PageIndex={request.PageIndex}" +
              $"&PageSize={request.PageSize}");

            return data;
        }
    }
}
