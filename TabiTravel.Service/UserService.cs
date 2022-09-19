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

[assembly: Dependency(typeof(UserService))]
namespace TabiTravel.Service
{
    public class UserService : IUserService
    {
        private HttpClient _httpClient;
        public UserService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ConfigService.Config.BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        #region GetById
        public async Task<ProfilEditVM> GetById(Guid id, string motherLanguage)
        {
            try
            {
                ProfilEditVM user = new ProfilEditVM();
                string responseContent = await _httpClient.GetStringAsync($"api/User/getprofileditvm/{id}/{motherLanguage}");
                user = JsonConvert.DeserializeObject<ProfilEditVM>(responseContent);
                return await Task.FromResult(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<string> GetMotherLanguage(string userId)
        {
            try
            {
                string responseContent = await _httpClient.GetStringAsync($"api/UserHasLanguage/getmotherlanguage/{userId}");
                return JsonConvert.DeserializeObject<string>(responseContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<HttpResponseMessage> UpdateUser(ProfilEditVM user)
        {
            try
            {               
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync("api/User/putprofileditvm", content);
                string body = await response.Content.ReadAsStringAsync();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<string> UploadImageUser(MultipartFormDataContent content,Guid userId)
        {
            try
            {
                var postResult = await _httpClient.PostAsync($"api/Image/userimage?UserId={userId}", content);
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
    }
}

