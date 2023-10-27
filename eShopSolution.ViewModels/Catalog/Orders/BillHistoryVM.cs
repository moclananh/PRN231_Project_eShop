using eShopSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.Catalog.Orders
{
    public class BillHistoryVM
    {
        public int id { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public DateTime orderDate { get; set; }
        public decimal price { get; set; }
        public OrderStatus Status { get; set; }
    }
}
