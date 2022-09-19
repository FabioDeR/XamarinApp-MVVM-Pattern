using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.XamarinAPP.Views;
using TabiTravel.XamarinModel.Models;
using TabiTravel.XamarinModel.Models.InterestModel;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.ViewModels.POI
{
    public class InterestListPageVM : BasePOIVM
    {
        private ObservableCollection<GetMyInterestVM> interests;
        public ObservableCollection<GetMyInterestVM> Interests { get => interests; set { interests = value; OnPropertyChanged(nameof(Interests)); } }

        public InterestListPageVM(INavigation navigation, Page page) : base(navigation, page)
        {
            Interests = new ObservableCollection<GetMyInterestVM>();
            GoTo = new Command(GoToInterestDetail);
            GoToTwo = new Command(GoToMapPage);
        }

        private void GoToMapPage(object obj)
        {
            try
            {
                Navigation.PushAsync(new MapPage());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private void GoToInterestDetail(object obj)
        {
            try
            {
                var itemSelected = obj as GetMyInterestVM;
                if (itemSelected.UserId == UserId)
                {
                    Navigation.PushAsync(new PoiDetailPage(itemSelected.InterestId, true));
                }
                else
                {
                    Navigation.PushAsync(new PoiDetailPage(itemSelected.InterestId, false));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #region Load Interest By UserId
        public async Task GetAllInterestByUserId()
        {
            try
            {
                Interests = await InterestService.GetAllInterestByUserId(UserId, MotherLanguage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        #region Load Interets with Search
        public async Task GetSearchInterest(SearchVM searchVM)
        {
            try
            {
                var searchList = await InterestService.GetResultResearchVM(searchVM);
                Interests = new ObservableCollection<GetMyInterestVM>(searchList.GetMyInterestVMs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        #region Load All Interest
        public async Task GetAllInterest()
        {
            try
            {
                Interests = await InterestService.GetAllInterest(MotherLanguage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion



    }
}
