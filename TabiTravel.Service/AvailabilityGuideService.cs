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

[assembly: Dependency(typeof(AvailabilityGuideService))]
namespace TabiTravel.Service
{
    public class AvailabilityGuideService : IAvailabilityGuideService
    {
        private HttpClient _httpClient;
        public AvailabilityGuideService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ConfigService.Config.BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        }

        public async Task<List<DayListVM>> GetAvailabilityGuideVM(Guid userId, string languageIso)
        {
            try
            {
                List<DayListVM> dayListVMs = new List<DayListVM>();
                string responseContent = await _httpClient.GetStringAsync($"api/AvailabilityGuide/getavailabilityguidevm/{userId}/{languageIso}");
                dayListVMs = JsonConvert.DeserializeObject<List<DayListVM>>(responseContent);
                return dayListVMs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<HttpResponseMessage> PutAvailabilityGuideVM(List<DayListVM> dayListVMs)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(dayListVMs);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage res = await _httpClient.PutAsync("api/AvailabilityGuide/putavailabilityguidevm", content);
                return res;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
