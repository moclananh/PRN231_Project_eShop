using eShopSolution.Data.EF;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.Statistical;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Statistical
{
    public class StatisticalServie : IStatisticalService
    {
        private readonly EShopDbContext _context;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public StatisticalServie(EShopDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<StatisticalVm>> Statistical(StatisticalRequest request)
        {
            var query = from od in _context.OrderDetails
                        join o in _context.Orders on od.OrderId equals o.Id
                        join p in _context.Products on od.ProductId equals p.Id
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pimg in _context.ProductImages on p.Id equals pimg.ProductId
                        where (o.OrderDate >= request.StartDate && o.OrderDate <= request.EndDate)
                        group new { od, pt, pimg } by new
                        {
                            od.ProductId,
                            pt.Name,
                            pimg.ImagePath
                        }
                        into grouped
                        select new StatisticalVm()
                        {
                            ProductId = grouped.Key.ProductId,
                            Name = grouped.Key.Name,
                            ImagePath = grouped.Key.ImagePath,
                            TotalQuantity = grouped.Sum(item => item.od.Quantity),
                            TotalPrice = (float)grouped.Sum(item => item.od.Price)
                        };


            var queryFilter = query
              .Where(item => item.Name != "N/A")
              .OrderByDescending(item => item.TotalQuantity) // Sort by TotalQuantity in descending order
              .ToList();

            int totalRow = queryFilter.Count();

            var data = queryFilter
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var pagedResult = new PagedResult<StatisticalVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }
    }
}
