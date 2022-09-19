using Microsoft.AspNetCore.Components;
using TabiTravel.Data.VM.CategoryVM;
using TabiTravel.Data.VM.StepVM;
using TabiTravel.Data.VM.TourVM;
using TabiTravel.ServiceUI.Interface;

namespace TabiTravel.UI.Pages.Tourist.TourPage
{
    public partial class TourOverview
    {
        [Parameter] public string Id { get; set; }
        [Inject] ITourService TourService { get; set; }
        [Inject] IStepService StepService { get; set; }
        [Inject] protected Sotsera.Blazor.Toaster.IToaster Toaster { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }

        public TourEditVM Item { get; set; } = new();
        public string languageiso { get; set; } = string.Empty;
        public List<string> Categories { get; set; } = new();
        public StepListOverviewVM MyStepVM { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            languageiso = "EN";
            Guid userid = Guid.Parse("2046249e-1ab1-4c5f-8a08-42e1c9f07542");

            if (!string.IsNullOrWhiteSpace(Id))
            {
                //Charger l'objet
                Item = await TourService.GetMyTourVM(userid, languageiso, Convert.ToInt32(Id));
                Categories = Item.Categories.Where(x => x.IsSelected == true).OrderBy(x => x.Name).Select(x => x.Name).ToList();
                MyStepVM = await StepService.GetStepListOverviewVM(Convert.ToInt32(Item.Tour.Id), languageiso);
            }
        }

        protected async Task Reserve(int tourid)
        {

        }
    }
}
