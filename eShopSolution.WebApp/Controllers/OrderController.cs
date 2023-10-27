using eShopSolution.ViewModels.Catalog.Orders;
using eShopSolution.WebApp.ApiIntegration.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eShopSolution.WebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderApiClient _orderApiClient;
        private readonly IConfiguration _configuration;
        public OrderController(IOrderApiClient orderApiClient, IConfiguration configuration)
        {
            _orderApiClient = orderApiClient;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult>GetBillHistory()
        {
            var id = Guid.Parse("69bd714f-9576-45ba-b5b7-f00649be00de");
            var userId = User.FindFirstValue(ClaimTypes.Dsa);
            if (userId != null)
            {
                id = Guid.Parse(userId);
            }
            var result = await _orderApiClient.GetBillHistory(id);
            return View(result);
        }
        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> GetBillDetail(int id)
        {
            var result = await _orderApiClient.BillDetail(id);
            ViewBag.TotalSum = result.price.Zip(result.quantity, (p, q) => p * q).Sum();
            return View(result);
        }


        [HttpGet]
        public async Task<IActionResult> SearchBillById(int id)
        {
            var result = await _orderApiClient.BillDetail(id);
            return View(result);
        }
    }
}
