using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.Service;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinModel.Models.StepModel;
using Xamarin.Forms;

[assembly: Dependency(typeof(StepService))]
namespace TabiTravel.Service
{
    public class StepService : IStepService
    {
        private HttpClient _httpClient;
        public StepService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ConfigService.Config.BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        }

        #region Post Step
        public async Task<HttpResponseMessage> AddStep(StepEditVM stepEditVM)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(stepEditVM);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("api/Step/poststepvm", content);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        #region Get Step By Id
        public async Task<StepEditVM> GetStepById(int stepid, string languageiso, Guid userid)
        {
            try
            {
                StepEditVM stepEditVM = new StepEditVM();
                string responseContent = await _httpClient.GetStringAsync($"api/Step/getstepeditvm/{stepid}/{languageiso}/{userid}");
                stepEditVM = JsonConvert.DeserializeObject<StepEditVM>(responseContent);
                return stepEditVM;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        #region Update Service 
        public async Task<HttpResponseMessage> UpdateStep(StepEditVM stepEditVM)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(stepEditVM);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync("api/Step/putstepvm", content);
                string bodyerror = await response.Content.ReadAsStringAsync();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        #region Get My StepID
        public async Task<GetMyStepVM> GetMyStepById(int stepid, string languageiso)
        {
            try
            {
                GetMyStepVM getMyStepVM = new GetMyStepVM();
                string responseContent = await _httpClient.GetStringAsync($"api/Step/getmystepvm/{stepid}/{languageiso}");               
                getMyStepVM = JsonConvert.DeserializeObject<GetMyStepVM>(responseContent);
                return getMyStepVM;
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
