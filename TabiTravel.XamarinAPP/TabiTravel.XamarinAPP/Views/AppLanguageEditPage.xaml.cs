using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.XamarinAPP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabiTravel.XamarinAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppLanguageEditPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        AppLanguageVM AppLanguageVM;

        public AppLanguageEditPage(Guid userId)
        {
            InitializeComponent();
            AppLanguageVM = new AppLanguageVM(Navigation, this);
            BindingContext = AppLanguageVM;

        }
        protected async override void OnAppearing()
        {
            Loader.IsVisible = true;
            try
            {
                await AppLanguageVM.LoadLanguageList();
                base.OnAppearing();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            PageContent.IsVisible = true;
            Loader.IsVisible = false;
        }


    }
}