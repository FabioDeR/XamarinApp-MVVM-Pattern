using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using TabiTravel.Data.DbModel;
using TabiTravel.ServiceUI.Interface;

namespace TabiTravel.UI.Pages.Admin.LanguagePage
{
    public partial class LanguageEdit
    {
        [Parameter] public string Id { get; set; }
        [Inject] ILanguageService LanguageService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] protected Sotsera.Blazor.Toaster.IToaster Toaster { get; set; }
        public Language Item { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrWhiteSpace(Id))
            {
                Item = await LanguageService.GetById(Id);
            }
        }

        protected async Task HandleValidRequest()
        {
            if (String.IsNullOrEmpty(Id)) // Post
            {
                var res = await LanguageService.Add(Item);

                if (res.IsSuccessStatusCode)
                {
                    Item = new();
                    Toaster.Success("Élément ajouté");
                    NavigationManager.NavigateTo("admin/languagelistoverview");
                }
                else
                {
                    Toaster.Warning("Erreur lors de l'enregistrement");
                }
            }
            else // Put
            {
                await LanguageService.Update(Item);
                Toaster.Info("Élément modifié");
                NavigationManager.NavigateTo("admin/languagelistoverview");
            }
        }
    }
}
