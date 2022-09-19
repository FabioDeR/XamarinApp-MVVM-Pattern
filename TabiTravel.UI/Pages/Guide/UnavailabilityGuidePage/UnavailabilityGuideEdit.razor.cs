using Microsoft.AspNetCore.Components;
using TabiTravel.Data.VM.UnavailabilityGuide;
using TabiTravel.ServiceUI.Interface;

namespace TabiTravel.UI.Pages.Guide.UnavailabilityGuidePage
{
    public partial class UnavailabilityGuideEdit
    {
        [Inject] IUnavailabilityGuideService UnavailabilityGuideService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] protected Sotsera.Blazor.Toaster.IToaster Toaster { get; set; }
        [Parameter] public string Id { get; set; }
        public Guid userid { get; set; } = Guid.Parse("2046249e-1ab1-4c5f-8a08-42e1c9f07542");

        public UnavailabilityGuideEditVM Item { get; set; } = new();

        private bool Loading { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrWhiteSpace(Id))
            {
                Item = await UnavailabilityGuideService.GetUnavailabilityGuideVMById(userid, Convert.ToInt32(Id));
            }
            else
            {
                Item.DateStart = DateTime.Now;
                Item.DateEnd = DateTime.Now;
            }
            StateHasChanged();
        }

        protected async Task HandleValidRequest()
        {
            if (String.IsNullOrEmpty(Id)) //Post
            {
                Item.UserId = userid;
                var res = await UnavailabilityGuideService.Add(Item);

                if (res.IsSuccessStatusCode)
                {
                    Item = new();
                    Toaster.Success("Item added");
                    NavigationManager.NavigateTo("guide/unavailabilityguidelistoverview");
                }
                else
                {
                    Toaster.Warning("Error during the save");
                }
            }
            else // Put
            {
                await UnavailabilityGuideService.Put(Item);
                Toaster.Info("Item modified");
                NavigationManager.NavigateTo("guide/unavailabilityguidelistoverview");
            }
        }

    }
}
