using eShopSolution.AdminApp.ApiIntegration.Interface;
using eShopSolution.ViewModels.Statistical;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.AdminApp.Controllers
{
    public class StatisticalController : Controller
    {
        private readonly IStatisticalApiClient _statisticalApiClient;
        private readonly IConfiguration _configuration;


        public StatisticalController(IStatisticalApiClient statisticalApiClient,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _statisticalApiClient = statisticalApiClient;

        }

        public async Task<IActionResult> Index(DateTime startDate, DateTime endDate, int pageIndex = 1, int pageSize = 10)
        {

            //can phai bat convert sang MM/dd/yyyy NEU KHONG BI LOI 


            var request = new StatisticalRequest()
            {

                StartDate = startDate,
                EndDate = endDate,
                PageIndex = pageIndex,
                PageSize = pageSize,
                /* LanguageId = languageId*/

            };
            var data = await _statisticalApiClient.Statistical(request);


            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }

    }
}
