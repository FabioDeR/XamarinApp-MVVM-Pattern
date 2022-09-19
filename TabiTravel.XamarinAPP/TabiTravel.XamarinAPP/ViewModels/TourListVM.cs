using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinAPP.Views;
using TabiTravel.XamarinModel.Models;
using TabiTravel.XamarinModel.Models.TourModel;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.ViewModels
{
    public class TourListVM : BaseVM
    {

        #region Service Instance
        ITourService TourService;
        #endregion        
        #region Observable Properties
        private ObservableCollection<GetMyTourListVM> getMyTourListVMs;
        public ObservableCollection<GetMyTourListVM> GetMyTourListVMs { get => getMyTourListVMs; set { getMyTourListVMs = value; OnPropertyChanged(nameof(GetMyTourListVMs)); } }
        #endregion
        public TourListVM(INavigation navigation, Page page) : base(navigation, page)
        {
            TourService = DependencyService.Get<ITourService>(DependencyFetchTarget.NewInstance);
            GoTo = new Command(GoToTourOverview);
            GoToDetail = new Command(GoToTourDetail);
            GoToTwo = new Command(GoToTourEdit);
        }

        private void GoToTourEdit(object obj)
        {
            try
            {
                Navigation.PushAsync(new TourEditPage());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private void GoToTourOverview(object obj)
        {
            try
            {
                var itemSelected = obj as GetMyTourListVM;
                Navigation.PushAsync(new TourOverviewPage(itemSelected.TourId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        
        private void GoToTourDetail(object obj)
        {
            try
            {
                var itemSelected = obj as GetMyTourListVM;
                Navigation.PushAsync(new TourDetailPage(itemSelected.TourId,false));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #region Load TourList
        public async Task LoadTourListByUserId()
        {
            try
            {
               GetMyTourListVMs = await TourService.GetTourList(UserId, MotherLanguage);                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion

        public async Task LoadAllTourList()
        {
            try
            {
                var list = await TourService.GetAllTour(MotherLanguage);
                GetMyTourListVMs = new ObservableCollection<GetMyTourListVM>(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async void GetSearchTour(SearchVM searchVM)
        {
            try
            {
                var searchList = await TourService.GetResultResearchVM(searchVM);
                GetMyTourListVMs = new ObservableCollection<GetMyTourListVM>(searchList.GetMyTourListVMs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


    }
}
