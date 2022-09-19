using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TabiTravel.Service;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinAPP.Views;
using TabiTravel.XamarinModel.Models;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.ViewModels
{
    public class UnavailabilityGuideVM : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        private Page _page;
        private ObservableCollection<UnavailabilityGuideEditVM> unavailabilityGuideList;
        public ObservableCollection<UnavailabilityGuideEditVM> UnavailabilityGuideList { get => unavailabilityGuideList; set { unavailabilityGuideList = value; OnPropertyChanged(nameof(UnavailabilityGuideList)); } }
        UnavailabilityGuideEditVM unavailabilityGuideEditVM;
        public UnavailabilityGuideEditVM UnavailabilityGuideEditVM { get => unavailabilityGuideEditVM; set { unavailabilityGuideEditVM = value; OnPropertyChanged(nameof(UnavailabilityGuideEditVM)); } }
        public ICommand UpdateUnavailabilityGuide { get; }
        public ICommand AddUnavailabilityGuide { get; }        
        public ICommand DeleteUnavailabilityGuide { get; }
        protected int UnavailabilityGuideId { get; set; }
        protected Guid UserId { get; set; }

        IUnavailabilityGuideService UnavailabilityGuideService;

        public UnavailabilityGuideVM(INavigation navigation, Page page)
        {
            Navigation = navigation;
            _page = page;
            UnavailabilityGuideService = DependencyService.Get<IUnavailabilityGuideService>(DependencyFetchTarget.NewInstance);
            UnavailabilityGuideList = new ObservableCollection<UnavailabilityGuideEditVM>();
            UnavailabilityGuideEditVM = new UnavailabilityGuideEditVM();
            UpdateUnavailabilityGuide = new Command(Update);
            AddUnavailabilityGuide = new Command(Add);
            DeleteUnavailabilityGuide = new Command(SoftDelete);
            UserId = App.Database.GetUserId();

        }

        #region LoadUnavailibilityGuideList
        public async Task GetUnavailabilityGuideListByUserId()
        {
            try
            {               
                var unavailabilityGuideList = await UnavailabilityGuideService.GetUnavailabilityGuideByUserId(UserId);
                unavailabilityGuideList.ForEach(e => { _ = e.DateStart.Date.Date; });
                UnavailabilityGuideList = new ObservableCollection<UnavailabilityGuideEditVM>(unavailabilityGuideList);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion

        private async void Add()
        {
            try
            {
                var res = await UnavailabilityGuideService.AddUnavailabilityGuide(UnavailabilityGuideEditVM);
                if (res.IsSuccessStatusCode)
                {
                    Navigation.RemovePage(_page);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }        
        
        private async void Update()
        {
            try
            {
                var res = await UnavailabilityGuideService.UpdateUnavailabilityGuide(UnavailabilityGuideEditVM);
                if (res.IsSuccessStatusCode)
                {
                    Navigation.RemovePage(_page);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async void GetById(int id)
        {
            UnavailabilityGuideEditVM = await UnavailabilityGuideService.GetUnavailabilityGuideById(UserId, id);
            UnavailabilityGuideId = id;
        }

        private async void SoftDelete()
        {
            try
            {
                bool responseDisplay = await _page.DisplayAlert("Warning", "Are you sure you want to delete this item ?", "Ok", "Cancel");
                var res = await UnavailabilityGuideService.SoftDelete(UnavailabilityGuideId, UserId);
                    if (res.IsSuccessStatusCode)
                    {
                        await _page.DisplayAlert("Success", "Item was deleted", "Ok");
                        await Navigation.PushAsync(new UnavailibilityGuideListPage());

                        Navigation.RemovePage(_page);
                    }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
