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
        Models.Route r = new Models.Route();

        public CreateRoutePage()
        {
            InitializeComponent();

            MapVriend.PinCollection.Add(new Pin() { Position = MapVriend.MyPosition, Type = PinType.Generic, Label = "I'm a Pin" });


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
            r = await db.GetRoute(1);

            //var position = await Plugin.Geolocator.CrossGeolocator.Current.GetPositionAsync();
            //MapVriend.MyPosition = new Xamarin.Forms.Maps.Position((double)r.StartLat, (double)r.StartLong);

        }
    }
}