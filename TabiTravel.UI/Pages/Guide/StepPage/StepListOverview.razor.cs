using Microsoft.AspNetCore.Components;
using TabiTravel.Data.VM.StepVM;
using TabiTravel.ServiceUI.Interface;
using TabiTravel.UI.Pages.Guide.InterestPage;

namespace TabiTravel.UI.Pages.Guide.StepPage
{
    public partial class StepListOverview
    {
        [Inject] IStepService StepService { get; set; }
        [Inject] protected Sotsera.Blazor.Toaster.IToaster Toaster { get; set; }
        [Parameter] public string Tourid { get; set; }
        [Parameter] public string LanguageIso { get; set; }
        public StepListOverviewVM MyStepVM { get; set; } = new();


        private string _message = "No item(s)";
        private bool Loading { get; set; }
        public Guid UserId { get; set; }
        public int DayIncrement { get; set; }

        protected bool orderAscending = true;

        protected override async Task OnInitializedAsync()
        {
            Loading = true;
            await LoadList();
            Loading = false;
            DayIncrement = MyStepVM.NbDays + 1;
        }

        #region LoadList
        protected async Task LoadList()
        {
            UserId= Guid.Parse("2046249e-1ab1-4c5f-8a08-42e1c9f07542");
            LanguageIso = "EN";

            MyStepVM = await StepService.GetStepListOverviewVM(Convert.ToInt32(Tourid), LanguageIso);

            StateHasChanged();
        }
        #endregion
        
    #region Delete
    protected async void Delete(int id)
    {
        //Guid temporaire à changer plus tard
        Guid userid = Guid.Parse("ff9cadb0-f2c7-4d20-b039-64bdd330fce3");

        var res = await StepService.Delete(id, userid);
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
