using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.Service;
using TabiTravel.XamarinModel.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(AvailabilityGuideService))]
namespace TabiTravel.Service.Interface
{
    public interface IAvailabilityGuideService
    {
        Task<HttpResponseMessage> PutAvailabilityGuideVM(List<DayListVM> dayListVMs);
        Task<List<DayListVM>> GetAvailabilityGuideVM(Guid userId, string languageIso);
    }
}
