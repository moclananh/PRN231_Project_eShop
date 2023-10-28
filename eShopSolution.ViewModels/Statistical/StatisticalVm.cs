using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.Statistical
{
    public class StatisticalVm
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int TotalQuantity { get; set; }
        public float TotalPrice { get; set; }

    }
}
