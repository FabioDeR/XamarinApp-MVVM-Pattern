using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TabiTravel.Data.DbModel;
using TabiTravel.Data.VM.CategoryVM;
using TabiTravel.Data.VM.TourVM;
using TabiTravel.ServiceUI.Interface;

namespace TabiTravel.UI.Pages.Guide.TourPage
{
    public partial class TourEdit
    {
        [Parameter] public string Id { get; set; }
        [Inject] ITourService TourService { get; set; }
        [Inject] protected Sotsera.Blazor.Toaster.IToaster Toaster { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] ICategoryService CategoryService { get; set; }
        [Inject] ICountryService CountryService { get; set; }


        public TourEditVM Item { get; set; } = new();
        public List<GetCategoryVM> Categories { get; set; } = new();
        public List<Country> Countries { get; set; } = new();
        public List<string> DayTranslated { get; set; } = new();
        public string languageiso { get; set; } = string.Empty;
        public string NoImageUrl { get; set; } = "http://localhost:6909/api/Image/interestimage/ca6fbce1-9cbb-4cfd-95f3-9c5ceaf97c97_nopicture.jpg";

        protected override async Task OnInitializedAsync()
        {
            languageiso = "EN";
            Guid userid = Guid.Parse("2046249e-1ab1-4c5f-8a08-42e1c9f07542");

            if (!string.IsNullOrWhiteSpace(Id))
            {
                //Charger l'objet + les catégories associées à celui-ci
                Item = await TourService.GetMyTourVM(userid, languageiso, Convert.ToInt32(Id));
                Categories = Item.Categories.OrderBy(x => x.Name).ToList();
            }
            else
            {
                //Charger toutes les catégories + NoImage
                Categories = (await CategoryService.GetAllCategoryVMForInterest(languageiso)).OrderBy(x => x.Name).ToList();
                Item.Tour.ImageUrl = NoImageUrl;

            }

            //Charler la liste des pays dans la bonne langue
            Countries = (await CountryService.GetAllWithLanguage(languageiso)).OrderBy(x => x.Name).ToList();
            Item.Tour.UserId = userid;
            Item.LanguageIso = languageiso;
        }

        #region SelectedImage
        private async Task SelectedImage(InputFileChangeEventArgs e)
        {
            var imageFiles = e.File;

            if (imageFiles != null)
            {
                var content = new MultipartFormDataContent();
                var stream = imageFiles.OpenReadStream();
                content.Add(new StreamContent(stream), "file", imageFiles.Name);
                string ImgUrl = await TourService.UploadTourImage(content);
                Item.Tour.ImageUrl = ImgUrl;
                StateHasChanged();
            }
        }
        #endregion

        protected async Task HandleValidRequest()
        {
            if (String.IsNullOrEmpty(Id)) //Post
            {
                Item.Categories = Categories;
                var res = await TourService.Add(Item);

                if (res.IsSuccessStatusCode)
                {
                    Item = new();
                    Toaster.Success("Item added");
                    NavigationManager.NavigateTo("guide/tourlistoverview");
                }
                else
                {
                    Toaster.Warning("Error during the save");
                }
            }
            else // Put
            {
                await TourService.Update(Item);
                Toaster.Info("Item modified");
                NavigationManager.NavigateTo("guide/tourlistoverview");
            }
        }
    }
}
