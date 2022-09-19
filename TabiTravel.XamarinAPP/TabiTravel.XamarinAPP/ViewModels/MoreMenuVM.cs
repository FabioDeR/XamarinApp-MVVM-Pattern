using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinAPP.Views;
using TabiTravel.XamarinModel.Enum;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.ViewModels
{
    public class MoreMenuVM : BaseVM
    {
        private NavigationEnum EnumNav;
        private EnumTranslate EnumTranslate;
        private int IdentityId;
        public int TourId;
        ISoftDeleteService SoftDeleteService;

        public MoreMenuVM(INavigation navigation, Page page, int id, NavigationEnum naviguationEnum, EnumTranslate translateEnum, int? tourId = null) : base(navigation, page)
        {
            GoTo = new Command(Edit_Tapped);
            GoToTwo = new Command(AddTranslation_Tapped);
            Delete = new Command(SofDelete);
            SoftDeleteService = DependencyService.Get<ISoftDeleteService>(DependencyFetchTarget.NewInstance);
            EnumNav = naviguationEnum;
            EnumTranslate = translateEnum;
            IdentityId = id;
            if (tourId != null)
            {
                TourId = (int)tourId;
            }
        }

        private async void SofDelete()
        {
            try
            {
                bool responseDisplay = await Page.DisplayAlert("Warning", "Are you sure you want to delete this item ?", "Ok", "Cancel");
                if (responseDisplay)
                {
                    var res = await SoftDeleteService.SofDelete(EnumNav, IdentityId, UserId);
                    if (res.IsSuccessStatusCode)
                    {
                        await Page.DisplayAlert("Success", "Item was deleted", "Ok");
                        await Navigation.PopPopupAsync();
                        ((MainPage)App.Current.MainPage).Detail.Navigation.RemovePage(Page);
                        switch (EnumNav)
                        {
                            case NavigationEnum.InterestEdit:
                                await ((MainPage)App.Current.MainPage).Detail.Navigation.PushAsync(new InterestListPage());
                                break;
                            case NavigationEnum.StepEdit:
                                //  ::::: /!\ Need TourId (not StepId = IdentityId) to return on TourDetailPage after SoftDelete /!\ :::::
                                await ((MainPage)App.Current.MainPage).Detail.Navigation.PushAsync(new TourOverviewPage(TourId));
                                break;
                            case NavigationEnum.TourEdit:
                                await ((MainPage)App.Current.MainPage).Detail.Navigation.PushAsync(new TourListPage());
                                break;
                            case NavigationEnum.TourOverviewEdit:
                                await ((MainPage)App.Current.MainPage).Detail.Navigation.PushAsync(new TourListPage());
                                break;
                            default:
                                break;
                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private async void Edit_Tapped()
        {
            await Navigation.PopPopupAsync();

            switch (EnumNav)
            {
                case NavigationEnum.InterestEdit:
                    await ((MainPage)App.Current.MainPage).Detail.Navigation.PushAsync(new PoiEditPage(IdentityId));
                    break;
                case NavigationEnum.StepEdit:
                    await ((MainPage)App.Current.MainPage).Detail.Navigation.PushAsync(new StepEditPage(IdentityId));
                    break;
                case NavigationEnum.TourEdit:
                    await ((MainPage)App.Current.MainPage).Detail.Navigation.PushAsync(new TourOverviewPage(IdentityId));
                    break;
                case NavigationEnum.TourOverviewEdit:
                    await ((MainPage)App.Current.MainPage).Detail.Navigation.PushAsync(new TourEditPage(IdentityId));
                    break;
                default:
                    break;
            }
        }

        private async void AddTranslation_Tapped()
        {
            await Navigation.PopPopupAsync();
            await ((MainPage)App.Current.MainPage).Detail.Navigation.PushAsync(new TranslationListPage(IdentityId, EnumTranslate));
        }
    }
}
