using eShopSolution.AdminApp.ApiIntegration.Interface;
using eShopSolution.Utilities.Constants;
using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Sales;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eShopSolution.AdminApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderApiClient _orderApiClient;
        private readonly IConfiguration _configuration;


        public OrderController(IOrderApiClient orderApiClient,
            IConfiguration configuration
           )
        {
            _configuration = configuration;
            _orderApiClient = orderApiClient;

        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {

            var request = new OrderPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize

            };
            var data = await _orderApiClient.GetPagings(request);
            ViewBag.Keyword = keyword;

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }

        public async Task<IActionResult> Details(int Id, int pageIndex = 1, int pageSize = 10)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var request = new OrderDetailPagingRequest()
            {
                OrderId = Id,
                PageIndex = pageIndex,
                PageSize = pageSize,
                /* LanguageId = languageId*/

            };
            var data = await _orderApiClient.GetOrderDetailPagings(request);
            ViewBag.Keyword = Id;

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }

     
    }
}
