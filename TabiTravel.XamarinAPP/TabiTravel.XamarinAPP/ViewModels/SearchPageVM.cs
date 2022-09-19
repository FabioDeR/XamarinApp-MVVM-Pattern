using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinAPP.Views;
using TabiTravel.XamarinModel.Enum;
using TabiTravel.XamarinModel.Models;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.ViewModels
{
    public class SearchPageVM : BaseVM

    {
        #region SearPage VM Prpoerties
        private SearchVM searchVM;
        public SearchVM SearchVM { get => searchVM; set { searchVM = value; OnPropertyChanged(nameof(SearchVM)); } }
        private ObservableCollection<PreferedLanguageVM> languageList;
        public ObservableCollection<PreferedLanguageVM> LanguageList { get => languageList; set { languageList = value; OnPropertyChanged(nameof(LanguageList)); } }
        private PreferedLanguageVM selectedLanguage;
        public PreferedLanguageVM SelectedLanguage { get => selectedLanguage; set { selectedLanguage = value; OnPropertyChanged(nameof(SelectedLanguage)); } }
        private ObservableCollection<GetCategoryVM> categoryList;
        public ObservableCollection<GetCategoryVM> CategoryList { get => categoryList; set { categoryList = value; OnPropertyChanged(nameof(CategoryList)); } }
        private GetCategoryVM selectedCategory;
        public GetCategoryVM SelectedCategory { get => selectedCategory; set { selectedCategory = value; OnPropertyChanged(nameof(SelectedCategory)); } }
        #endregion
        public ICommand Search { get; }
        EnumSearch EnumSearch { get; set; }

        public ICommand UnSelectedLanguage { private set; get; }
        public ICommand UnSelectedCategory { private set; get; }
        ICategoryService CategoryService;
        ILanguageService LanguageService;
        public SearchPageVM(INavigation navigation, Page page, EnumSearch enumSearch) : base(navigation, page)
        {
            SearchVM = new SearchVM();
            Search = new Command(NaviguationTolistPage);
            EnumSearch = enumSearch;
            LanguageService = DependencyService.Get<ILanguageService>(DependencyFetchTarget.NewInstance);
            CategoryService = DependencyService.Get<ICategoryService>(DependencyFetchTarget.NewInstance);
            UnSelectedLanguage = new Command(execute: () =>
            {
                SelectedLanguage = null;
            });
            UnSelectedCategory = new Command(execute: () =>
            {
                SelectedCategory = null;
            });
        }

        private async void NaviguationTolistPage(object obj)
        {
            try
            {
                if (SelectedLanguage == null)
                {
                    SearchVM.LanguageIso = MotherLanguage;
                }
                if(SelectedLanguage != null)
                {
                    SearchVM.LanguageIso = SelectedLanguage.LanguageIso;
                }
                if (SelectedCategory != null)
                {
                    SearchVM.CategoryId = SelectedCategory.CategoryId;
                }
                switch (EnumSearch)
                {
                    case EnumSearch.Interest:
                        SearchVM.EnumSearch = EnumSearch.Interest;
                        await Navigation.PushAsync(new InterestListPage(SearchVM));
                        break;
                    case EnumSearch.Tour:
                        SearchVM.EnumSearch = EnumSearch.Tour;
                        await Navigation.PushAsync(new TourListPage(SearchVM));
                        break;
                    case EnumSearch.AllTour:
                        SearchVM.EnumSearch = EnumSearch.Tour;
                        await Navigation.PushAsync(new AllTourListPage(SearchVM));
                        break;
                    case EnumSearch.AllInterest:
                        SearchVM.EnumSearch = EnumSearch.Interest;
                        await Navigation.PushAsync(new AllInterestListPage(SearchVM));
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

        internal async void LoadCategoryAndLanguage()
        {
            try
            {
                switch (EnumSearch)
                {
                    case EnumSearch.Interest:
                        var listCate = await CategoryService.GetCategoryVM(MotherLanguage);
                        CategoryList = new ObservableCollection<GetCategoryVM>(listCate);
                        break;
                    case EnumSearch.Tour:
                        var listTour = await CategoryService.GetAllCategoryVMForTour(MotherLanguage);
                        CategoryList = new ObservableCollection<GetCategoryVM>(listTour);
                        break;
                    case EnumSearch.AllTour:
                        var listAllTour = await CategoryService.GetAllCategoryVMForTour(MotherLanguage);
                        CategoryList = new ObservableCollection<GetCategoryVM>(listAllTour);
                        break;
                    case EnumSearch.AllInterest:
                        var listAllInterest = await CategoryService.GetAllCategoryVMForTour(MotherLanguage);
                        CategoryList = new ObservableCollection<GetCategoryVM>(listAllInterest);
                        break;
                    default:
                        break;
                }
                var languageList = await LanguageService.GetLanguageApp(UserId);
                LanguageList = new ObservableCollection<PreferedLanguageVM>(languageList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
