using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public class StepVM : BaseVM
    {
        private StepEditVM stepEdit;
        public StepEditVM StepEdit { get => stepEdit; set { stepEdit = value; OnPropertyChanged(nameof(StepEdit)); } }
        #region Service Instance
        IInterestService InterestService;
        IUnitService UnitService;
        IStepService StepService;
        #endregion
        #region Observable Properties
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
                    OnPropertyChanged(nameof(_unitSelected));
                }
            }
        }
        #endregion

        public StepVM(INavigation navigation, Page page) : base(navigation, page)
        {
            InterestService = DependencyService.Get<IInterestService>(DependencyFetchTarget.NewInstance);
            UnitService = DependencyService.Get<IUnitService>(DependencyFetchTarget.NewInstance);
            StepService = DependencyService.Get<IStepService>(DependencyFetchTarget.NewInstance);
            Interests = new ObservableCollection<GetMyInterestVM>();
            Units = new ObservableCollection<GetUnitVM>();
            Save = new Command(PostStep);
            StepEdit = new StepEditVM();

        }
        private async void PostStep()
        {
            try
            {
                if (InterestSelected != null)
                {
                    StepEdit.InterestId = InterestSelected.InterestId;

                }
                if (UnitSelected != null)
                {
                    StepEdit.UnitId = UnitSelected.Id;
                }
                if(StepEdit.PreviousStep == null || StepEdit.PreviousStep == 0)
                {
                    StepEdit.UnitId = 0;
                }
                StepEdit.LanguageIso = MotherLanguage;
                if (!ValidationHelper.IsFormValid(StepEdit, Page)) { return; }
                var res = await StepService.AddStep(StepEdit);
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
        #region Load My Unit
        public async Task LoadUnit()
        {
            try
            {
                var list = await UnitService.GetUnits(MotherLanguage);
                Units = new ObservableCollection<GetUnitVM>(list);
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
