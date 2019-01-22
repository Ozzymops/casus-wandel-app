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
        bool hasLocationPermission = false;

        public MapPage()
        {
            InitializeComponent();
            //GetPermission();
        }

        //private async void GetPermission()
        //{
        //    try
        //    {
        //        var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);
        //        if (status != PermissionStatus.Granted)
        //        {
        //            if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.LocationWhenInUse))
        //            {
        //                await DisplayAlert("Need your location", "We need to access your location", "ok");
        //            }
        //            var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationWhenInUse);
        //            if (results.ContainsKey(Permission.LocationWhenInUse))
        //            {
        //                status = results[Permission.LocationWhenInUse];
        //            }
        //        }

        //        if (status == PermissionStatus.Granted)
        //        {
        //            hasLocationPermission = true;
        //            locationsMap.IsShowingUser = true;

        //            GetLocation();
        //        }
        //        else
        //        {
        //            await DisplayAlert("Location denied", "You didn't give us permission to access your location, so we cant show", "Ok");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await DisplayAlert("Error", ex.Message, "Ok");
        //    }
        //}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //if (hasLocationPermission)
            //{
                var locator = CrossGeolocator.Current;
                locator.PositionChanged += Locator_PositionChanged;
                await locator.StartListeningAsync(TimeSpan.Zero, 100);
            //}

            //GetLocation();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            CrossGeolocator.Current.StopListeningAsync();
            CrossGeolocator.Current.PositionChanged -= Locator_PositionChanged;
        }

        void Locator_PositionChanged(object sender, PositionEventArgs e)
        {
            MoveMap(e.Position);
        }

        private async void GetLocation()
        {
            //if (hasLocationPermission)
            //{
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync();

                MoveMap(position);
            //}
        }
        private void MoveMap(Position position)
        {
            var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
            var span = new Xamarin.Forms.Maps.MapSpan(center, 1, 1);
            GoogleAPIMaps.MoveToRegion(span);
        }
    }
}