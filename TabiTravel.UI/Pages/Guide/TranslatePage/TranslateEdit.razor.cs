using Microsoft.AspNetCore.Components;
using TabiTravel.Data.VM.TranslateVM;
using TabiTravel.ServiceUI.Interface;

namespace TabiTravel.UI.Pages.Guide.TranslatePage
{
    public partial class TranslateEdit
    {
        [Inject] ITranslateService TranslateService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] protected Sotsera.Blazor.Toaster.IToaster Toaster { get; set; }
        [Parameter] public string Id { get; set; }
        [Parameter] public string LanguageIso { get; set; }
        [Parameter] public string Enumint { get; set; }
        public List<string> LanguageNotUsed { get; set; } = new();
        public TranslateVM Item { get; set; } = new();


        protected override async Task OnInitializedAsync()
        {
            Guid userid = Guid.Parse("2046249e-1ab1-4c5f-8a08-42e1c9f07542");
            if (!string.IsNullOrWhiteSpace(LanguageIso))
            {
                Item = await TranslateService.GetTranslate(Convert.ToInt32(Id), Convert.ToInt32(Enumint), LanguageIso);
            }
            else
            {
                //Recuperer ID et le placé dans le bon element
                switch (Enumint)
                {
                    case "1":
                        Item.InterestId = Convert.ToInt32(Id);
                        break;
                    case "2":
                        Item.StepId = Convert.ToInt32(Id);
                        break;
                    case "3":
                        Item.TourId = Convert.ToInt32(Id);
                        break;
                }
            }
            //Charger la liste des langues non utilisées
            LanguageNotUsed = await TranslateService.GetLanguageNotUsed(Convert.ToInt32(Id), userid, Convert.ToInt32(Enumint));
        }

        protected async Task HandleValidRequest()
        {
            if (String.IsNullOrEmpty(LanguageIso)) //Post
            {
                var res = await TranslateService.Add(Item, Convert.ToInt32(Enumint));

                if (res.IsSuccessStatusCode)
                {
                    Item = new();
                    Toaster.Success("Item added");
                    NavigationManager.NavigateTo($"guide/translatelistoverview/{Id}/{Enumint}/EN");
                }
                else
                {
                    Toaster.Warning("Error during the save");
                }
            }
            else // Put
            {
                await TranslateService.Put(Item);
                Toaster.Info("Item modified");
                NavigationManager.NavigateTo($"guide/translatelistoverview/{Id}/{Enumint}/EN");
            }
        }
    }
}
