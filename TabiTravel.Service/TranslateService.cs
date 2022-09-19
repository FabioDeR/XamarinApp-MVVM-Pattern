using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.Service;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinModel.Enum;
using TabiTravel.XamarinModel.Models.TranslationModel;
using Xamarin.Forms;

[assembly: Dependency(typeof(TranslateService))]

namespace TabiTravel.Service
{
    public class TranslateService : ITranslateService
    {
        private HttpClient _httpClient;
        public TranslateService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ConfigService.Config.BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<TranslateVM>> GetTranslateVMs(int id, EnumTranslate enumTranslate,string motherLanguage)
        {
            try
            {
                List<TranslateVM> translateVMs = new List<TranslateVM>();
                string responseContent = await _httpClient.GetStringAsync($"api/TranslateInterest/getalltranslatevm/{id}/{(int)enumTranslate}/{motherLanguage}");
                translateVMs = JsonConvert.DeserializeObject<List<TranslateVM>>(responseContent);                
                return translateVMs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<HttpResponseMessage> PostTranslateVM(TranslateVM translateVM, EnumTranslate enumTranslate)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(translateVM);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"api/TranslateInterest/posttranslatevm/{(int)enumTranslate}", content);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task<TranslateVM> GetTranslateVM(int idx, EnumTranslate enumTranslate, string languageiso)
        {
            try
            {
                string responseContent = await _httpClient.GetStringAsync($"api/TranslateInterest/gettranslatevm/{idx}/{((int)enumTranslate)}/{languageiso}");
                TranslateVM translateVM = JsonConvert.DeserializeObject<TranslateVM>(responseContent);
                return translateVM;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<HttpResponseMessage> UpdateTranslate(TranslateVM translateVM)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(translateVM);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync($"api/TranslateInterest/puttranslatevm", content);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
