using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinAPP.Views;
using TabiTravel.XamarinModel.Models;
using TabiTravel.XamarinModel.Models.TourModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.ViewModels
{
    public class TourEditPageVM : BaseVM
    {
        #region Tour properties
        private TourEditVM tourEditVM;
        public TourEditVM TourEditVM { get => tourEditVM; set { tourEditVM = value; OnPropertyChanged(nameof(TourEditVM)); } }
        private Tour tour;
        public Tour Tour { get => tour; set { tour = value; OnPropertyChanged(nameof(Tour)); } }
        private bool isFree;
        public bool IsFree { get => isFree; set { isFree = value; OnPropertyChanged(nameof(IsFree)); } }
        #endregion
        #region Observable Properties
        private ObservableCollection<GetCategoryVM> categoryTourList;
        public ObservableCollection<GetCategoryVM> CategoryTourList { get => categoryTourList; set { categoryTourList = value; OnPropertyChanged(nameof(CategoryTourList)); } }
        private ObservableCollection<GetCountryVM> countriesList;
        public ObservableCollection<GetCountryVM> CountriesList { get => countriesList; set { countriesList = value; OnPropertyChanged(nameof(CountriesList)); } }
        #endregion
        #region Simple Properties
        private string content;
        public string Content { get => content; set { content = value; OnPropertyChanged(nameof(Content)); } }
        private string countryName;
        public string CountryName { get => countryName; set { countryName = value; OnPropertyChanged(nameof(CountryName)); } }
        #endregion
        #region Service Instance
        ICountryService CountryService;
        ITourService TourService;
        #endregion        
        #region Selected Properties
        private GetCountryVM _countrySelected;
        public GetCountryVM CountrySelected
        {
            get { return _countrySelected; }
            set
            {
                if (_countrySelected != value)
                {
                    _countrySelected = value;
                    OnPropertyChanged(nameof(CountrySelected));
                }
            }
        }

        #endregion

        public TourEditPageVM(INavigation navigation, Page page) : base(navigation, page)
        {
            Save = new Command(PostTour);
            PostPhoto = new Command(PostPhotoTourAsync);
            TourService = DependencyService.Get<ITourService>(DependencyFetchTarget.NewInstance);
            CountryService = DependencyService.Get<ICountryService>(DependencyFetchTarget.NewInstance);
            CountriesList = new ObservableCollection<GetCountryVM>();
            CategoryTourList = new ObservableCollection<GetCategoryVM>();
            TourEditVM = new TourEditVM();
            Tour = new Tour();
            if(Tour.ImageUrl == null)
            {
                Tour.ImageUrl = "http://10.0.2.2:6909/api/Image/interestimage/ca6fbce1-9cbb-4cfd-95f3-9c5ceaf97c97_nopicture.jpg";
            }
        }

        

       
        #region Post Photo
        private async void PostPhotoTourAsync()
        {
            try
            {

                var response = await Page.DisplayActionSheet("Take a Picture", "Cancel", null, "Folders", "Camera");
                switch (response)
                {
                    case "Folders":
                        var ResultFile = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                        {
                            Title = "Please pick a photo",
                        });
                        if (ResultFile == null)
                        {
                            return;
                        }
                        await PostPhotointerest(ResultFile);
                        break;
                    case "Camera":
                        var ResultFileCam = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                        {
                            Title = "Please pick a photo",
                        });
                        if (ResultFileCam == null)
                        {
                            return;
                        }
                        await PostPhotointerest(ResultFileCam);
                        break;
                    default:
                        break;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        private async Task PostPhotointerest(FileResult resultFile)
        {
            var content = new MultipartFormDataContent();
            var stream = await resultFile.OpenReadAsync();
            content.Add(new StreamContent(stream), "file", resultFile.FileName);
            var response = await TourService.UploadImageTour(content);
            try
            {
                Tour.ImageUrl = response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        #region Post
        public async void PostTour()
        {
            try
            {
                if (isFree)
                {
                    Tour.PricePerVehicule = null;
                    Tour.PricePerChild = null;
                    Tour.PricePerAdult = null;
                }

                List<GetCategoryVM> getCategoryVMs = new List<GetCategoryVM>();
                getCategoryVMs = new List<GetCategoryVM>(CategoryTourList);
                if (CountrySelected != null)
                {
                    Tour.CountryId = CountrySelected.CountryId;
                }
                Tour.UserId = UserId;
                if (!ValidationHelper.IsFormValid(Tour, Page)) { return; }
                TourEditVM.Categories = getCategoryVMs;
                TourEditVM.Content = Content;
                TourEditVM.LanguageIso = MotherLanguage;
                TourEditVM.Country = CountrySelected.Name;
                TourEditVM.Tour = Tour;

                
                var res = await TourService.AddTour(TourEditVM);
                if (res.IsSuccessStatusCode)
                {
                    await Page.DisplayAlert("Success", "Item has added", "OK");
                    string tourid = res.Content.ReadAsStringAsync().Result;
                    await Navigation.PushAsync(new TourOverviewPage(Int32.Parse(tourid)));
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
        #region Get Category Tour
        public async Task LoadCategoryTour()
        {
            try
            {
                var Catelist = await TourService.GetAllCategoryVMForTour(MotherLanguage);
                CategoryTourList = new ObservableCollection<GetCategoryVM>(Catelist);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        #region Get Country Tour
        public async Task LoadCountry()
        {
            try
            {
                var List = await CountryService.GetCountryVM(MotherLanguage);
                CountriesList = new ObservableCollection<GetCountryVM>(List);
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
