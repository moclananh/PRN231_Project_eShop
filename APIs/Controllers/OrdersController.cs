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


        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetById(int orderId)
        {
            var product = await _orderService.GetById(orderId);
            if (product == null)
                return BadRequest("Cannot find Order");
            return Ok(product);
        }

        [HttpPut("{orderId}")]
        [Consumes("multipart/form-data")]
        //[Authorize]
        public async Task<IActionResult> UpdateStatus([FromRoute] int orderId, [FromForm] UpdateStatusRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            request.OrderId = orderId;
            var affectedResult = await _orderService.UpdateStatus(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

    }
}
