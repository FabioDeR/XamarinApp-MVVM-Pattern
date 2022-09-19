using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.Service;
using TabiTravel.XamarinModel.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(UserService))]

namespace TabiTravel.Service.Interface
{
    public interface IUserService
    {
        Task<ProfilEditVM> GetById(Guid id,string motherLanguage);       
        Task<HttpResponseMessage> UpdateUser(ProfilEditVM user);
        Task<string> GetMotherLanguage(string v);
        Task<string> UploadImageUser(MultipartFormDataContent content,Guid userId);
    }
}
