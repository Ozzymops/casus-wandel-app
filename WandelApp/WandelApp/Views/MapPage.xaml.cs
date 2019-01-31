using System;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WandelApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{
        private bool hasLocationPermission = false;
        private object content;

        public MapPage()
        {
            InitializeComponent();
        }

        public ShowRouteOnMap()
        {
            try
            {
                Models.Database db = new Models.Database();
                currentRoute = await db.GetRoute(1);
                DrawPins(currentRoute);
            }
            catch()
            {
                
            }
        }
    }
}