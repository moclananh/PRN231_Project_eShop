using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopSolution.WebApp.ApiIntegration.Interface
{
    public interface ICategoryApiClient
    {
        Task<List<CategoryVm>> GetAll(string languageId);

        Task<CategoryVm> GetById(int id, string languageId);
        Task<PagedResult<CategoryVm>> GetPagings(GetCategoryPagingRequest request);
     
    }
}
