using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.Service;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinModel.Models;
using TabiTravel.XamarinModel.Models.StepModel;
using TabiTravel.XamarinModel.Models.TourModel;
using Xamarin.Forms;

[assembly: Dependency(typeof(TourService))]

namespace TabiTravel.Service
{
    public class TourService : ITourService
    {
        private HttpClient _httpClient;
        public TourService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ConfigService.Config.BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        }

        #region Post Tour
        public async Task<HttpResponseMessage> AddTour(TourEditVM tourEditVM)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(tourEditVM);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("api/Tour/posttourvm", content);
                //var test = response.Content.ReadAsStringAsync().Result;
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        #region Get By id
        public async Task<TourEditVM> GetById(Guid userId, string languageIso, int tourId)
        {
            try
            {
                string responContent = await _httpClient.GetStringAsync($"api/Tour/getedittourvm/{userId}/{languageIso}/{tourId}");
                TourEditVM tourEditVM = JsonConvert.DeserializeObject<TourEditVM>(responContent);
                return tourEditVM;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        #region Get All Category For Tour
        public async Task<List<GetCategoryVM>> GetAllCategoryVMForTour(string languageIso)
        {
            try
            {
                List<GetCategoryVM> getCategoryVMs = new List<GetCategoryVM>();
                string responseContent = await _httpClient.GetStringAsync($"api/Category/getallcategoryvmfortour/{languageIso}");
                getCategoryVMs = JsonConvert.DeserializeObject<List<GetCategoryVM>>(responseContent);
                return getCategoryVMs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        #region Upload Image
        public async Task<string> UploadImageTour(MultipartFormDataContent content)
        {
            try
            {
                var postResult = await _httpClient.PostAsync("api/Image/tourimage", content);
                var postContent = await postResult.Content.ReadAsStringAsync();
                if (!postResult.IsSuccessStatusCode)
                {
                    throw new ApplicationException(postContent);
                }
                else
                {

                    return postContent;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        #region Get TourOverview by Id
        public async Task<StepListOverviewVM> GetTourOverviewById(int tourId, string languageIso)
        {
            try
            {
                StepListOverviewVM stepListOverviewVM = new StepListOverviewVM();
                string responseContent = await _httpClient.GetStringAsync($"api/Step/getsteplistoverviewvm/{tourId}/{languageIso}");
                stepListOverviewVM = JsonConvert.DeserializeObject<StepListOverviewVM>(responseContent);
                return stepListOverviewVM;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        #region Get TourList
        public async Task<ObservableCollection<GetMyTourListVM>> GetTourList(Guid userId,string languageIso)
        {
            try
            {                
                string responseContent = await _httpClient.GetStringAsync($"api/Tour/getmytourlistvm/{userId}/{languageIso}");
                ObservableCollection<GetMyTourListVM> getMyTourListVMs = JsonConvert.DeserializeObject<ObservableCollection<GetMyTourListVM>>(responseContent);
                return getMyTourListVMs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        #region Update
        public async Task<HttpResponseMessage> UpdateTour(TourEditVM tourEditVM)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(tourEditVM);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage responseContent = await _httpClient.PutAsync("api/Tour/puttourvm", content);
                return responseContent;                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
         

        }
        #endregion
        #region NoImage
        public async Task<string> GetTourNoImage()
        {
            try
            {
                var postResult = await _httpClient.GetStringAsync("api/Image/tourimage");
                return postResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        public async Task<ResultSearchVM> GetResultResearchVM(SearchVM researchVM)
        {
            try
            {
                ResultSearchVM resultSearchVM = new ResultSearchVM();
                string jsonData = JsonConvert.SerializeObject(researchVM);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage responseContent = await _httpClient.PostAsync("api/Search/getresultresearchvm", content);
                var result = await responseContent.Content.ReadAsStringAsync();
                resultSearchVM = JsonConvert.DeserializeObject<ResultSearchVM>(result);
                return resultSearchVM;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<GetMyTourListVM>> GetAllTour(string motherLanguage)
        {
            try
            {
                List<GetMyTourListVM> getMyTourListVMs = new List<GetMyTourListVM>();
                string responseContent = await _httpClient.GetStringAsync($"api/Search/getalltourvm/{motherLanguage}");
                getMyTourListVMs = JsonConvert.DeserializeObject<List<GetMyTourListVM>>(responseContent);
                return getMyTourListVMs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
