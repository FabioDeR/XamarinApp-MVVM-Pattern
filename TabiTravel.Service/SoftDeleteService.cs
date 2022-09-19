using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.Service;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinModel.Enum;
using Xamarin.Forms;

[assembly: Dependency(typeof(SoftDeleteService))]

namespace TabiTravel.Service
{
    public class SoftDeleteService : ISoftDeleteService
    {
        private HttpClient _httpClient;
        public SoftDeleteService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ConfigService.Config.BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        }

        #region SoftDelete Interest - Step - Tour
        public async Task<HttpResponseMessage> SofDelete(NavigationEnum navigationEnum, int id, Guid userId)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                switch (navigationEnum)
                {
                    case NavigationEnum.InterestEdit:
                        response = await _httpClient.DeleteAsync($"api/Interest/interestdelete/{id}/{userId}");
                        break;
                    case NavigationEnum.StepEdit:
                        response = await _httpClient.DeleteAsync($"api/Step/stepdelete/{id}/{userId}");
                        break;
                    case NavigationEnum.TourEdit:
                        response = await _httpClient.DeleteAsync($"api/Tour/tourdelete/{id}/{userId}");
                        break;
                    case NavigationEnum.TourOverviewEdit:
                        response = await _httpClient.DeleteAsync($"api/Tour/tourdelete/{id}/{userId}");
                        break;                                    
                    default:
                        break;
                }
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        #region SoftDelete Translate Interest - Soft - Tour
        public async Task<HttpResponseMessage> SofDeleteTranslate(EnumTranslate enumTranslate, int id, string languageIso)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                switch (enumTranslate)
                {
                    case EnumTranslate.Interest:
                        response = await _httpClient.DeleteAsync($"api/TranslateInterest/translateinterestdelete/{id}/{languageIso}");
                        break;
                    case EnumTranslate.Step:
                        response = await _httpClient.DeleteAsync($"api/TranslateStep/translatestepdelete/{id}/{languageIso}");
                        break;
                    case EnumTranslate.Tour:
                        response = await _httpClient.DeleteAsync($"api/TranslateTour/translatetourdelete/{id}/{languageIso}");
                        break;
                    default:
                        break;
                }
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
    }
}
