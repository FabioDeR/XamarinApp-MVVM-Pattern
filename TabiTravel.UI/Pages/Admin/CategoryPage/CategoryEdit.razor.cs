using Microsoft.AspNetCore.Components;
using TabiTravel.Data.DbModel;
using TabiTravel.ServiceUI.Interface;
using TabiTravel.UI.Pages.Admin.LanguagePage;

namespace TabiTravel.UI.Pages.Admin.CategoryPage
{
    public partial class CategoryEdit
    {
        [Parameter] public string Id { get; set; }
        [Parameter] public string Language { get; set; }
        [Inject] ILanguageService LanguageService { get; set; }
        [Inject] ICategoryService CategoryService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] protected Sotsera.Blazor.Toaster.IToaster Toaster { get; set; }

        public FilterAdminPage FilterAdminPageItem { get; set; } = new();
        public Category Item { get; set; } = new();
        public List<Language> Languages { get; set; } = new();
        public List<Category> AllCategories { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public int LastCategoryId { get; set; } 

        public bool IsShowCategories { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrWhiteSpace(Id))
            {
                Item = await CategoryService.GetById(Convert.ToInt32(Id), Language);
            }

            Languages = await LanguageService.GetAll();
            AllCategories = await CategoryService.GetAll();
            Categories = AllCategories.Where(x => x.LanguageCodeIso == "EN").ToList();
        }

        protected async Task CategoryChoice()
        {
            if (!FilterAdminPageItem.IsExisting)
            {
                IsShowCategories = true;
            }
        }
        protected async Task HandleValidRequest()
        {
            if (String.IsNullOrEmpty(Id)) //Post
            {
                if (!FilterAdminPageItem.IsExisting)
                {
                    Item.CategoryId = (await CategoryService.GetAllNoDeletedDate()).Max(x => x.CategoryId) + 1;
                }
                
                var res = await CategoryService.Add(Item);

                if (res.IsSuccessStatusCode)
                {
                    Item = new();
                    Toaster.Success("Item added");
                    NavigationManager.NavigateTo("admin/categorylistoverview");
                }
                else
                {
                    Toaster.Warning("Error during the save");
                }
            }
            else // Put
            {
                await CategoryService.Update(Item);
                Toaster.Info("Item modified");
                NavigationManager.NavigateTo("admin/categorylistoverview");
            }
        }
    }
}
