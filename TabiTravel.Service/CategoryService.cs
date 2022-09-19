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

[assembly: Dependency(typeof(CategoryService))]

namespace TabiTravel.Service
{
    public class CategoryService : ICategoryService
    {
        private HttpClient _httpClient;
        public CategoryService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ConfigService.Config.BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<GetCategoryVM>> GetCategoryVM(string languageiso)
        {
            try
            {
                List<GetCategoryVM> getCategoryVMs = new List<GetCategoryVM>();
                string responseContent = await _httpClient.GetStringAsync($"api/Category/getallcategoryvmforinterest/{languageiso}");
                getCategoryVMs = JsonConvert.DeserializeObject<List<GetCategoryVM>>(responseContent);
                return getCategoryVMs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

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

    }
}
