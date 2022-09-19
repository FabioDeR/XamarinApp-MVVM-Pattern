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
    public partial class CountryListPage : ContentPage
    {
        CountryVM countryVM;
        public CountryListPage(Guid UserId)
        {
            InitializeComponent();
            countryVM = new CountryVM(Navigation, this);
            BindingContext = countryVM;
        }

        protected override void OnAppearing()
        {
            try
            {
                countryVM.LoadCountriesList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                base.OnAppearing();
                throw;
            }
        }



    }
}