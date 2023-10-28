using eShopSolution.Application.Sales;
using eShopSolution.Application.Statistical;
using eShopSolution.ViewModels.Statistical;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticalController : ControllerBase
    {
        private readonly IStatisticalService _statisticalService;

        public StatisticalController(
            IStatisticalService statiticalService)
        {
            _statisticalService = statiticalService;
        }

        [HttpGet]
        public async Task<IActionResult> Statistical([FromQuery] StatisticalRequest request)
        {
            var statistical = await _statisticalService.Statistical(request);
            return Ok(statistical);
        }
    }
}
