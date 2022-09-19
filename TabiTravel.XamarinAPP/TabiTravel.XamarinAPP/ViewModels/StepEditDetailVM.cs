using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinAPP.Views;
using TabiTravel.XamarinModel.Models;
using TabiTravel.XamarinModel.Models.InterestModel;
using TabiTravel.XamarinModel.Models.StepModel;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.ViewModels
{
    public class StepEditDetailVM : BaseVM
    {


        #region Service Instance
        IInterestService InterestService;
        IUnitService UnitService;
        IStepService StepService;
        #endregion
        #region List Instance
        private ObservableCollection<GetMyInterestVM> interests;
        public ObservableCollection<GetMyInterestVM> Interests
        {
            get => interests;
            set
            {
                interests = value;
                OnPropertyChanged(nameof(Interests));
            }
        }
        private ObservableCollection<GetUnitVM> untis;
        public ObservableCollection<GetUnitVM> Units
        {
            get => untis;
            set
            {
                untis = value;
                OnPropertyChanged(nameof(Units));
            }
        }
        #endregion
        #region Selected Properties
        private GetMyInterestVM _interestSelected;
        public GetMyInterestVM InterestSelected
        {
            get { return _interestSelected; }
            set
            {
                if (_interestSelected != value)
                {
                    _interestSelected = value;
                    OnPropertyChanged(nameof(InterestSelected));
                }
            }
        }
        private GetUnitVM _unitSelected;
        public GetUnitVM UnitSelected
        {
            get { return _unitSelected; }
            set
            {
                if (_unitSelected != value)
                {
                    _unitSelected = value;
                    OnPropertyChanged(nameof(UnitSelected));
                }
            }
        }
        #endregion     
        private int stepId;
        public int StepId
        {
            get => stepId;
            set
            {
                stepId = value;
                OnPropertyChanged(nameof(Interest));
            }

        }
        private StepEditVM stepEdit;
        public StepEditVM StepEdit { get => stepEdit; set { stepEdit = value; OnPropertyChanged(nameof(StepEdit)); } }

        public StepEditDetailVM(INavigation navigation, Page page, int stepId) : base(navigation, page)
        {
            InterestService = DependencyService.Get<IInterestService>(DependencyFetchTarget.NewInstance);
            UnitService = DependencyService.Get<IUnitService>(DependencyFetchTarget.NewInstance);
            StepService = DependencyService.Get<IStepService>(DependencyFetchTarget.NewInstance);
            Interests = new ObservableCollection<GetMyInterestVM>();
            Units = new ObservableCollection<GetUnitVM>();
            Update = new Command(UpdateStep);
            StepId = stepId;
            StepEdit = new StepEditVM();
        }

        #region Get Step By Id
        public async void GetStepId()
        {
            try
            {
                StepEdit = await StepService.GetStepById(StepId, MotherLanguage, UserId);
                LoadUnit();
                LoadInterestList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        #region Load My Unit
        public async void LoadUnit()
        {
            try
            {
              
                Units = await UnitService.GetUnits(MotherLanguage);
                if (StepEdit.UnitId > 0)
                {
                    UnitSelected = Units.Where(i => i.Id == StepEdit.UnitId).First();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        #region Load My Interest
        public async void LoadInterestList()
        {
            try
            {
                Interests = await InterestService.GetAllInterestByUserId(UserId, MotherLanguage);               
                InterestSelected = Interests.Where(i => i.InterestId == StepEdit.InterestId).First();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        #region Update Step
        private async void UpdateStep(object obj)
        {
            try
            {
                if (!ValidationHelper.IsFormValid(StepEdit, Page)) { return; }
                StepEdit.InterestId = InterestSelected.InterestId;
                if (UnitSelected != null)
                {
                    StepEdit.UnitId = UnitSelected.Id;

                }
                if (StepEdit.PreviousStep == null || StepEdit.PreviousStep == 0)
                {
                    StepEdit.UnitId = 0;
                }
                var res = await StepService.UpdateStep(StepEdit);
                if (res.IsSuccessStatusCode)
                {
                    await Navigation.PushAsync(new TourOverviewPage(StepEdit.TourId));
                    Navigation.RemovePage(Page);
                }
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
