using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using TabiTravel.Data.VM.BookingVM;
using TabiTravel.ServiceUI.Interface;

namespace TabiTravel.UI.Pages.Tourist.BookingPage
{
    public partial class BookingTour
    {
        [Inject] public IBookingService BookingService { get; set; }
        [Inject] public IStepService StepService { get; set; }
        [Inject] protected Sotsera.Blazor.Toaster.IToaster Toaster { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Parameter] public string Id { get; set; }
        public bool IsShowVehicule { get; set; } = false;

        public PreBookingVM Item { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            Guid guideid = Guid.Parse("2046249e-1ab1-4c5f-8a08-42e1c9f07542");
            Guid touristid = Guid.Parse("1046249e-1ab1-4c5f-8a08-42e1c9f07542");

            if (!string.IsNullOrWhiteSpace(Id))
            {
                IsShowVehicule = (await StepService.GetStepListOverviewVM(Convert.ToInt32(Id), "EN")).IsShowEmplacement; 
                Item.TourId = Convert.ToInt32(Id);
                Item.TouristId = touristid;
                Item.GuideId = guideid;
                Item.DateStart = DateTime.Now;
                StateHasChanged();
            }
        }

        protected async Task HandleValidRequest()
        {

            var res = await BookingService.ErrorList(Item);

            if (res.IsSuccessStatusCode)
            {
                var res2 = await BookingService.Add(Item);
                if (res2.IsSuccessStatusCode)
                {
                    Item = new();
                    Toaster.Success("Item sended");
                    NavigationManager.NavigateTo("tourist/index");
                }
                else
                {
                    Item = new();
                    Toaster.Warning("Error");
                }
            }
            else
            {
                List<bool> boolList = JsonConvert.DeserializeObject<List<bool>>(await res.Content.ReadAsStringAsync());
                Item.ErrorMessage = "";
                Toaster.Warning("Item sended");
                if (boolList[0])
                {
                    Item.ErrorMessage += "\n- Guide no available for this period";
                }
                if (boolList[1])
                {
                    Item.ErrorMessage += "\n- Interest no available for this period";
                }
                if (boolList[2])
                {
                    Item.ErrorMessage += "\n- Emplacement no available for this period";
                }
            }
        }
    }
}
