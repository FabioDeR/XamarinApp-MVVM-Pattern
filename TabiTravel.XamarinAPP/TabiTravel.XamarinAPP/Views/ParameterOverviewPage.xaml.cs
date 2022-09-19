using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabiTravel.XamarinAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ParameterOverviewPage : ContentPage
    {
        public ParameterOverviewPage()
        {
            InitializeComponent();
        }

        private void AppLangEditButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var userId = App.Database.GetUserId();
                Navigation.PushPopupAsync(new AppLanguageEditPage(userId));
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}