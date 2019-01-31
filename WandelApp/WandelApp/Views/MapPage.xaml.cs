using System;
using System.Collections.Generic;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace WandelApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        private bool hasLocationPermission = false;

        public MapPage(Models.Route nRoute)
        {
            InitializeComponent();
            NameLabel.Text = nRoute.Name;
            LengthLabel.Text = nRoute.Length.ToString();
            DrawSpecialPin(nRoute);
        }

        /// <summary>
        /// DrawPins - single
        /// </summary>
        /// <param name="pin"></param>
        private void DrawSpecialPin(Models.Route route)
        {
            // Overwrite pins
            CMap.CustomPins.Clear();

            // Start
            Models.CustomPin startPin = new Models.CustomPin
            {
                Position = new Xamarin.Forms.Maps.Position((double)route.StartLat, (double)route.StartLong),
                Label = "Start",
                Name = "Start",
                Description = "Startpunt van de route.",
                ImageId = 0,
                Type = PinType.Generic
            };
            CMap.CustomPins.Add(startPin);

            foreach (Models.RouteSequence seq in route.SequenceList)
            {
                Models.CustomPin stepPin = new Models.CustomPin
                {
                    Position = new Xamarin.Forms.Maps.Position((double)seq.Lat, (double)seq.Long),
                    Label = "Step",
                    Name = "Stap " + seq.StepNumber.ToString(),
                    Description = "Tussenstap van de route.",
                    ImageId = 2,
                    Type = PinType.Generic
                };
                CMap.CustomPins.Add(stepPin);
            }

            Models.CustomPin endPin = new Models.CustomPin
            {
                Position = new Xamarin.Forms.Maps.Position((double)route.EndLat, (double)route.EndLong),
                Label = "Eind",
                Name = "Eind",
                Description = "Eindpunt van de route.",
                ImageId = 1,
                Type = PinType.Generic
            };
            CMap.CustomPins.Add(endPin);

            foreach (Models.POI poi in route.POIList)
            {
                Models.CustomPin poiPin = new Models.CustomPin
                {
                    Position = new Xamarin.Forms.Maps.Position((double)poi.Lat, (double)poi.Long),
                    Label = "POI",
                    Name = poi.Name,
                    Description = poi.Description,
                    ImageId = 3,
                    Type = PinType.Generic
                };
                CMap.CustomPins.Add(poiPin);
            }

            CMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(CMap.CustomPins[0].Position.Latitude, CMap.CustomPins[0].Position.Longitude), Distance.FromKilometers(3.0)));
        }

        private void CMap_Tapped(object sender, Models.MapTapEventArgs e)
        {

        }
    }
}