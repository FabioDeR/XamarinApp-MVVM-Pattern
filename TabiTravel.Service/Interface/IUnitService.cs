using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.Service;
using TabiTravel.XamarinModel.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(UnitService))]

namespace TabiTravel.Service.Interface
{
    public interface IUnitService
    {
        Task<ObservableCollection<GetUnitVM>> GetUnits(string languageiso);
    }
}
