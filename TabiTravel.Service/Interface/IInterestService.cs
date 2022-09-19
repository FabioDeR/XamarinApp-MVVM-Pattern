using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.Service;
using TabiTravel.XamarinModel.Models;
using TabiTravel.XamarinModel.Models.InterestModel;
using Xamarin.Forms;

[assembly: Dependency(typeof(InterestService))]
namespace TabiTravel.Service.Interface
{
    public interface IInterestService
    {
        Task<List<Interest>> GetAll();
        Task<HttpResponseMessage> AddInterest(AddInterestVM interest);
        Task<string> UploadImageInterest(MultipartFormDataContent content);
        Task<AddInterestVM> GetById(string motherLanguage,int interestId);
        Task<HttpResponseMessage> Update(AddInterestVM interest);       
        Task<ResultSearchVM> GetResultResearchVM(SearchVM researchVM);
        Task<ObservableCollection<GetMyInterestVM>> GetAllInterestByUserId(Guid userId, string motherLanguage);      
        Task<ObservableCollection<GetMyInterestVM>> GetAllInterest(string motherLanguage);
    }
}
