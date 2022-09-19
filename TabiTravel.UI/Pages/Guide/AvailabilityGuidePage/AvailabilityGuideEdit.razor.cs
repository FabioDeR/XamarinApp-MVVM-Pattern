using Microsoft.AspNetCore.Components;
using TabiTravel.Data.VM.AvailabilityGuideVM;
using TabiTravel.ServiceUI.Interface;

namespace TabiTravel.UI.Pages.Guide.AvailabilityGuidePage
{
    public partial class AvailabilityGuideEdit
    {
        [Inject] IAvailabilityGuideService AvailabilityGuideService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] protected Sotsera.Blazor.Toaster.IToaster Toaster { get; set; }
        public List<DayListVM> Item { get; set; } = new();

        private bool Loading { get; set; }
        public string LanguageIso { get; set; } = "EN";

        protected override async Task OnInitializedAsync()
        {
            Loading = true;
            await LoadList();
            Loading = false;
        }

        #region LoadList
        protected async Task LoadList()
        {
            Guid userid = Guid.Parse("2046249e-1ab1-4c5f-8a08-42e1c9f07542");
            Item = await AvailabilityGuideService.GetAvailabilityGuideVM(userid, LanguageIso);
            StateHasChanged();
        }
        #endregion

        #region Put
        public async Task Put()
        {

            var res = await AvailabilityGuideService.PutAvailabilityGuideVM(Item);
            if (res.IsSuccessStatusCode)
            {
                Item = new();
                Toaster.Success("Item added");
                NavigationManager.NavigateTo("guide/index");
            }
            else
            {
                Toaster.Warning("Error during the save");
            }

        }
        #endregion


    }
}
