using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Users;
using System.Threading.Tasks;
using System;

namespace eShopSolution.WebApp.ApiIntegration.Interface
{
    public interface IUserApiClient
    {
        Task<ApiResult<string>> Authenticate(LoginRequest request);

        Task<ApiResult<PagedResult<UserVm>>> GetUsersPagings(GetUserPagingRequest request);

        Task<ApiResult<bool>> RegisterUser(RegisterRequest registerRequest);
        Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest request);

        Task<ApiResult<UserVm>> GetById(Guid id);

    }
}
