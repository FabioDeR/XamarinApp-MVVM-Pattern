using Microsoft.AspNetCore.Components;
using TabiTravel.Data.DbModel;
using TabiTravel.ServiceUI.Interface;
using TabiTravel.UI.Pages.Admin.LanguagePage;

namespace TabiTravel.UI.Pages.Admin.DayPage
{
    public partial class DayListOverview
    {
        [Inject] IDayService DayService { get; set; }
        [Inject] ILanguageService LanguageService { get; set; }
        [Inject] protected Sotsera.Blazor.Toaster.IToaster Toaster { get; set; }
        public List<Language> Languages { get; set; }
        public List<Day> Days { get; set; }
        public List<Day> AllDays { get; set; }
        public FilterAdminPage FilterAdminPageItem { get; set; } = new();

        private string _message = "No item(s)";
        private bool Loading { get; set; }
        protected bool orderAscending = true;

        protected override async Task OnInitializedAsync()
        {
            Loading = true;
            await LoadList();
            Loading = false;
            AllDays = Days;
        }

        #region LoadList
        protected async Task LoadList()
        {
            FilterAdminPageItem.Filter = "";
            FilterAdminPageItem.Language = "0";
            Languages = (await LanguageService.GetAll()).OrderBy(x => x.Name).ToList();
            Days = (await DayService.GetAll()).OrderBy(x => x.LanguageCodeIso).ThenBy(x => x.Name).ToList();
            StateHasChanged();
        }
        #endregion

        #region Filter
        protected void Filter()
        {
            string filter = FilterAdminPageItem.Filter.ToLower();
            string iso = FilterAdminPageItem.Language;

            //Si filtre nom + langue
            if (!(String.IsNullOrWhiteSpace(filter)) && iso != "0")
            {
                Days = AllDays.Where(x => x.Name.ToLower().Contains(filter) && x.LanguageCodeIso == iso).OrderBy(x => x.LanguageCodeIso).ThenBy(x => x.Name).ToList();
                StateHasChanged();
            }

            //Si juste filtre nom
            if (!(String.IsNullOrWhiteSpace(filter)) && iso == "0")
            {
                Days = AllDays.Where(x => x.Name.ToLower().Contains(filter)).OrderBy(x => x.LanguageCodeIso).ThenBy(x => x.Name).ToList();
                StateHasChanged();
            }

            // Si juste langue
            if (String.IsNullOrWhiteSpace(filter) && iso != "0")
            {
                Days = AllDays.Where(x => x.LanguageCodeIso == iso).OrderBy(x => x.LanguageCodeIso).ThenBy(x => x.Name).ToList();
                StateHasChanged();
            }
        }
        #endregion

        #region OrderBy
        protected void SortBy(string type)
        {
            orderAscending = !orderAscending;

            if (orderAscending)
            {
                switch (type)
                {
                    case "name":
                        Days = Days.OrderBy(c => c.Name).ToList();
                        break;
                    case "language":
                        Days = Days.OrderBy(c => c.Language.Name).ToList();
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case "name":
                        Days = Days.OrderByDescending(c => c.Name).ToList();
                        break;
                    case "language":
                        Days = Days.OrderByDescending(c => c.Language.Name).ToList();
                        break;
                }
            }
            StateHasChanged();
        }
        #endregion

        #region Delete
        protected async void Delete(int id, string iso)
        {
            //Guid temporaire à changer plus tard
            Guid userid = Guid.Parse("ff9cadb0-f2c7-4d20-b039-64bdd330fce3");

            var res = await DayService.Delete(id, iso, userid);
            if (res.IsSuccessStatusCode)
            {
                Toaster.Success("Item deleted");
                await LoadList();
            }
            else
            {
                Toaster.Warning("Error during delete");
            }
        }
        #endregion
    }
}
