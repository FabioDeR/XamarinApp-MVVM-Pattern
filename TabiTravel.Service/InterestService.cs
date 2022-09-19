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
using TabiTravel.XamarinModel.Models.InterestModel;
using Xamarin.Forms;

[assembly: Dependency(typeof(InterestService))]
namespace TabiTravel.Service
{
    public class InterestService : IInterestService
    {
        private HttpClient _httpClient;
        public InterestService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ConfigService.Config.BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        }

        #region Get ById
        public async Task<AddInterestVM> GetById(string motherLanguage, int interestId)
        {
            try
            {
                string responseContent = await _httpClient.GetStringAsync($"api/Interest/getaddinterestvm/{motherLanguage}/{interestId}");
                var res = JsonConvert.DeserializeObject<AddInterestVM>(responseContent);
                return await Task.FromResult(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        #region Get All Interest
        public async Task<List<Interest>> GetAll()
        {
            try
            {
                List<Interest> listOfInterest = new List<Interest>();
                string responseContent = await _httpClient.GetStringAsync("api/Interest");
                listOfInterest = JsonConvert.DeserializeObject<List<Interest>>(responseContent);
                return await Task.FromResult(listOfInterest);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        #region Get All Interest by UserId
        public async Task<ObservableCollection<GetMyInterestVM>> GetAllInterestByUserId(Guid userId, string motherLanguage)
        {
            try
            {
                string responseContent = await _httpClient.GetStringAsync($"api/Interest/getmyinterestvm/{userId}/{motherLanguage}");
                ObservableCollection<GetMyInterestVM> listOfInterest = JsonConvert.DeserializeObject<ObservableCollection<GetMyInterestVM>>(responseContent);
                return listOfInterest;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        #region Post Interest
        public async Task<HttpResponseMessage> AddInterest(AddInterestVM interest)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(interest);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("api/Interest/postinterestvm", content);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
        #endregion
        #region UploadImageInterest
        public async Task<string> UploadImageInterest(MultipartFormDataContent content)
        {

            try
            {
                var postResult = await _httpClient.PostAsync("api/Image/interestimage", content);
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
        #region Update
        public async Task<HttpResponseMessage> Update(AddInterestVM interest)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(interest);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var res = await _httpClient.PutAsync("api/Interest", content);
                string body = await res.Content.ReadAsStringAsync();
                return res;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        #region Get All Interest
        public async Task<ObservableCollection<GetMyInterestVM>> GetAllInterest(string motherLanguage)
        {
            try
            {
                string responseContent = await _httpClient.GetStringAsync($"api/Search/getallinterestvm/{motherLanguage}");
                ObservableCollection<GetMyInterestVM> listOfInterest = JsonConvert.DeserializeObject<ObservableCollection<GetMyInterestVM>>(responseContent);
                return listOfInterest;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        #region Search Interest
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
        #endregion


        
    }
}
