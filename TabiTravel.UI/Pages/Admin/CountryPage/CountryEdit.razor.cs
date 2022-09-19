using Microsoft.AspNetCore.Components;
using TabiTravel.Data.DbModel;
using TabiTravel.ServiceUI.Interface;
using TabiTravel.UI.Pages.Admin.LanguagePage;

namespace TabiTravel.UI.Pages.Admin.CountryPage
{
    public partial class CountryEdit
    {
        [Parameter] public string Id { get; set; }
        [Parameter] public string Language { get; set; }
        [Inject] ILanguageService LanguageService { get; set; }
        [Inject] ICountryService CountryService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] protected Sotsera.Blazor.Toaster.IToaster Toaster { get; set; }

        public FilterAdminPage FilterAdminPageItem { get; set; } = new();
        public Country Item { get; set; } = new();
        public List<Language> Languages { get; set; } = new();
        public List<Country> AllCountries { get; set; } = new();
        public List<Country> Countries { get; set; } = new();


        public bool IsShowCountries { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrWhiteSpace(Id))
            {
                Item = await CountryService.GetById(Convert.ToInt32(Id), Language);
            }

            Languages = await LanguageService.GetAll();
            AllCountries = await CountryService.GetAll();
            Countries = AllCountries.Where(x => x.LanguageCodeIso == "EN").ToList();
        }

        protected async Task CountryChoice()
        {
            if (!FilterAdminPageItem.IsExisting)
            {
                IsShowCountries = true;
            }
        }
        protected async Task HandleValidRequest()
        {
            if (String.IsNullOrEmpty(Id)) //Post
            {
                if (!FilterAdminPageItem.IsExisting)
                {
                    Item.CountryId = ((await CountryService.GetAllNoDeletedDate()).Max(x => x.CountryId)) + 1;
                }

                var res = await CountryService.Add(Item);

                if (res.IsSuccessStatusCode)
                {
                    Item = new();
                    Toaster.Success("Item added");
                    NavigationManager.NavigateTo("admin/countrylistoverview");
                }
                else
                {
                    Toaster.Warning("Error during the save");
                }
            }
            else // Put
            {
                await CountryService.Update(Item);
                Toaster.Info("Item modified");
                NavigationManager.NavigateTo("admin/countrylistoverview");
            }
        }
    }
}

