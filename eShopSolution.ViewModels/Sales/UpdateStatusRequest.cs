using eShopSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.Sales
{
    public class UpdateStatusRequest
    {
        public int OrderId { get; set; }
        public OrderStatus Status { set; get; }
    }
}
