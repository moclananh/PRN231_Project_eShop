using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.Statistical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Statistical
{
    public interface IStatisticalService
    {
        Task<PagedResult<StatisticalVm>> Statistical(StatisticalRequest request);
    }
}
