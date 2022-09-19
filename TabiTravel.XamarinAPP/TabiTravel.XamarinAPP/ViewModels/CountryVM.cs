using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinAPP.Views;
using TabiTravel.XamarinModel.Models;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.ViewModels
{
    public class CountryVM : BaseVM, INotifyPropertyChanged
    {
        private ObservableCollection<GetCountryVM> countriesList;
        public ObservableCollection<GetCountryVM> CountriesList { get => countriesList; set { countriesList = value; OnPropertyChanged(nameof(CountriesList)); } }
        ICountryService CountryService;
        public CountryVM(INavigation navigation, Page page) : base(navigation, page)
        {
            CountryService = DependencyService.Get<ICountryService>(DependencyFetchTarget.NewInstance);
            CountriesList = new ObservableCollection<GetCountryVM>();
            Update = new Command(UpdateUserCountry);
        }
        #region Update User Country
        private async void UpdateUserCountry(object obj)
        {
            try
            {

                var countrySelected = obj as GetCountryVM;

                var res = CountryService.UpdateUserCountry(countrySelected.Name, UserId);
                Navigation.RemovePage(Page);


                if (res.IsCompleted)
                {
                    await Page.DisplayAlert("Success", $"Item Updated - {countrySelected.Name} ", "ok");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        #region LoadCountriesList
        public async void LoadCountriesList()
        {
            try
            {
                var countriesList = await CountryService.GetCountryVM(MotherLanguage);
                CountriesList = new ObservableCollection<GetCountryVM>(countriesList);
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
