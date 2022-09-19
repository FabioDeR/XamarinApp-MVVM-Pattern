using BrowserInterop.Extensions;
using BrowserInterop.Geolocation;
using FisSst.BlazorMaps;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using TabiTravel.Data.DbModel;
using TabiTravel.Data.VM.CategoryVM;
using TabiTravel.Data.VM.InterestVM;
using TabiTravel.ServiceUI.Interface;

namespace TabiTravel.UI.Pages.Guide.InterestPage
{
    public partial class InterestEdit
    {
        [Parameter] public string Id { get; set; }
        [Inject] IInterestService InterestService { get; set; }
        [Inject] protected Sotsera.Blazor.Toaster.IToaster Toaster { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] ICategoryService CategoryService { get; set; }
        [Inject] ICountryService CountryService { get; set; }
        [Inject] IDayService DayService { get; set; }
        [Inject] IJSRuntime jsRuntime { get; set; }
        [Inject] private IMarkerFactory MarkerFactory { get; set; }


        public AddInterestVM Item { get; set; } = new();
        public List<GetCategoryVM> Categories { get; set; } = new();
        public List<Country> Countries { get; set; } = new();
        public List<string> DayTranslated { get; set; } = new();
        public string languageiso { get; set; } = string.Empty;
        public string NoImageUrl { get; set; } = "http://localhost:6909/api/Image/interestimage/ca6fbce1-9cbb-4cfd-95f3-9c5ceaf97c97_nopicture.jpg";

        //Map
        public bool IsShowMarker { get; set; }
        private Map mapRef;
        private MapOptions mapOptions = new();
        private MarkerOptions markerOptions { get; init; }
        private Marker marker1 { get; set; }
        private LatLng firstMarkerLatLng { get; set; } = new();
        private LatLng center { get; set; } = new();

        //Localisation (BrowserInterop)
        private WindowNavigatorGeolocation geolocationWrapper { get; set; }
        private GeolocationResult currentPosition { get; set; }

        public InterestEdit()
        {
            this.center = new LatLng(50.279133, 18.685578);
            this.firstMarkerLatLng = new LatLng(50.279133, 18.685578);
            this.mapOptions = new MapOptions()
            {
                DivId = "mapId",
                Center = center,
                Zoom = 17,
                UrlTileLayer = "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",
                SubOptions = new MapSubOptions()
                {
                    Attribution = "&copy; <a lhref='http://www.openstreetmap.org/copyright'>OpenStreetMap</a>",
                    MaxZoom = 17,
                    TileSize = 512,
                    ZoomOffset = -1,
                }
            };
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                languageiso = "EN";
                Guid userid = Guid.Parse("2046249e-1ab1-4c5f-8a08-42e1c9f07542");

                if (!string.IsNullOrWhiteSpace(Id))
                {
                    //Charger l'objet + les catégories associées à celui-ci
                    Item = await InterestService.GetMyInterestVM(languageiso, Convert.ToInt32(Id));
                    Categories = Item.Categories.OrderBy(x => x.Name).ToList();
                    await MapGetLatLgn();

                }
                else
                {
                    //Charger toutes les catégories + NoImage
                    Categories = (await CategoryService.GetAllCategoryVMForInterest(languageiso)).OrderBy(x => x.Name).ToList();
                    Item.Interest.ImageUrl = NoImageUrl;

                    //Si nouveau interest, charger la latitude et longitude
                    await GetGeolocation();
                    await MapGetLatLgn();

                    //Charger les jours dans la bonne langue
                    DayTranslated = (await DayService.GetAllBylanguageIso(languageiso)).ToList();
                    Item.Monday = DayTranslated[0];
                    Item.Tuesday = DayTranslated[1];
                    Item.Wednesday = DayTranslated[2];
                    Item.Thursday = DayTranslated[3];
                    Item.Friday = DayTranslated[4];
                    Item.Saturday = DayTranslated[5];
                    Item.Sunday = DayTranslated[6];
                }

                //Charler la liste des pays dans la bonne langue
                Countries = (await CountryService.GetAllWithLanguage(languageiso)).OrderBy(x => x.Name).ToList();
                Item.Interest.UserId = userid;
                Item.LanguageIso = languageiso;
                StateHasChanged();
            }
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
                string ImgUrl = await InterestService.UploadInterestImage(content);
                Item.Interest.ImageUrl = ImgUrl;
                StateHasChanged();
            }
        }
        #endregion

        #region Get Location
        public async Task GetGeolocation()
        {
            var window = await jsRuntime.Window();
            var navigator = await window.Navigator();
            geolocationWrapper = navigator.Geolocation;

            currentPosition = await geolocationWrapper.GetCurrentPosition();
            Item.Interest.Latitude = currentPosition.Location.Coords.Latitude;
            Item.Interest.Longitude = currentPosition.Location.Coords.Longitude;
            StateHasChanged();

        }
        #endregion

        #region Map GetLatLgn
        public async Task MapGetLatLgn()
        {
            mapOptions.Center = (new LatLng(Item.Interest.Latitude, Item.Interest.Longitude));
            firstMarkerLatLng.Lng = Item.Interest.Longitude;
            firstMarkerLatLng.Lat = Item.Interest.Latitude;
            StateHasChanged();
        }
        #endregion

        #region ShowMarker
        private async Task ShowMarker(bool state)
        {
            firstMarkerLatLng.Lng = Item.Interest.Longitude;
            firstMarkerLatLng.Lat = Item.Interest.Latitude;
            if (state)
            {
                await this.marker1.Remove();
                IsShowMarker = false;
            }
            else
            {
                this.marker1 = await this.MarkerFactory.CreateAndAddToMap(this.firstMarkerLatLng, this.mapRef);
                IsShowMarker = true;
            }
        }
        #endregion


        protected async Task HandleValidRequest()
        {
            if (String.IsNullOrEmpty(Id)) //Post
            {
                Item.Categories = Categories;
                var res = await InterestService.Add(Item);

                if (res.IsSuccessStatusCode)
                {
                    Item = new();
                    Toaster.Success("Item added");
                    NavigationManager.NavigateTo("guide/interestlistoverview");
                }
                else
                {
                    Toaster.Warning("Error during the save");
                }
            }
            else // Put
            {
                await InterestService.Update(Item);
                Toaster.Info("Item modified");
                NavigationManager.NavigateTo("guide/interestlistoverview");
            }
        }
    }
}
