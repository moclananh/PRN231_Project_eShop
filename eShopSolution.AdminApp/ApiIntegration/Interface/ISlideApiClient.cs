using eShopSolution.ViewModels.Utilities.Slides;

namespace eShopSolution.AdminApp.ApiIntegration.Interface
{
    public interface ISlideApiClient
    {
        Task<List<SlideVm>> GetAll();
    }
}
