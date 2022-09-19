using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.Service;
using TabiTravel.XamarinModel.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(CategoryService))]

namespace TabiTravel.Service.Interface
{
    public interface ICategoryService
    {
        Task<List<GetCategoryVM>> GetAllCategoryVMForTour(string languageIso);
        Task<List<GetCategoryVM>> GetCategoryVM(string languageiso);
    }
}
