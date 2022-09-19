using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.Service;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinModel.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(CountryService))]
namespace TabiTravel.Service
{
    public class CountryService : ICountryService
    {
        private HttpClient _httpClient;
        public CountryService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ConfigService.Config.BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<GetCountryVM>> GetCountryVM(string languageiso)
        {
            try
            {
                List<GetCountryVM> getCountryVMs = new List<GetCountryVM>();
                string responseContent = await _httpClient.GetStringAsync($"api/Country/getcountryvm/{languageiso}");
                getCountryVMs = JsonConvert.DeserializeObject<List<GetCountryVM>>(responseContent);
                return getCountryVMs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<string> GetByIdAndLanguage(int id, string languageiso)
        {
            try
            {
                string responseContent = await _httpClient.GetStringAsync($"api/Country/{id}/{languageiso}");
                Country countryName = JsonConvert.DeserializeObject<Country>(responseContent);
                return countryName.Name;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<string> GetTranslateCountry(string countryname, string languageiso)
        {
            try
            {
                string responseContent = await _httpClient.GetStringAsync($"api/Country/gettranslatecountry/{countryname}/{languageiso}");
                string countryTrad = JsonConvert.DeserializeObject<string>(responseContent);
                return countryTrad;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<HttpResponseMessage> UpdateUserCountry(string countryName, Guid userId )
        {
            try
            {
                
                HttpResponseMessage responseContent = await _httpClient.GetAsync($"api/User/putcountry/{countryName}/{userId}");
                return responseContent;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
