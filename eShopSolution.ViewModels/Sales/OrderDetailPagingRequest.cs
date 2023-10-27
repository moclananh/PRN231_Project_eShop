using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.Sales
{
    public class OrderDetailPagingRequest : PagingRequestBase
    {
        public int OrderId { get; set; }
        public string LanguageId { get; set; }
    }
}
