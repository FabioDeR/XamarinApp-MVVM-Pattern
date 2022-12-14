using Microsoft.AspNetCore.Components;
using TabiTravel.Data.DbModel;
using TabiTravel.ServiceUI.Interface;
using TabiTravel.UI.Pages.Admin.LanguagePage;

namespace TabiTravel.UI.Pages.Admin.UnitPage
{
    public partial class UnitListOverview
    {
        [Inject] IUnitService UnitService { get; set; }
        [Inject] ILanguageService LanguageService { get; set; }
        [Inject] protected Sotsera.Blazor.Toaster.IToaster Toaster { get; set; }
        public List<Language> Languages { get; set; }
        public List<Unit> Units { get; set; }
        public List<Unit> AllUnits { get; set; }
        public FilterAdminPage FilterAdminPageItem { get; set; } = new();

        private string _message = "No item(s)";
        private bool Loading { get; set; }
        protected bool orderAscending = true;

        protected override async Task OnInitializedAsync()
        {
            Loading = true;
            await LoadList();
            Loading = false;
            AllUnits = Units;
        }

        #region LoadList
        protected async Task LoadList()
        {
            FilterAdminPageItem.Filter = "";
            FilterAdminPageItem.Language = "0";
            Languages = (await LanguageService.GetAll()).OrderBy(x => x.Name).ToList();
            Units = (await UnitService.GetAll()).OrderBy(x => x.LanguageCodeIso).ThenBy(x => x.Name).ToList();
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
                Units = AllUnits.Where(x => x.Name.ToLower().Contains(filter) && x.LanguageCodeIso == iso).OrderBy(x => x.LanguageCodeIso).ThenBy(x => x.Name).ToList();
                StateHasChanged();
            }

            //Si juste filtre nom
            if (!(String.IsNullOrWhiteSpace(filter)) && iso == "0")
            {
                Units = AllUnits.Where(x => x.Name.ToLower().Contains(filter)).OrderBy(x => x.LanguageCodeIso).ThenBy(x => x.Name).ToList();
                StateHasChanged();
            }

            // Si juste langue
            if (String.IsNullOrWhiteSpace(filter) && iso != "0")
            {
                Units = AllUnits.Where(x => x.LanguageCodeIso == iso).OrderBy(x => x.LanguageCodeIso).ThenBy(x => x.Name).ToList();
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
                        Units = Units.OrderBy(c => c.Name).ToList();
                        break;
                    case "language":
                        Units = Units.OrderBy(c => c.Language.Name).ToList();
                        break;
                    case "abbreviation":
                        Units = Units.OrderBy(c => c.Abbreviation).ToList();
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case "name":
                        Units = Units.OrderByDescending(c => c.Name).ToList();
                        break;
                    case "language":
                        Units = Units.OrderByDescending(c => c.Language.Name).ToList();
                        break;
                    case "abbreviation":
                        Units = Units.OrderByDescending(c => c.Abbreviation).ToList();
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

            var res = await UnitService.Delete(id, iso, userid);
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
