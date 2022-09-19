using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinAPP.Views;
using TabiTravel.XamarinModel.Enum;
using TabiTravel.XamarinModel.Models.TranslationModel;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.ViewModels
{
    public class TranslatePageVM : BaseVM
    {
        #region Translate Properties
        private ObservableCollection<TranslateVM> translateVMs;
        public ObservableCollection<TranslateVM> TranslateVMs { get => translateVMs; set { translateVMs = value; OnPropertyChanged(nameof(TranslateVMs)); } }

        #endregion
        #region Service Instance
        ITranslateService TranslateService;
        ISoftDeleteService SoftDeleteService;
        EnumTranslate EnumTranslate;
        #endregion
        private int identityId;
        public int IdentityId { get => identityId; set { identityId = value; OnPropertyChanged(nameof(IdentityId)); } }

        public TranslatePageVM(INavigation navigation, Page page, EnumTranslate enumTranslate, int id) : base(navigation, page)
        {
            TranslateVMs = new ObservableCollection<TranslateVM>();
            TranslateService = DependencyService.Get<ITranslateService>(DependencyFetchTarget.NewInstance);
            SoftDeleteService = DependencyService.Get<ISoftDeleteService>(DependencyFetchTarget.NewInstance);
            EnumTranslate = enumTranslate;
            IdentityId = id;
            GoTo = new Command(GoToAddTranslation);
            Update = new Command(UpdateTranslation);
            Delete = new Command(SoftDelete);
        }

        private async void SoftDelete(object obj)
        {
            try
            {

                var selectedItem = obj as TranslateVM;
                bool responseDiqplay = await Page.DisplayAlert("Warning", "Are you sure you want to delete this item ?", "Ok", "Cancel");
                if (responseDiqplay)
                {
                    var res = await SoftDeleteService.SofDeleteTranslate(EnumTranslate, selectedItem.TranslateId, selectedItem.LanguageIso);
                    if (res.IsSuccessStatusCode)
                    {
                        await Page.DisplayAlert("Success", "Item was Deleted", "Ok");
                        TranslateVMs.Remove(selectedItem);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private async void UpdateTranslation(object obj)
        {
            try
            {
                var selectedItem = obj as TranslateVM;
                await Navigation.PushAsync(new TranslationEditPage(IdentityId, EnumTranslate, selectedItem.LanguageIso));
                Navigation.RemovePage(Page);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private async void GoToAddTranslation()
        {
            try
            {
                await Navigation.PushAsync(new TranslationEditPage(IdentityId, EnumTranslate));
                Navigation.RemovePage(Page);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async void GetTranslateList()
        {
            try
            {
                var list = await TranslateService.GetTranslateVMs(IdentityId, EnumTranslate, MotherLanguage);
                TranslateVMs = new ObservableCollection<TranslateVM>(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

    }
}
