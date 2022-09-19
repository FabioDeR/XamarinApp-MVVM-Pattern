using Microsoft.AspNetCore.Components;
using TabiTravel.Data.DbModel;
using TabiTravel.ServiceUI.Interface;
using TabiTravel.UI.Pages.Admin.LanguagePage;

namespace TabiTravel.UI.Pages.Admin.UnitPage
{
    public partial class UnitEdit
    {
        [Parameter] public string Id { get; set; }
        [Parameter] public string Language { get; set; }
        [Inject] ILanguageService LanguageService { get; set; }
        [Inject] IUnitService UnitService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] protected Sotsera.Blazor.Toaster.IToaster Toaster { get; set; }

        public FilterAdminPage FilterAdminPageItem { get; set; } = new();
        public Unit Item { get; set; } = new();
        public List<Language> Languages { get; set; } = new();
        public List<Unit> AllUnits { get; set; } = new();
        public List<Unit> Units { get; set; } = new();
        public int LastCategoryId { get; set; }

        public bool IsShowUnit { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrWhiteSpace(Id))
            {
                Item = await UnitService.GetById(Convert.ToInt32(Id), Language);
            }

            Languages = await LanguageService.GetAll();
            AllUnits = await UnitService.GetAll();
            Units = AllUnits.Where(x => x.LanguageCodeIso == "EN").ToList();
        }

        protected async Task UnitChoice()
        {
            if (!FilterAdminPageItem.IsExisting)
            {
                IsShowUnit = true;
            }
        }

        protected async Task HandleValidRequest()
        {
            if (String.IsNullOrEmpty(Id)) //Post
            {
                if (!FilterAdminPageItem.IsExisting)
                {
                    Item.UnitId = (await UnitService.GetAllNoDeletedDate()).Max(x => x.UnitId) + 1;
                }

                var res = await UnitService.Add(Item);

                if (res.IsSuccessStatusCode)
                {
                    Item = new();
                    Toaster.Success("Item added");
                    NavigationManager.NavigateTo("admin/unitlistoverview");
                }
                else
                {
                    Toaster.Warning("Error during the save");
                }
            }
            else // Put
            {
                await UnitService.Update(Item);
                Toaster.Info("Item modified");
                NavigationManager.NavigateTo("admin/unitlistoverview");
            }
        }
    }
}
