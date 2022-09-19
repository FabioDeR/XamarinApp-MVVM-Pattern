using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.Service;
using TabiTravel.XamarinModel.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(UnavailabilityGuideService))]
namespace TabiTravel.Service.Interface
{
    public interface IUnavailabilityGuideService
    {
        Task<List<UnavailabilityGuideEditVM>> GetUnavailabilityGuideByUserId(Guid userId);
        Task<HttpResponseMessage> UpdateUnavailabilityGuide(UnavailabilityGuideEditVM unavailabilityGuideEditVM);
        Task<HttpResponseMessage> AddUnavailabilityGuide(UnavailabilityGuideEditVM unavailabilityGuideEditVM);
        Task<UnavailabilityGuideEditVM> GetUnavailabilityGuideById(Guid userId, int unavailabilityGuideId);
        Task<HttpResponseMessage> SoftDelete(int unavailabilityGuideId, Guid userId);
    }
}
