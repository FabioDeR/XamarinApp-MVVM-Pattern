using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinAPP.Views;
using TabiTravel.XamarinModel.Models.StepModel;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.ViewModels
{
    public class TourOverviewVM : BaseVM
    {
        #region TourOverview Properties
        private StepListOverviewVM stepListOverviewVM;
        public StepListOverviewVM StepListOverviewVM { get => stepListOverviewVM; set { stepListOverviewVM = value; OnPropertyChanged(nameof(StepListOverviewVM)); } }
        #endregion

        ITourService TourService;
        public ICommand GoToDetail { get; }
        public ICommand GoStep { get; }
        public ICommand Add { get; }
        public ICommand Edit { get; }

        public TourOverviewVM(INavigation navigation, Page page) : base(navigation, page)
        {
            TourService = DependencyService.Get<ITourService>(DependencyFetchTarget.NewInstance);
            GoStep = new Command(GoToStep);
            Add = new Command(AddDayToStep);
            Edit = new Command(EditStep);
            GoToDetail = new Command(GoToStepDeTail);
        }

        private async void GoToStepDeTail(object obj)
        {
            var selectedItem = obj as StepEditVM;
            await Navigation.PushAsync(new StepDetailPage(selectedItem.StepId,stepListOverviewVM.TourId ));
        }

        private async void EditStep(object obj)
        {
            try
            {
                var selectedItem = obj as StepEditVM;
                await Navigation.PushAsync(new StepEditPage(selectedItem.StepId));
                Navigation.RemovePage(Page);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        private async void GoToStep(object obj)
        {
            try
            {
                var selectedItem = obj as DayVM;
                await Navigation.PushAsync(new StepEditPage(stepListOverviewVM.TourId, selectedItem.Day));
                Navigation.RemovePage(Page);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private async void AddDayToStep()
        {
            try
            {

                await Navigation.PushAsync(new StepEditPage(stepListOverviewVM.TourId, (stepListOverviewVM.NbDays + 1)));
                Navigation.RemovePage(Page);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task LoadTourOverview(int tourId)
        {
            try
            {
                StepListOverviewVM = await TourService.GetTourOverviewById(tourId, MotherLanguage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


    }
}
