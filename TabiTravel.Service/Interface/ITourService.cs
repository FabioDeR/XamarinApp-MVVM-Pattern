using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.Service;
using TabiTravel.XamarinModel.Models;
using TabiTravel.XamarinModel.Models.StepModel;
using TabiTravel.XamarinModel.Models.TourModel;
using Xamarin.Forms;

[assembly: Dependency(typeof(TourService))]

namespace TabiTravel.Service.Interface
{
    public interface ITourService
    {
        Task<HttpResponseMessage> AddTour(TourEditVM tourEditVM);
        Task<List<GetCategoryVM>> GetAllCategoryVMForTour(string languageIso);
        Task<TourEditVM> GetById(Guid userId, string languageIso, int tourId);
        Task<ObservableCollection<GetMyTourListVM>> GetTourList(Guid userId, string languageIso);
        Task<StepListOverviewVM> GetTourOverviewById(int tourId, string languageIso);
        Task<string> UploadImageTour(MultipartFormDataContent content);
        Task<HttpResponseMessage> UpdateTour(TourEditVM tourEditVM);
        Task<ResultSearchVM> GetResultResearchVM(SearchVM researchVM);
        Task<string> GetTourNoImage();
        Task<List<GetMyTourListVM>> GetAllTour(string motherLanguage);
    }
}
