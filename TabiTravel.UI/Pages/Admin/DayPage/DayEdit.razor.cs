using Microsoft.AspNetCore.Components;
using TabiTravel.Data.DbModel;
using TabiTravel.ServiceUI.Interface;
using TabiTravel.UI.Pages.Admin.LanguagePage;

namespace TabiTravel.UI.Pages.Admin.DayPage
{
    public partial class DayEdit
    {
        [Parameter] public string Id { get; set; }
        [Parameter] public string Language { get; set; }
        [Inject] ILanguageService LanguageService { get; set; }
        [Inject] IDayService DayService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] protected Sotsera.Blazor.Toaster.IToaster Toaster { get; set; }

        public FilterAdminPage FilterAdminPageItem { get; set; } = new();
        public Day Item { get; set; } = new();
        public List<Language> Languages { get; set; } = new();
        public List<Day> AllDays { get; set; } = new();
        public List<Day> Days { get; set; } = new();


        public bool IsShowCountries { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrWhiteSpace(Id))
            {
                Item = await DayService.GetById(Convert.ToInt32(Id), Language);
            }

            Languages = await LanguageService.GetAll();
            AllDays = await DayService.GetAll();
            Days = AllDays.Where(x => x.LanguageCodeIso == "EN").ToList();
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
                    Item.DayId = ((await DayService.GetAllNoDeletedDate()).Max(x => x.DayId)) + 1;
                }

                var res = await DayService.Add(Item);

                if (res.IsSuccessStatusCode)
                {
                    Item = new();
                    Toaster.Success("Item added");
                    NavigationManager.NavigateTo("admin/daylistoverview");
                }
                else
                {
                    Toaster.Warning("Error during the save");
                }
            }
            else // Put
            {
                await DayService.Update(Item);
                Toaster.Info("Item modified");
                NavigationManager.NavigateTo("admin/daylistoverview");
            }
        }
    }
}
