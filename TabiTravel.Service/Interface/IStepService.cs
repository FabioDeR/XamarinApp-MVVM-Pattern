using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.Service;
using TabiTravel.XamarinModel.Models.StepModel;
using Xamarin.Forms;

[assembly: Dependency(typeof(StepService))]

namespace TabiTravel.Service.Interface
{
    public interface IStepService
    {
        Task<HttpResponseMessage> AddStep(StepEditVM stepEditVM);
        Task<GetMyStepVM> GetMyStepById(int stepid, string languageiso);
        Task<StepEditVM> GetStepById(int stepid, string languageiso, Guid userid);
        Task<HttpResponseMessage> UpdateStep(StepEditVM stepEditVM);
    }
}
