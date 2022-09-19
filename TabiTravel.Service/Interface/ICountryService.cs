using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.Service;
using TabiTravel.XamarinModel.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(CountryService))]
namespace TabiTravel.Service.Interface
{
    public interface ICountryService
    {
        Task<string> GetByIdAndLanguage(int id, string languageiso);
        Task<List<GetCountryVM>> GetCountryVM(string languageiso);
        Task<string> GetTranslateCountry(string countryname, string languageiso);
        Task<HttpResponseMessage> UpdateUserCountry(string countryName, Guid userId);
    }
}
