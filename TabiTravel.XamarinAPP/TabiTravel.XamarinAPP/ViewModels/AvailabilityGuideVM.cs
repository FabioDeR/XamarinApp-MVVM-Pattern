using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TabiTravel.Service;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinModel.Models;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.ViewModels
{
    public class AvailabilityGuideVM : BaseVM
    {
        IAvailabilityGuideService AvailabilityGuideService;

        private ObservableCollection<DayListVM> dayList;
        public ObservableCollection<DayListVM> DayList { get => dayList; set { dayList = value; OnPropertyChanged(nameof(DayList)); } }

        public AvailabilityGuideVM(INavigation navigation, Page page) : base(navigation, page)
        {
            AvailabilityGuideService = DependencyService.Get<IAvailabilityGuideService>(DependencyFetchTarget.NewInstance);
            Update = new Command(UpdateAvailabilityGuide);
        }

        private async void UpdateAvailabilityGuide(object obj)
        {
            try
            {
                var list = new List<DayListVM>(dayList);
                var res = await AvailabilityGuideService.PutAvailabilityGuideVM(list);
                if (res.IsSuccessStatusCode)
                {
                    await Page.DisplayAlert("Success", "Item updated !", "Ok");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task GetAvailabilityGuideListByUserId()
        {
            try
            {
                var list = await AvailabilityGuideService.GetAvailabilityGuideVM(UserId, MotherLanguage);
                DayList = new ObservableCollection<DayListVM>(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
