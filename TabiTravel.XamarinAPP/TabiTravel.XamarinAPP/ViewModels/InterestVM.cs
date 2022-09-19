using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TabiTravel.Service;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinAPP.DbSqlite;
using TabiTravel.XamarinAPP.Views;
using TabiTravel.XamarinModel.Models;
using TabiTravel.XamarinModel.Models.InterestModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.ViewModels
{
    public class InterestListVM : BaseVM
    {
        private bool freeParking;
        public bool FreeParking { get => freeParking; set { freeParking = value;OnPropertyChanged(nameof(FreeParking)); } }
        private Interest _interest;
        public Interest Interest
        {
            get { return _interest; }
            set
            {
                _interest = value;

                OnPropertyChanged(nameof(Interest));
            }
        }
        private AddInterestVM addInterestVM;
        public AddInterestVM AddInterestVM { get => addInterestVM; set { addInterestVM = value; OnPropertyChanged(nameof(AddInterestVM)); } }
        private string content;      

        public string Content { get => content; set { content = value; OnPropertyChanged(nameof(Content)); } }
        private string countryName;
        public string CountryName { get => countryName; set { countryName = value; OnPropertyChanged(nameof(CountryName)); } }
       
        private ObservableCollection<GetCategoryVM> categoryList;
        public ObservableCollection<GetCategoryVM> CategoryList { get => categoryList; set { categoryList = value;OnPropertyChanged(nameof(CategoryList)); } }
        IInterestService InterestService;
        ICountryService CountryService;
        ICategoryService CategoryService;
        

        public InterestListVM(INavigation navigation, Page page) : base(navigation, page)
        {
            Save = new Command(PostInterest);
            CategoryList = new ObservableCollection<GetCategoryVM>();
            Interest = new Interest();
            AddInterestVM = new AddInterestVM();
            PostPhoto = new Command(PostPhotoInterestAsync);
            InterestService = DependencyService.Get<IInterestService>(DependencyFetchTarget.NewInstance);
            CountryService = DependencyService.Get<ICountryService>(DependencyFetchTarget.NewInstance);
            CategoryService = DependencyService.Get<ICategoryService>(DependencyFetchTarget.NewInstance);
            if(Interest.ImageUrl == null)
            {
                Interest.ImageUrl = "http://10.0.2.2:6909/api/Image/interestimage/ca6fbce1-9cbb-4cfd-95f3-9c5ceaf97c97_nopicture.jpg";
            }            
        }

     

        #region Post Photo
        private async void PostPhotoInterestAsync()
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
            var response = await InterestService.UploadImageInterest(content);
            try
            {
                Interest.ImageUrl = response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion        
        #region PostInterest
        public async void PostInterest()
        {
            try
            {
                if (CategoryList.Where(c => c.CategoryId == 8 && c.IsSelected == false).Any())
                {
                    Interest.EmplacementAvailable = null;
                }
                Interest.UserId = UserId;
                if (Interest.IsFree)
                {
                    Interest.PricePerAdult = null;
                    Interest.PricePerChild = null;
                }
                if (FreeParking)
                {
                    Interest.PricePerVehicule = null;
                }
                if (!ValidationHelper.IsFormValid(Interest, Page)) { return; }                        
               

                
                AddInterestVM.Interest = Interest;
                AddInterestVM.Content = Content;
                AddInterestVM.LanguageIso = MotherLanguage;
                AddInterestVM.Country = CountryName;
                AddInterestVM.Categories = new List<GetCategoryVM>(CategoryList); 
                
                var response = await InterestService.AddInterest(AddInterestVM);
                if (response.IsSuccessStatusCode)
                {
                    await Navigation.PushAsync(new MapPage());
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
        #region Feed Forms
        public async void FeedForms(double latitude, double longitude)
        {
            try
            {
                string motherLanguage = App.Database.GetUserMotherLanguage();
                var placemarks = await Geocoding.GetPlacemarksAsync(latitude, longitude);
                var placemark = placemarks?.FirstOrDefault();
                string countryTrad = await CountryService.GetTranslateCountry(placemark.CountryName, MotherLanguage);
                Interest.City = placemark.Locality;
                Interest.ZipCode = placemark.PostalCode;                
                CountryName = countryTrad;
                var categoryList1 = await CategoryService.GetCategoryVM(MotherLanguage);
                CategoryList = new ObservableCollection<GetCategoryVM>(categoryList1);
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
