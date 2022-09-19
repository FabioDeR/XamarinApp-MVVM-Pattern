using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.XamarinModel.Enum;
using TabiTravel.XamarinModel.Models.TranslationModel;

namespace TabiTravel.Service.Interface
{
    public interface ITranslateService
    {
        Task<TranslateVM> GetTranslateVM(int idx, EnumTranslate enumTranslate, string languageiso);
        Task<List<TranslateVM>> GetTranslateVMs(int id, EnumTranslate enumTranslate, string motherLanguage);
        Task<HttpResponseMessage> PostTranslateVM(TranslateVM translateVM, EnumTranslate enumTranslate);
        Task<HttpResponseMessage> UpdateTranslate(TranslateVM translateVM);
    }
}
