using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinModel.Models;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.ViewModels
{
    public class AppLanguageVM : BaseVM
    {
       
        private ObservableCollection<PreferedLanguageVM> languageList;
        public ObservableCollection<PreferedLanguageVM> LanguageList { get => languageList; set { languageList = value; OnPropertyChanged(nameof(LanguageList)); } }
        
        ILanguageService LanguageService;
        public AppLanguageVM(INavigation navigation, Page page) : base(navigation, page)
        {
           LanguageService = DependencyService.Get<ILanguageService>(DependencyFetchTarget.NewInstance);
           Update = new Command(UpdateLanguageApp);
        }     

        private async void UpdateLanguageApp(object obj)
        {
            try
            {
                var languageSelected = obj as PreferedLanguageVM;
                languageSelected.IsSelected = true;
                LanguageList.Where(e => e.LanguageIso != languageSelected.LanguageIso).ToList().ForEach(e => { e.IsSelected = false; });
                var newListLanguage = new List<PreferedLanguageVM>(LanguageList);
                var res = LanguageService.UpdateLanguageApp(newListLanguage);
                if(res.IsCompleted)
                {
                    await Page.DisplayAlert("Sucess", "Item Updated", "Ok");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task LoadLanguageList()
        {
            try
            {
                var languageListservice = await LanguageService.GetLanguageApp(UserId);
                LanguageList = new ObservableCollection<PreferedLanguageVM>(languageListservice);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }        

    }
}
