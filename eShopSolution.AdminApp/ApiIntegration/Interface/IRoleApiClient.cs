using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Roles;

namespace eShopSolution.AdminApp.ApiIntegration.Interface
{
    public interface IRoleApiClient
    {
        Task<ApiResult<List<RoleVm>>> GetAll();
    }
}
