using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopSolution.Application.Catalog.Products;
using eShopSolution.Application.Sales;
using eShopSolution.ViewModels.Catalog.Categories;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Sales;
using eShopSolution.ViewModels.Statistical;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
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

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] OrderPagingRequest request)
        {
            var categories = await _orderService.GetAllPaging(request);
            return Ok(categories);
        }

        [HttpGet("GetOrderDetailPagingRequest")]
        public async Task<IActionResult> GetOrderDetailPagingRequest([FromQuery] OrderDetailPagingRequest request)
        {
            var categories = await _orderService.GetOrderDetailPagingRequest(request);
            return Ok(categories);
        }

       
    }
}
