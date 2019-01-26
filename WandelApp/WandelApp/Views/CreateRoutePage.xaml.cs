using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace WandelApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateRoutePage : ContentPage
    {
        Models.Route route = new Models.Route();
        List<Models.Route> routeList = new List<Models.Route>();

        public CreateRoutePage()
        {
            InitializeComponent();

            // debug
            DoDebug();
        }

        private void HideMapButton_Clicked(object sender, EventArgs e)
        {
            hideyboi.IsVisible = !hideyboi.IsVisible;
            if (hideyboi.IsVisible)
            {
                HideMapButton.Text = "Verstop kaart";
            }
            else
            {
                HideMapButton.Text = "Toon kaart";
            }
        }

        private async void DoDebug()
        {
            Models.Database db = new Models.Database();
            route = await db.GetRoute(1);

            var position = new Xamarin.Forms.Maps.Position(Convert.ToDouble(route.StartLat), Convert.ToDouble(route.StartLong));
            MapVriend.PinCollection.Add(new Pin() { Position = position, Type = PinType.Generic, Label = route.Name + " start!" });
            position = new Xamarin.Forms.Maps.Position(Convert.ToDouble(route.EndLat), Convert.ToDouble(route.EndLong));
            MapVriend.PinCollection.Add(new Pin() { Position = position, Type = PinType.Generic, Label = route.Name + " eind!" });

            //routeList = await db.GetAllRoutes();

            //foreach (Models.Route route in routeList)
            //{
            //    var position = new Xamarin.Forms.Maps.Position(Convert.ToDouble(route.StartLat), Convert.ToDouble(route.StartLong));
            //    MapVriend.PinCollection.Add(new Pin() { Position = position, Type = PinType.Generic, Label = route.Name });
            //}



            MapVriend.BindingContext = MapVriend;
            //var position = await Plugin.Geolocator.CrossGeolocator.Current.GetPositionAsync();
            //MapVriend.MyPosition = new Xamarin.Forms.Maps.Position((double)r.StartLat, (double)r.StartLong);

        }
    }
}