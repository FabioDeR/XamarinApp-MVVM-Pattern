using Mapsui;
using Mapsui.Fetcher;
using Mapsui.Projection;
using Mapsui.UI;
using Mapsui.UI.Forms;
using Mapsui.UI.Objects;
using Mapsui.Utilities;
using Mapsui.Widgets;
using Mapsui.Widgets.ScaleBar;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.XamarinAPP.ViewModels;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Pin = Mapsui.UI.Forms.Pin;
using PinClickedEventArgs = Mapsui.UI.Forms.PinClickedEventArgs;
using Position = Mapsui.UI.Forms.Position;

namespace TabiTravel.XamarinAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public double Latitude { get; set; } = 0;
        public double Longitude { get; set; } = 0;
        private readonly Geocoder _geocoder = new Geocoder();
        protected bool PinAlreadyClicked { get; set; }

        public MapPage()
        {
            try
            {
                InitializeComponent();
                AbsoluImageButton.IsVisible = true;
                StackInfoPin.IsVisible = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public Position UserLocation { get; private set; }

        protected async override void OnAppearing()
        {
            try
            {
                MapSuiVM mapSuiVM = new MapSuiVM(Navigation, this);
                BindingContext = mapSuiVM;
                mapView.Map = mapSuiVM.MapVM;
                var location = CrossGeolocator.Current;
                try
                {
                    var position = await location.GetPositionAsync();
                    Console.WriteLine(position);
                    UserLocation = new Position(position.Latitude, position.Longitude);
                    Latitude = position.Latitude;
                    Longitude = position.Longitude;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                mapView.RotationLock = false;
                mapView.UnSnapRotationDegrees = 30;
                mapView.ReSnapRotationDegrees = 5;
                mapView.MyLocationFollow = true;
                mapView.MyLocationEnabled = true;
                mapView.MyLocationLayer.UpdateMyLocation(UserLocation, true);
                List<Pin> pins = await mapSuiVM.GeneratePin();
                foreach (var item in pins)
                {
                    mapView.Pins.Add(item);
                }


                // Init pop-up position
                StackInfoPin.TranslationY = 150;
                PinAlreadyClicked = false;




            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            base.OnAppearing();
        }


        private async void MapView_MapLongClicked(object sender, MapLongClickedEventArgs e)
        {
            try
            {
                var answer = await DisplayAlert("Add Interest", "Do you want to add this point of interest?", "ok", "cancel");

               

                if (answer == true)
                {
                    var placemarks = await Geocoding.GetPlacemarksAsync(e.Point.Latitude, e.Point.Longitude);
                    var placemark = placemarks?.FirstOrDefault();
                    if (placemark != null)

                    {
                        await Navigation.PushAsync(new PoiEditPage(e.Point.Longitude, e.Point.Latitude));
                        Navigation.RemovePage(this);
                    }
                    else
                    {
                        await DisplayAlert("Error", "Not possible to add this location", "ok");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private void MapView_PinClicked(object sender, PinClickedEventArgs e)
        {
            try
            {
                AbsoluImageButton.IsVisible = false;
                LabelPin.Text = e.Pin.Label;
                AdressPin.Text = e.Pin.Address;
                ImagePin.Source = e.Pin.Svg;              

                StackInfoPin.IsVisible = true;

                
                // Open pop-up animation
                if (PinAlreadyClicked == false)
                {
                    StackInfoPin.TranslateTo(0, StackInfoPin.TranslationY - 150);
                }
                PinAlreadyClicked = true;

                ImagePin.Source = e.Pin.Svg;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Close pop-up animation
                StackInfoPin.TranslateTo(0, StackInfoPin.TranslationY + 150);
                PinAlreadyClicked = false;

                AbsoluImageButton.IsVisible = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}