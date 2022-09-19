using Microsoft.AspNetCore.Components;
using TabiTravel.Data.VM.InterestVM;
using TabiTravel.ServiceUI.Interface;

namespace TabiTravel.UI.Pages.Tourist.InterestPage
{
    public partial class InterestOverview
    {
        [Parameter] public string Id { get; set; }
        [Inject] IInterestService InterestService { get; set; }
        [Inject] protected Sotsera.Blazor.Toaster.IToaster Toaster { get; set; }

        public AddInterestVM Item { get; set; } = new();
        public string languageiso { get; set; } = string.Empty;
        public List<string> Categories { get; set; } = new();


        protected override async Task OnInitializedAsync()
        {
            languageiso = "EN";
            Guid userid = Guid.Parse("2046249e-1ab1-4c5f-8a08-42e1c9f07542");

            if (!string.IsNullOrWhiteSpace(Id))
            {
                //Charger l'objet
                Item = await InterestService.GetMyInterestVM(languageiso, Convert.ToInt32(Id));
                Categories = Item.Categories.Where(x => x.IsSelected == true).OrderBy(x => x.Name).Select(x => x.Name).ToList();
            }
        }
    }
}
