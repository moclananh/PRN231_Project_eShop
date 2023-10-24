using eShopSolution.Application.Sales;
using eShopSolution.ViewModels.Sales;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UserAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(
            IOrderService productService)
        {
            _orderService = productService;
        }

        [HttpPost]


        public async Task<IActionResult> Create(CheckoutRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _orderService.Create(request);
            if (result == null)
            {
                return BadRequest();
            }

            // var product = await _orderService.GetById(productId, request.LanguageId);


            return Ok();
        }
    }
}
