using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TabiTravel.Service;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinAPP.DbSqlite;
using TabiTravel.XamarinAPP.Views;
using TabiTravel.XamarinModel.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.ViewModels
{
    public class UserVM : BaseVM
    {
        
        private ProfilEditVM user;
        public ProfilEditVM User { get => user; set { user = value; OnPropertyChanged(nameof(User)); } }

        ILanguageService LanguageService;

        private ObservableCollection<string> motherLanguesPicker;
        public ObservableCollection<string> MotherLanguesPicker { get => motherLanguesPicker; set { motherLanguesPicker = value; OnPropertyChanged("MotherLanguesPicker"); } }

        private ObservableCollection<string> firstLanguagePicker;
        public ObservableCollection<string> FirstLanguagePicker { get => firstLanguagePicker; set { firstLanguagePicker = value; OnPropertyChanged("FirstLanguagePicker"); } }

        private ObservableCollection<string> secondLanguagePicker;
        public ObservableCollection<string> SecondLanguagePicker { get => secondLanguagePicker; set { secondLanguagePicker = value; OnPropertyChanged("SecondLanguagePicker"); } }

        private string _selectedMotherLanguage;
        public string SelectedMotherLanguage
        {
            get { return _selectedMotherLanguage; }
            set
            {
                if (_selectedMotherLanguage != value)
                {
                    _selectedMotherLanguage = value;
                    OnPropertyChanged(nameof(SelectedMotherLanguage));
                }
            }
        }
        private string _selectedFirstLanguage;
        public string SelectedFirstLanguage
        {
            get { return _selectedFirstLanguage; }
            set
            {
                if (_selectedFirstLanguage != value)
                {
                    _selectedFirstLanguage = value;
                    OnPropertyChanged(nameof(SelectedFirstLanguage));

                }
            }
        }
        private string _selectedSecondLanguage;
        public string SelectedSecondLanguage
        {
            get { return _selectedSecondLanguage; }
            set
            {
                if (_selectedSecondLanguage != value)
                {
                    _selectedSecondLanguage = value;
                    OnPropertyChanged(nameof(SelectedSecondLanguage));

                }
            }
        }
        private string countryName;
        public string CountryName { get => countryName; set { countryName = value; OnPropertyChanged(nameof(CountryName)); } }
        ICountryService CountryService;
        IUserService UserService;

        public UserVM(INavigation navigation, Page page) : base(navigation, page)
        {
            UserService = DependencyService.Get<IUserService>(DependencyFetchTarget.NewInstance);
            LanguageService = DependencyService.Get<ILanguageService>(DependencyFetchTarget.NewInstance);
            CountryService = DependencyService.Get<ICountryService>(DependencyFetchTarget.NewInstance);
            User = new ProfilEditVM();
            MotherLanguesPicker = new ObservableCollection<string>();
            FirstLanguagePicker = new ObservableCollection<string>();
            SecondLanguagePicker = new ObservableCollection<string>();
            Goto = new Command(NavigationCategoriesListPage);
            Update = new Command(UpdateUser);
            UnSelectedOtherlanguage1 = new Command(execute: () =>
            {
                SelectedFirstLanguage = null;
            });
            UnSelectedOtherLanguage2 = new Command(execute: () =>
            {
                SelectedSecondLanguage = null;
            });            
            PostPhoto = new Command(PostPhotoUserAsync);
        }

        #region Post Photo
        private async void PostPhotoUserAsync()
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
            var response = await UserService.UploadImageUser(content,UserId);
            try
            {
                User.Avatar = response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion




        #region Get By UserId
        public async Task GetUserById()
        {
            try
            {

                User = await UserService.GetById(UserId, MotherLanguage);
                var LanguagesList = await LanguageService.GetAllLanguage();
                MotherLanguesPicker = new ObservableCollection<string>(LanguagesList);
                FirstLanguagePicker = new ObservableCollection<string>(LanguagesList);
                SecondLanguagePicker = new ObservableCollection<string>(LanguagesList);
                SelectedSecondLanguage = user.AnotherLanguage2;
                SelectedMotherLanguage = user.MotherLanguage;
                SelectedFirstLanguage = user.AnotherLanguage1;
                if (User.Avatar == null)
                {
                    User.Avatar = "http://10.0.2.2:6909/api/Image/UserImage/ca6fbce1-9cbb-4cfd-95f3-9c5ceaf97c97_nopicture.jpg";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async void UpdateUser()
        {
            try
            {
                if (SelectedMotherLanguage != string.Empty)
                {
                    User.MotherLanguage = SelectedMotherLanguage;
                }
                if (SelectedFirstLanguage != string.Empty)
                {
                    User.AnotherLanguage1 = SelectedFirstLanguage;
                }
                if (SelectedSecondLanguage != string.Empty)
                {
                    User.AnotherLanguage2 = SelectedSecondLanguage;
                }
                var res = await UserService.UpdateUser(User);
                if (res.IsSuccessStatusCode)
                {
                    App.Database.UpdateUserInfo(SelectedMotherLanguage);
                    await Page.DisplayAlert("Success", "Item updated", "Ok");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        #region Navigation To CountriesListPage
        private void NavigationCategoriesListPage(object obj)
        {
            try
            {
                Navigation.PushAsync(new CountryListPage(User.UserId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        public ICommand UnSelectedOtherlanguage1 { private set; get; }
        public ICommand UnSelectedOtherLanguage2{ private set; get; }
     
        public ICommand Goto { get; set; }
      }

       




      

    
}
