using System;
using System.Collections.Generic;
using System.Text;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinAPP.Views;
using TabiTravel.XamarinModel.Enum;
using TabiTravel.XamarinModel.Models.TranslationModel;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.ViewModels
{
    public class TranslateDetailPageVM : BaseVM
    {

        private TranslateVM translateVM;
        public TranslateVM TranslateVM { get => translateVM; set { translateVM = value;OnPropertyChanged(nameof(TranslateVM)); } }
        private List<string> languages;
        public List<string> Languages { get => languages; set { languages = value; OnPropertyChanged(nameof(Languages)); } }
        private string _selectedLanguage { get; set; }
        public string SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                if (_selectedLanguage != value)
                {
                    _selectedLanguage = value;
                    OnPropertyChanged(nameof(SelectedLanguage));
                }
            }
        }
        private string _myLanguage;
        public string MyLanguage
        {
            get { return _myLanguage; }
            set
            {
                if (_myLanguage != value)
                {
                    _myLanguage = value;
                    OnPropertyChanged(nameof(MyLanguage));
                }
            }
        }
        ITranslateService TranslateService;
        ILanguageService LanguageService;
        EnumTranslate EnumTranslate;
        private int identityId;
        public int IdentityId { get => identityId; set { identityId = value;OnPropertyChanged(nameof(IdentityId)); } }        

        public TranslateDetailPageVM(INavigation navigation, Page page,EnumTranslate enumTranslate,int id) : base(navigation, page)
        {
            Save = new Command(PostTranslate);
            Update = new Command(UpdateTranslate);
            TranslateVM = new TranslateVM();
            EnumTranslate = enumTranslate;
            IdentityId = id;
            TranslateService = DependencyService.Get<ITranslateService>(DependencyFetchTarget.NewInstance);
            LanguageService = DependencyService.Get<ILanguageService>(DependencyFetchTarget.NewInstance);

        }
        public async void GetLanguagelist()
        {
            try
            {                
                Languages = await LanguageService.GetLanguageNotUsed(IdentityId, UserId, EnumTranslate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        private async void PostTranslate()
        {
            try
            {
                switch (EnumTranslate)
                {
                    case EnumTranslate.Interest:
                        TranslateVM.InterestId = IdentityId;
                        break;
                    case EnumTranslate.Step:
                        TranslateVM.StepId = IdentityId;
                        break;
                    case EnumTranslate.Tour:
                        TranslateVM.TourId = IdentityId;
                        break;
                    default:
                        break;
                }
                TranslateVM.LanguageIso = SelectedLanguage;                
                var res = await TranslateService.PostTranslateVM(TranslateVM,EnumTranslate);
                if (res.IsSuccessStatusCode)
                {
                    await Page.DisplayAlert("Sucess", "Item Added", "ok");
                    await Navigation.PushAsync(new TranslationListPage(IdentityId,EnumTranslate));
                    Navigation.RemovePage(Page);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async void GetTranslateVMById(string languageIso)
        {
            try
            {
                TranslateVM = await TranslateService.GetTranslateVM(IdentityId, EnumTranslate, languageIso);                
                MyLanguage = languageIso;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async void UpdateTranslate()
        {
            try
            {
                var res = await TranslateService.UpdateTranslate(TranslateVM);
                if (res.IsSuccessStatusCode)
                {
                    await Page.DisplayAlert("Sucess", "Item Updated", "ok");
                    await Navigation.PushAsync(new TranslationListPage(IdentityId, EnumTranslate));
                    Navigation.RemovePage(Page);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
