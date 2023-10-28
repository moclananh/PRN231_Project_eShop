using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.Statistical;

namespace eShopSolution.AdminApp.ApiIntegration.Interface
{
    public interface IStatisticalApiClient
    {
        Task<PagedResult<StatisticalVm>> Statistical(StatisticalRequest request);
    }
}
