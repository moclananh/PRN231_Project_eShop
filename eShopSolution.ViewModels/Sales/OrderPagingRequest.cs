using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ViewModels.Sales
{
    public class OrderPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    /*    public string LanguageId { get; set; }*/ // ve sau xu ly get all order by languageId
    }
}
