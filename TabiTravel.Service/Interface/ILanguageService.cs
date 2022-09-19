using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.Service;
using TabiTravel.XamarinModel.Enum;
using TabiTravel.XamarinModel.Models;
using TabiTravel.XamarinModel.Models.TranslationModel;
using Xamarin.Forms;

[assembly: Dependency(typeof(LanguageService))]

namespace TabiTravel.Service.Interface
{
    public interface ILanguageService
    {
        Task<HttpResponseMessage> AddTranslation(AddTranslateInterestVM addTranslateInterestVM);
        Task<List<string>> GetAllLanguage();     
        Task<List<PreferedLanguageVM>> GetLanguageApp(Guid userId);
        Task<List<string>> GetLanguageNotUsed(int interestId, Guid userId, EnumTranslate enumTranslate);
        Task<HttpResponseMessage> UpdateLanguageApp(List<PreferedLanguageVM> preferedLanguageVMs);


        
    }
}
