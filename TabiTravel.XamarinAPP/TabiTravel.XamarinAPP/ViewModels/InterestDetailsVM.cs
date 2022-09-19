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
using TabiTravel.XamarinModel.Enum;
using TabiTravel.XamarinModel.Models;
using TabiTravel.XamarinModel.Models.InterestModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.ViewModels
{
    public class InterestDetailsVM : BaseVM
    {
        IInterestService InterestService;          
        private int interestId;
        public int InterestId
        {
            get => interestId;
            set
            {
                interestId = value;
                OnPropertyChanged(nameof(InterestId));
            }

        }
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

        #region Interest Details VM
        private string countryName;
        public string CountryName { get => countryName; set { countryName = value; OnPropertyChanged(nameof(CountryName)); } }
        private string content;
        public string Content { get => content; set { content = value; OnPropertyChanged(nameof(Content)); } }
        private AddInterestVM addInterestVM;
        private ObservableCollection<GetCategoryVM> categoryListDetail;
        public ObservableCollection<GetCategoryVM> CategoryListDetail { get => categoryListDetail; set { categoryListDetail = value; OnPropertyChanged(nameof(CategoryListDetail)); } }

        private ObservableCollection<GetCategoryVM> categoryList;
        public ObservableCollection<GetCategoryVM> CategoryList { get => categoryList; set { categoryList = value; OnPropertyChanged(nameof(CategoryList)); } }

        public AddInterestVM AddInterestVM { get => addInterestVM; set { addInterestVM = value;OnPropertyChanged(nameof(AddInterestVM)); } }
        #endregion

        public InterestDetailsVM(INavigation navigation, int interestId, Page page) : base(navigation, page)
        {
            InterestId = interestId;
            Interest = new Interest();
            InterestService = DependencyService.Get<IInterestService>(DependencyFetchTarget.NewInstance);           
            Update = new Command(UpdateInterest);          
            PostPhoto = new Command(PostPhotoInterestAsync);
            CategoryList = new ObservableCollection<GetCategoryVM>();
            AddInterestVM = new AddInterestVM();
        }
        
        
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

        private async void UpdateInterest()
        {
            try
            {
                AddInterestVM addInterestVM = new AddInterestVM();
                if (Interest.IsFree)
                {
                    Interest.PricePerAdult = null;
                    Interest.PricePerChild = null;
                }
                if (Interest.IsFreeParking)
                {
                    Interest.PricePerVehicule = null;
                }
                if (CategoryList.Where(c => c.CategoryId == 8 && c.IsSelected == false).Any())
                {
                    Interest.EmplacementAvailable = null;
                }
                if (!ValidationHelper.IsFormValid(Interest, Page)) { return; }
                addInterestVM.Interest = Interest;
                addInterestVM.Content = Content;
                addInterestVM.LanguageIso = App.Database.GetUserMotherLanguage();
                addInterestVM.Categories = new List<GetCategoryVM>(CategoryList);
                var res = await InterestService.Update(addInterestVM);
                if (res.IsSuccessStatusCode)
                {                    
                    await Page.DisplayAlert("Update", "Item Updated", "ok");
                    await Navigation.PushAsync(new PoiDetailPage(InterestId,true));
                    Navigation.RemovePage(Page);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task LoadInterest(int interestId)
        {
            try
            {             
                AddInterestVM = await InterestService.GetById(MotherLanguage, interestId);
                Interest = AddInterestVM.Interest;
                Content = AddInterestVM.Content;
                CountryName = AddInterestVM.Country;
                CategoryList = new ObservableCollection<GetCategoryVM>(AddInterestVM.Categories);             
                CategoryListDetail = new ObservableCollection<GetCategoryVM>(CategoryList.Where(i => i.IsSelected == true).ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }      
    }
}
