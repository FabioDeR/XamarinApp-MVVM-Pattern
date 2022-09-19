using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TabiTravel.Data.DbModel;
using TabiTravel.ServiceUI.Interface;

namespace TabiTravel.UI.Pages.Admin.LanguagePage
{
    public partial class LanguageListOverview
    {

        [Inject] ILanguageService LanguageService { get; set; }
        [Inject] protected Sotsera.Blazor.Toaster.IToaster Toaster { get; set; }
        public List<Language> Languages { get; set; }
        public List<Language> AllLanguages { get; set; }
        public FilterAdminPage FilterAdminPageItem { get; set; } = new();

        private string _message = "No item(s)";
        private bool Loading { get; set; }
        protected bool orderAscending = true;

        protected override async Task OnInitializedAsync()
        {
            Loading = true;
            await LoadList();
            Loading = false;
            AllLanguages = Languages;
        }

        #region LoadList
        protected async Task LoadList()
        {
            FilterAdminPageItem.Filter = "";
            Languages = (await LanguageService.GetAll()).OrderBy(x => x.Name).ToList();
            StateHasChanged();
        }

        #endregion

        #region Filter
        protected void FilterIso()
        {
            string filter = FilterAdminPageItem.Filter.ToLower();

            if (filter.Count() > 0)
            {
                Languages = AllLanguages.Where(x => x.ISOCode.ToLower().Contains(filter)).ToList();
                StateHasChanged();
            }
        }
        #endregion

        #region Delete
        protected async void Delete(string iso)
        {
            //Guid temporaire à changer plus tard
            Guid userid = Guid.Parse("ff9cadb0-f2c7-4d20-b039-64bdd330fce3");

            var res = await LanguageService.Delete(iso, userid);
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

        #region OrderBy
        protected void SortBy(string type)
        {
            orderAscending = !orderAscending;

            if (orderAscending)
            {
                switch (type)
                {
                    case "name":
                        Languages = Languages.OrderBy(c => c.Name).ToList();
                        break;
                    case "iso":
                        Languages = Languages.OrderBy(c => c.ISOCode).ToList();
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case "name":
                        Languages = Languages.OrderByDescending(c => c.Name).ToList();
                        break;
                    case "iso":
                        Languages = Languages.OrderByDescending(c => c.ISOCode).ToList();
                        break;
                }
            }
            StateHasChanged();
        }
        #endregion
    }

    public class FilterAdminPage
    {
        public string Filter { get; set; }
        public string Language { get; set; }
        public bool IsExisting { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please select a unit")]
        public int UnitId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please select a country")]
        public int CountryId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please select a day")]
        public int DayId { get; set; }
    }
}
