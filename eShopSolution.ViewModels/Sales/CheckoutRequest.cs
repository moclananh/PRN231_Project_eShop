﻿using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Sales
{
    public class CheckoutRequest
    {

        //Them id de tìm user
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }



        public List<OrderDetailVm> OrderDetails { set; get; } = new List<OrderDetailVm>();
    }
}