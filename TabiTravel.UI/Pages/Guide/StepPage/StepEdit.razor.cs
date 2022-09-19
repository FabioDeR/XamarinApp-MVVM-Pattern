using Microsoft.AspNetCore.Components;
using TabiTravel.Data.DbModel;
using TabiTravel.Data.VM.InterestVM;
using TabiTravel.Data.VM.StepVM;
using TabiTravel.Data.VM.UnitVM;
using TabiTravel.ServiceUI.Interface;

namespace TabiTravel.UI.Pages.Guide.StepPage
{
    public partial class StepEdit
    {
        [Inject] IStepService StepService { get; set; }
        [Inject] IInterestService InterestService { get; set; }
        [Inject] IUnitService UnitService { get; set; }
        [Inject] protected Sotsera.Blazor.Toaster.IToaster Toaster { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Parameter] public string TourId { get; set; }
        [Parameter] public string Day { get; set; }
        [Parameter] public string StepId { get; set; }


        public StepEditVM Item { get; set; } = new();
        public List<InterestListVM> InterestListVMs { get; set; } = new();
        public List<GetUnitVM> UnitVMs { get; set; } = new();
        public string LanguageIso { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            LanguageIso = "EN";
            Guid userid = Guid.Parse("2046249e-1ab1-4c5f-8a08-42e1c9f07542");

            if (!string.IsNullOrWhiteSpace(StepId)) 
            {
                //Charger l'objet
                Item = await StepService.GetStepEditVM(Convert.ToInt32(StepId), LanguageIso, userid);
            }
            else //New
            {
                //Remplir l'objet avec les elements recus en parametres
                Item.TourId = Convert.ToInt32(TourId);
                Item.LanguageIso = LanguageIso;
                Item.Day = Convert.ToInt32(Day);
            }

            //Charler la liste des interets et unité
            InterestListVMs = (await InterestService.GetInterestListVMs(userid)).OrderBy(x => x.Name).ToList();
            UnitVMs = (await UnitService.GetUnitVM(LanguageIso)).OrderBy(x => x.Name).ToList();
        }

        protected async Task HandleValidRequest()
        {
            if (String.IsNullOrEmpty(StepId)) //Post
            {
                var res = await StepService.Add(Item);

                if (res.IsSuccessStatusCode)
                {
                    Item = new();
                    Toaster.Success("Item added");
                    NavigationManager.NavigateTo($"guide/steplistoverview/{TourId}/{LanguageIso}");
                }
                else
                {
                    Toaster.Warning("Error during the save");
                }
            }
            else // Put
            {
                await StepService.Update(Item);
                Toaster.Info("Item modified");
                NavigationManager.NavigateTo($"guide/steplistoverview/{Item.TourId}/{LanguageIso}");
            }
        }
    }
}
