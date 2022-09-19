using Microsoft.AspNetCore.Components;
using TabiTravel.Data.VM.TranslateVM;
using TabiTravel.ServiceUI.Interface;

namespace TabiTravel.UI.Pages.Guide.TranslatePage
{
    public partial class TranslateListOverview
    {
        [Inject] ITranslateService TranslateService { get; set; }
        [Inject] protected Sotsera.Blazor.Toaster.IToaster Toaster { get; set; }
        [Parameter] public string Id { get; set; }
        [Parameter] public string Enumint { get; set; }
        [Parameter] public string MotherLanguage { get; set; }
        public List<TranslateVM> TranslateVMs { get; set; } = new();
        private bool Loading { get; set; }
        private string _message = "No item(s)";
        protected bool orderAscending = true;
        public int TourId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Loading = true;
            await LoadList();
            Loading = false;

            //Pour redirect
            TourId = TranslateVMs[0].TourId;
        }

        #region LoadList
        protected async Task LoadList()
        {
            TranslateVMs = await TranslateService.GetAllTranslate(Convert.ToInt32(Id), Convert.ToInt32(Enumint), MotherLanguage);
            StateHasChanged();
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
                    case "language":
                        TranslateVMs = TranslateVMs.OrderBy(c => c.LanguageIso).ToList();
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case "language":
                        TranslateVMs = TranslateVMs.OrderByDescending(c => c.LanguageIso).ToList();
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
            switch (Enumint)
            {
                case "1": 
                    var res = await TranslateService.HardDeleteInterest(id, iso);
                    if (res.IsSuccessStatusCode)
                    {
                        Toaster.Success("Item deleted");
                        await LoadList();
                    }
                    else
                    {
                        Toaster.Warning("Error during delete");
                    }
                    break;
                case "2":
                    var res2 = await TranslateService.HardDeleteStep(id, iso);
                    if (res2.IsSuccessStatusCode)
                    {
                        Toaster.Success("Item deleted");
                        await LoadList();
                    }
                    else
                    {
                        Toaster.Warning("Error during delete");
                    }
                    break;
                case "3":
                    var res3 = await TranslateService.HardDeleteTour(id, iso);
                    if (res3.IsSuccessStatusCode)
                    {
                        Toaster.Success("Item deleted");
                        await LoadList();
                    }
                    else
                    {
                        Toaster.Warning("Error during delete");
                    }
                    break;
            }
        }
        #endregion
    }
}
