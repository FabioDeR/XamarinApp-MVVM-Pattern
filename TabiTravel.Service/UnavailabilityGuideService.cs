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

[assembly: Dependency(typeof(UnavailabilityGuideService))]
namespace TabiTravel.Service
{

    public class UnavailabilityGuideService : IUnavailabilityGuideService
    {
        private HttpClient _httpClient;
        public UnavailabilityGuideService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ConfigService.Config.BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        }

        public async Task<HttpResponseMessage> AddUnavailabilityGuide(UnavailabilityGuideEditVM unavailabilityGuideEditVM)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(unavailabilityGuideEditVM);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("api/UnavailabilityGuide/postunavailabilityguide", content);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<UnavailabilityGuideEditVM> GetUnavailabilityGuideById(Guid userId, int unavailabilityGuideId)
        {
            try
            {
                UnavailabilityGuideEditVM unavailabilityGuideEditVM = new UnavailabilityGuideEditVM();
                string responseContent = await _httpClient.GetStringAsync($"api/UnavailabilityGuide/getunavailabilityguideeditvm/{userId}/{unavailabilityGuideId}");
                unavailabilityGuideEditVM = JsonConvert.DeserializeObject<UnavailabilityGuideEditVM>(responseContent);
                return unavailabilityGuideEditVM;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<UnavailabilityGuideEditVM>> GetUnavailabilityGuideByUserId(Guid userId)
        {
            try
            {
                List<UnavailabilityGuideEditVM> getUnavailabilityGuideVMs = new List<UnavailabilityGuideEditVM>();
                string responseContent = await _httpClient.GetStringAsync($"api/UnavailabilityGuide/getlistoverviewunavailabilityguidevm/{userId}");
                getUnavailabilityGuideVMs = JsonConvert.DeserializeObject<List<UnavailabilityGuideEditVM>>(responseContent);
                return getUnavailabilityGuideVMs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<HttpResponseMessage> SoftDelete(int unavailabilityGuideId, Guid userId)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                response = await _httpClient.DeleteAsync($"api/UnavailabilityGuide/unavailabilityguidedelete/{unavailabilityGuideId}/{userId}");

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<HttpResponseMessage> UpdateUnavailabilityGuide(UnavailabilityGuideEditVM unavailabilityGuideEditVM)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(unavailabilityGuideEditVM);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var res = await _httpClient.PutAsync("api/UnavailabilityGuide/putunavailabilityguidevm", content);
                string body = await res.Content.ReadAsStringAsync();
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
