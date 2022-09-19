using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinAPP.Views;
using TabiTravel.XamarinModel.Models;
using TabiTravel.XamarinModel.Models.TourModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.ViewModels
{
    public class TourEditDetailVM : BaseVM
    {
        #region Tour properties
        private TourEditVM tourEditVM;
        public TourEditVM TourEditVM { get => tourEditVM; set { tourEditVM = value; OnPropertyChanged(nameof(TourEditVM)); } }

        private Tour tour;
        public Tour Tour { get => tour; set { tour = value; OnPropertyChanged(nameof(Tour)); } }
        private bool isFree;
        public bool IsFree { get => isFree; set { isFree = value; OnPropertyChanged(nameof(IsFree)); } }
        #endregion
        #region Simple Properties
        private string content;
        public string Content { get => content; set { content = value; OnPropertyChanged(nameof(Content)); } }
        private string countryName;
        public string CountryName { get => countryName; set { countryName = value; OnPropertyChanged(nameof(CountryName)); } }
        #endregion
        ICountryService CountryService;
        ITourService TourService;
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
        #region Observable Properties
        private ObservableCollection<GetCategoryVM> categoryTourList;
        public ObservableCollection<GetCategoryVM> CategoryTourList { get => categoryTourList; set { categoryTourList = value; OnPropertyChanged(nameof(CategoryTourList)); } }
        private ObservableCollection<GetCountryVM> countriesList;
        public ObservableCollection<GetCountryVM> CountriesList { get => countriesList; set { countriesList = value; OnPropertyChanged(nameof(CountriesList)); } }
        #endregion
        public TourEditDetailVM(INavigation navigation, Page page) : base(navigation, page)
        {
            Update = new Command(UpdateTour);
            PostPhoto = new Command(PostPhotoTourAsync);
            TourService = DependencyService.Get<ITourService>(DependencyFetchTarget.NewInstance);
            CountryService = DependencyService.Get<ICountryService>(DependencyFetchTarget.NewInstance);
        }
        private async void UpdateTour()
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
                TourEditVM.Categories = getCategoryVMs;
                TourEditVM.LanguageIso = MotherLanguage;
                TourEditVM.Country = CountrySelected.Name;
                TourEditVM.Content = Content;
                TourEditVM.Tour = Tour;
                var res = await TourService.UpdateTour(TourEditVM);
                if (res.IsSuccessStatusCode)
                {
                    await Page.DisplayAlert("Success", "Item has updated", "OK");
                    await Navigation.PushAsync(new TourListPage());
                    Navigation.RemovePage(Page);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
        public async Task GetTourById(int tourId)
        {
            try
            {
                TourEditVM = await TourService.GetById(UserId, MotherLanguage, tourId);
                Tour = TourEditVM.Tour;
                CountryName = TourEditVM.Country;
                Content = TourEditVM.Content;
                CountrySelected = CountriesList.Where(c => c.CountryId == TourEditVM.Tour.CountryId).First();
                CategoryTourList = new ObservableCollection<GetCategoryVM>(TourEditVM.Categories);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #region Post Photo
        private async void PostPhotoTourAsync()
        {
            try
            {
                var ResultFile = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Please pick a photo",
                });
                if (ResultFile == null)
                {
                    return;
                }
                PostPhotointerest(ResultFile);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        private async void PostPhotointerest(FileResult resultFile)
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
