using eShopSolution.ViewModels.Utilities.Slides;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopSolution.WebApp.ApiIntegration.Interface
{
    public interface ISlideApiClient
    {
        Task<List<SlideVm>> GetAll();
    }
}
