using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinAPP.Views;
using TabiTravel.XamarinModel.Models.StepModel;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.ViewModels
{
    public class StepDetailVM : BaseVM
    {

        #region InstanceService
        IStepService StepService;
        #endregion
        private GetMyStepVM getMyStepVM;
        public GetMyStepVM GetMyStepVM { get => getMyStepVM; set { getMyStepVM = value; OnPropertyChanged(nameof(GetMyStepVM)); } }

        public StepDetailVM(INavigation navigation, Page page) : base(navigation, page)
        {
            GoTo = new Command(GoToInterestDetail);
            StepService = DependencyService.Get<IStepService>(DependencyFetchTarget.NewInstance);
            GetMyStepVM = new GetMyStepVM();
        }

        private async void GoToInterestDetail()
        {
            try
            {
                await Navigation.PushAsync(new PoiDetailPage(GetMyStepVM.InterestId,false));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task GetMyStepbyId(int stepId)
        {
            try
            {
                GetMyStepVM = await StepService.GetMyStepById(stepId, MotherLanguage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
