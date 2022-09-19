using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Text;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinAPP.Views;
using TabiTravel.XamarinModel.Models.BookingModel;
using TabiTravel.XamarinModel.Models.StepModel;
using TabiTravel.XamarinModel.Models.TourModel;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.ViewModels.Booking
{
    public class PreBookingPageVM : BaseBookingVM
    {
        private StepListOverviewVM stepListOverviewVM;
        public StepListOverviewVM StepListOverviewVM { get => stepListOverviewVM; set { stepListOverviewVM = value; OnPropertyChanged(nameof(StepListOverviewVM)); } }
        private readonly ITourService TourService;
        private int TourId;
        private PreBookingVM preBookingVM;
        public PreBookingVM PreBookingVM { get => preBookingVM; set { preBookingVM = value; OnPropertyChanged(nameof(PreBookingVM)); } }
        private int adultNumber;
        public int AdultNumber { get => adultNumber; set { adultNumber = value; OnPropertyChanged(nameof(AdultNumber)); } }
        private int childNumber;
        public int ChildNumber { get => childNumber; set { childNumber = value; OnPropertyChanged(nameof(ChildNumber)); } }
        private int emplacementNumber;
        public int EmplacementNumber { get => emplacementNumber; set { emplacementNumber = value; OnPropertyChanged(nameof(EmplacementNumber)); } }

        private ObservableCollection<string> errorMessages;
        public ObservableCollection<string> ErrorMessages { get => errorMessages; set { errorMessages = value;OnPropertyChanged(nameof(ErrorMessages)); } }

        private bool isErrorListVisible;
        public bool IsErrorListVisible { get => isErrorListVisible; set { isErrorListVisible = value;OnPropertyChanged(nameof(IsErrorListVisible)); } }

        public PreBookingPageVM(INavigation navigation, Page page, int tourId) : base(navigation, page)
        {
            TourId = tourId;
            TourService = DependencyService.Get<ITourService>(DependencyFetchTarget.NewInstance);
            Post = new Command(PostPreBooking);
            PreBookingVM = new PreBookingVM();
            ErrorMessages = new ObservableCollection<string>();
            ErrorMessages.Clear();
        }

        private async void PostPreBooking()
        {
            try
            {
                ErrorMessages.Clear();
                IsErrorListVisible = false;
                PreBookingVM.TourId = TourId;             
                PreBookingVM.TouristId = UserId;
                PreBookingVM.GuideId = StepListOverviewVM.GuideId;              
                PreBookingVM.NbrVehicule = EmplacementNumber;
                PreBookingVM.NbrChild = ChildNumber;
                PreBookingVM.NbrAdult = AdultNumber;
                HttpResponseMessage result = await BookingService.CheckErrors(PreBookingVM);  
                
                if (result.IsSuccessStatusCode)
                {
                    BookingDetailVM bookingDetailVM = new BookingDetailVM()
                    {
                        TourName = StepListOverviewVM.TourName,
                        TourId = StepListOverviewVM.TourId,
                        TourImg = StepListOverviewVM.ImageUrl,
                        DateStart = PreBookingVM.DateStart,
                        DateEnd = PreBookingVM.DateStart.AddDays(StepListOverviewVM.NbDays),
                        NbAdult = PreBookingVM.NbrAdult,
                        NbChild = PreBookingVM.NbrChild,
                        NbVehicule = PreBookingVM.NbrVehicule,
                        TotalPriceAdult = PreBookingVM.NbrAdult * StepListOverviewVM.PricePerAdult,
                        TotalPriceChild = PreBookingVM.NbrChild * StepListOverviewVM.PricePerChild,
                        TotalPriceVehicule = PreBookingVM.NbrVehicule * StepListOverviewVM.PricePerVehicule,
                    };
                    bookingDetailVM.TotalPrice = bookingDetailVM.TotalPriceChild + bookingDetailVM.TotalPriceAdult + bookingDetailVM.TotalPriceVehicule;
                    await Navigation.PushPopupAsync(new SummaryBookingPage(bookingDetailVM,Page, PreBookingVM));                    
                }
                else
                {
                    IsErrorListVisible = true;
                    List<bool> boolList = JsonConvert.DeserializeObject<List<bool>>(await result.Content.ReadAsStringAsync());
                    if (boolList[0])
                    {
                        ErrorMessages.Add("Guide is not available at this date");
                    }
                    if (boolList[1])
                    {
                        ErrorMessages.Add("Point of interest unavailable at this date");
                    }
                    if (boolList[2])
                    {
                        ErrorMessages.Add("There is no more emplacement at this date");
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        #region Load TourId
        public async void LoadTour()
        {
            try
            {
                StepListOverviewVM = await TourService.GetTourOverviewById(TourId, MotherLanguage);
                if(StepListOverviewVM.PricePerAdult == null)
                {
                    StepListOverviewVM.PricePerAdult = 0;
                }
                if (StepListOverviewVM.PricePerChild == null)
                {
                    StepListOverviewVM.PricePerChild = 0;
                }
                if (StepListOverviewVM.PricePerVehicule == null)
                {
                    StepListOverviewVM.PricePerVehicule = 0;
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
