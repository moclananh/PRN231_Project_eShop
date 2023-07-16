using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
    public interface ICategoryApiClient
    {
        Task<List<CategoryVm>> GetAll(string languageId);

        Task<CategoryVm> GetById(int id, string languageId);
        Task<PagedResult<CategoryVm>> GetPagings(GetCategoryPagingRequest request);
        Task<bool> CreateCategory(CategoryCreateRequest request);
        Task<bool> UpdateCategory(CategoryUpdateRequest request);
        Task<bool> DeleteCategory(int id);
    }
}