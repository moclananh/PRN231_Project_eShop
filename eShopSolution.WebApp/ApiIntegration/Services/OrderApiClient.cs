﻿using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Constants;
using eShopSolution.ViewModels.Sales;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using eShopSolution.WebApp.ApiIntegration.Interface;

namespace eShopSolution.WebApp.ApiIntegration.Services
{

    public class OrderApiClient : BaseApiClient, IOrderApiClient
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public OrderApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Order> Create(CheckoutRequest request)
        {
            var sessions = _httpContextAccessor
               .HttpContext
               .Session
               .GetString(SystemConstants.AppSettings.Token);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.UserBaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);


            //////////////////////////////////
            // Serialize the request as JSON
            var requestJson = JsonConvert.SerializeObject(request);
            JObject requestObject = JsonConvert.DeserializeObject<JObject>(requestJson);

            string updatedRequestJson = requestObject.ToString();

            // Create a StringContent object with JSON data and set the content type
            var content = new StringContent(updatedRequestJson, Encoding.UTF8, "application/json");



            //////////////////////////
            var response = await client.PostAsync($"/api/Orders/", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var order = JsonConvert.DeserializeObject<Order>(responseContent);
                return order;
            }
            else
            {
                // Capture and log detailed error information from the response
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("API Error Response Content:");
                Console.WriteLine(responseContent);

                // Throw an exception with the error details
                throw new Exception($"API request failed with status code {response.StatusCode}. Reason: {response.ReasonPhrase}. Response: {responseContent}");
            }
        }
    }
}
