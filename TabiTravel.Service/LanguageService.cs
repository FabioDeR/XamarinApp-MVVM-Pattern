using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.Service;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinModel.Enum;
using TabiTravel.XamarinModel.Models;
using TabiTravel.XamarinModel.Models.TranslationModel;
using Xamarin.Forms;

[assembly: Dependency(typeof(LanguageService))]

namespace TabiTravel.Service
{
    public class LanguageService : ILanguageService
    {
        private HttpClient _httpClient;
        public LanguageService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ConfigService.Config.BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        }
        public async Task<List<string>> GetLanguageNotUsed(int id, Guid userId, EnumTranslate enumTranslate)
        {
            try
            {
                List<string> languageNotUsed = new List<string>();
                string responseContent = await _httpClient.GetStringAsync($"api/TranslateInterest/getlanguagenotused/{id}/{userId}/{(int)enumTranslate}");
                languageNotUsed = JsonConvert.DeserializeObject<List<string>>(responseContent);
                return await Task.FromResult(languageNotUsed);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<HttpResponseMessage> AddTranslation(AddTranslateInterestVM addTranslateInterestVM)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(addTranslateInterestVM);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("api/TranslateInterest/posttranslateinterestvm", content);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<string>> GetAllLanguage()
        {
            try
            {
                List<string> listOfLanguage = new List<string>();
                string responseContent = await _httpClient.GetStringAsync($"api/Language/getlanguagesstring");
                listOfLanguage = JsonConvert.DeserializeObject<List<string>>(responseContent);
                return await Task.FromResult(listOfLanguage);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task<HttpResponseMessage> UpdateLanguageApp(List<PreferedLanguageVM> preferedLanguageVMs)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(preferedLanguageVMs);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync("api/User/putpreferedlanguagevm", content);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task<List<PreferedLanguageVM>> GetLanguageApp(Guid userId)
        {
            try
            {
                List<PreferedLanguageVM> preferedLanguageVMs = new List<PreferedLanguageVM>();
                string responseContent = await _httpClient.GetStringAsync($"api/User/getpreferedlanguagevm/{userId}");
                preferedLanguageVMs = JsonConvert.DeserializeObject<List<PreferedLanguageVM>>(responseContent);
                return preferedLanguageVMs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


    }
}
