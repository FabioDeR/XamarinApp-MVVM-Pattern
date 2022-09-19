using Microsoft.AspNetCore.Components;
using TabiTravel.Data.VM.BookingVM;
using TabiTravel.ServiceUI.Interface;

namespace TabiTravel.UI.Pages.Tourist.BookingPage
{
    public partial class BookingDetail
    {
        [Inject] IBookingService BookingService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] protected Sotsera.Blazor.Toaster.IToaster Toaster { get; set; }
        [Parameter] public string Id { get; set; }

        public BookingDetailVM Item { get; set; } = new();
        public BookingVM BookingVM { get; set; } = new();

        private bool Loading { get; set; }

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
            Item = await BookingService.GetBookingDetailVM(Convert.ToInt32(Id), userid);
            StateHasChanged();
        }
        #endregion

    }
}
