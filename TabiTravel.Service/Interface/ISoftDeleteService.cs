using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.Service;
using TabiTravel.XamarinModel.Enum;
using Xamarin.Forms;

[assembly: Dependency(typeof(SoftDeleteService))]

namespace TabiTravel.Service.Interface
{
    public interface ISoftDeleteService
    {
        Task<HttpResponseMessage> SofDelete(NavigationEnum navigationEnum, int id, Guid userId);
        Task<HttpResponseMessage> SofDeleteTranslate(EnumTranslate enumTranslate, int id,string languageIso);
    }
}
