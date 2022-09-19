using Mapsui;
using Mapsui.Projection;
using Mapsui.UI;
using Mapsui.UI.Forms;
using Mapsui.UI.Objects;
using Mapsui.Utilities;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinAPP.Views;
using TabiTravel.XamarinModel.Models.InterestModel;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.ViewModels
{
    public class MapSuiVM : INotifyPropertyChanged
    {
        #region Properties
        public INavigation Navigation { get; set; }
        public Interest Interest { get; set; } = new Interest();
        public ObservableCollection<Interest> interests { get; set; }
        public ObservableCollection<Interest> Interests
        {
            get => interests;
            set
            {
                interests = value;
                OnPropertyChanged(nameof(Interests));
            }
        }
        IInterestService InterestService;
        public Map _map { get; set; }
        public Map MapVM
        {
            get
            {
                return _map;
            }
            set
            {
                if (value != null)
                {
                    _map = value;
                    OnPropertyChanged("MapVM");
                }
            }
        }
        public ICommand GoTo { get; }

        private Page _page;
        #endregion
        public List<Pin> Pins { get; set; }
        public MapSuiVM(INavigation _navigation, Page page)
        {
            GoTo = new Command(GoToPoiEditPage);
            _page = page;
            Navigation = _navigation;
            MapVM = new Map()
            {
                CRS = "EPSG:3857",
                Transformation = new MinimalTransformation(),
                Limiter =
                {
                    ZoomLimits = new MinMax(0.2,1000),
                },
            };
            var tileLayer = OpenStreetMap.CreateTileLayer();
            MapVM.Layers.Add(tileLayer);
            MapVM.Widgets.Add(new Mapsui.Widgets.ScaleBar.ScaleBarWidget(MapVM)
            {
                TextAlignment = Mapsui.Widgets.Alignment.Center,
                HorizontalAlignment = Mapsui.Widgets.HorizontalAlignment.Left,
                VerticalAlignment = Mapsui.Widgets.VerticalAlignment.Bottom
            });
            InterestService = DependencyService.Get<IInterestService>(DependencyFetchTarget.NewInstance);
            Pins = new List<Pin>();
            Interests = new ObservableCollection<Interest>();
        }

        private void GoToPoiEditPage(object obj)
        {
            try
            {
                Navigation.PushAsync(new PoiEditPage());
                Navigation.RemovePage(_page);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #region Generate Pin
        public async Task<List<Pin>> GeneratePin()
        {
            try
            {     
                var color = Color.FromHex("099bdd");
                var list = await InterestService.GetAll();
                Interests = new ObservableCollection<Interest>(list);
                List<Pin> pins = Interests.Select(x => new Pin
                {
                    Position = new Position(x.Latitude, x.Longitude),
                    Label = x.Name,
                    Address = x.Address,
                    Color = color,
                    Svg = x.ImageUrl
                }).ToList();

                return pins;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
      
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
