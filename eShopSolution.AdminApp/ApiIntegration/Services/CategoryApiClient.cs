using eShopSolution.AdminApp.ApiIntegration.Interface;
using eShopSolution.Utilities.Constants;
using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Common;
using System.Net.Http.Headers;

namespace eShopSolution.AdminApp.ApiIntegration.Services
{
    public class CategoryApiClient : BaseApiClient, ICategoryApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public CategoryApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> CreateCategory(CategoryCreateRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);

            var languageId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();

            requestContent.Add(new StringContent(request.SortOrder.ToString()), "sort");
            requestContent.Add(new StringContent(request.IsShowOnHome.ToString()), "isShowOnHome");
            requestContent.Add(new StringContent(request.Status.ToString()), "status");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Name) ? "" : request.Name.ToString()), "name");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.SeoDescription) ? "" : request.SeoDescription.ToString()), "seoDescription");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.SeoTitle) ? "" : request.SeoTitle.ToString()), "seoTitle");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.SeoAlias) ? "" : request.SeoAlias.ToString()), "seoAlias");
            requestContent.Add(new StringContent(languageId), "languageId");

            var response = await client.PostAsync($"/api/categories/", requestContent);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateCategory(CategoryUpdateRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);

            var languageId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();
            //requestContent.Add(new StringContent(request.Id.ToString()), "id");

            requestContent.Add(new StringContent(request.SortOrder.ToString()), "sort");
            requestContent.Add(new StringContent(request.IsShowOnHome.ToString()), "isShowOnHome");
            requestContent.Add(new StringContent(request.Status.ToString()), "status");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Name) ? "" : request.Name.ToString()), "name");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.SeoDescription) ? "" : request.SeoDescription.ToString()), "seoDescription");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.SeoTitle) ? "" : request.SeoTitle.ToString()), "seoTitle");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.SeoAlias) ? "" : request.SeoAlias.ToString()), "seoAlias");
            requestContent.Add(new StringContent(languageId), "languageId");

            var response = await client.PutAsync($"/api/categories/" + request.Id, requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryVm>> GetAll(string languageId)
        {
            return await GetListAsync<CategoryVm>("/api/categories?languageId=" + languageId);
        }

        public async Task<CategoryVm> GetById(int id, string languageId)
        {
            return await GetAsync<CategoryVm>($"/api/categories/{id}/{languageId}");
        }

        public async Task<PagedResult<CategoryVm>> GetPagings(GetCategoryPagingRequest request)
        {
            var data = await GetAsync<PagedResult<CategoryVm>>(
                 $"/api/categories/paging?pageIndex={request.PageIndex}" +
                 $"&pageSize={request.PageSize}" +
                 $"&keyword={request.Keyword}&languageId={request.LanguageId}");

            return data;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            return await Delete($"/api/categories/" + id);
        }
    }
}
