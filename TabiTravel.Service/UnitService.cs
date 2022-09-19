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
using Xamarin.Forms;

[assembly: Dependency(typeof(UnitService))]

namespace TabiTravel.Service
{
    public class UnitService : IUnitService
    {
        private HttpClient _httpClient;
        public UnitService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ConfigService.Config.BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        }


        public async Task<ObservableCollection<GetUnitVM>> GetUnits(string languageiso)
        {
            try
            {
               
                string responseContent = await _httpClient.GetStringAsync($"api/Unit/getunitvm/{languageiso}");
                ObservableCollection<GetUnitVM> getUnitVMs = JsonConvert.DeserializeObject<ObservableCollection<GetUnitVM>>(responseContent);
                return getUnitVMs;                    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
