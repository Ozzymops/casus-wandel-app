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
            TipLabel.IsVisible = !TipLabel.IsVisible;
            StartLocButton.IsVisible = !StartLocButton.IsVisible;
            EndLocButton.IsVisible = !EndLocButton.IsVisible;
            if (hideyboi.IsVisible)
            {
                HideMapButton.Text = "Verstop kaart";
            }
            else
            {
                HideMapButton.Text = "Toon kaart";
            }
        }

        private async void CreateRoute()
        {
            Models.Database db = new Models.Database();

            List<Models.POI> poiList = new List<Models.POI>();
            List<Models.RouteSequence> sequenceList = new List<Models.RouteSequence>();

            // Fill POI & RouteSequence lists here

            Models.Route newRoute = new Models.Route()
            {
                Id = 0,
                OwnerId = db.GetCurrentUser().Id,
                Name = NameEntry.Text,
                Difficulty = 0, // always 0
                StartLong = 0,  // chosen from pin
                StartLat = 0,   // chosen from pin
                EndLong = 0,    // chosen from pin
                EndLat = 0,     // chosen from pin
                POIList = poiList,
                SequenceList = sequenceList,
                Length = (int)LengthStepper.Value,
                HillType = (Models.HillType)HillTypeStepper.Value,
                Marshiness = false,
                ForestDensity = (Models.ForestDensity)ForestDensityStepper.Value,
                RouteFlatness = (Models.RouteFlatness)RouteFlatnessStepper.Value,
                RouteAsphalted = false,
                RoadSigns = 0
            };
            newRoute.Difficulty = newRoute.CalculateDifficulty(newRoute);

            // db.SaveRoute(newRoute)
            // First, save internally and then try to push externally.
            // Make DB create seperate tables of POI and Sequence
        }

        private async void DoDebug()
        {
            Models.Database db = new Models.Database();
            route = await db.GetRoute(1);

            var position = new Xamarin.Forms.Maps.Position(Convert.ToDouble(route.StartLat), Convert.ToDouble(route.StartLong));
            MapVriend.PinCollection.Add(new Pin() { Position = position, Type = PinType.Generic, Label = "Start!" });
            position = new Xamarin.Forms.Maps.Position(Convert.ToDouble(route.EndLat), Convert.ToDouble(route.EndLong));
            MapVriend.PinCollection.Add(new Pin() { Position = position, Type = PinType.Generic, Label = "Eind!" });

            foreach (Models.RouteSequence rs in route.SequenceList)
            {
                position = new Xamarin.Forms.Maps.Position(Convert.ToDouble(rs.Lat), Convert.ToDouble(rs.Long));
                MapVriend.PinCollection.Add(new Pin() { Position = position, Type = PinType.SavedPin, Label = "Stap " + rs.StepNumber.ToString() + "!" });
            }

            foreach (Models.POI poi in route.POIList)
            {
                position = new Xamarin.Forms.Maps.Position(Convert.ToDouble(poi.Lat), Convert.ToDouble(poi.Long));
                MapVriend.PinCollection.Add(new Pin() { Position = position, Type = PinType.Place, Label = poi.Name });
            }

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

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            CreateRoute();
        }

        private void HillTypeStepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            switch (HillTypeStepper.Value)
            {
                case 0:
                    HillTypeLabel.Text = "Vlak";
                    break;

                case 1:
                    HillTypeLabel.Text = "Helling";
                    break;

                case 2:
                    HillTypeLabel.Text = "Steil";
                    break;
            }
        }

        private void LengthStepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            try
            {
                LengthLabel.Text = LengthStepper.Value.ToString();
            }
            catch (Exception exc)
            {
                Models.Logger l = new Models.Logger();
                l.WriteToLog("Een of andere bullshit error weer: " + exc);
            }
        }

        private void StartLocButton_Clicked(object sender, EventArgs e)
        {
            // Tik op kaart en laat een pin verschijnen
        }

        private void EndLocButton_Clicked(object sender, EventArgs e)
        {
            // Tik op kaart en laat een pin verschijnen
        }

        private void RouteFlatnessStepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            switch (RouteFlatnessStepper.Value)
            {
                case 0:
                    RouteFlatnessLabel.Text = "Vlak";
                    break;

                case 1:
                    RouteFlatnessLabel.Text = "Hobbelig";
                    break;
            }
        }

        private void ForestDensityStepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            switch (ForestDensityStepper.Value)
            {
                case 0:
                    ForestDensityLabel.Text = "Vlak";
                    break;

                case 1:
                    ForestDensityLabel.Text = "Dun";
                    break;

                case 2:
                    ForestDensityLabel.Text = "Dik";
                    break;
            }
        }

        private void RoadSignsStepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            switch (RoadSignsStepper.Value)
            {
                case 0:
                    RoadSignsLabel.Text = "Geen";
                    break;

                case 1:
                    RoadSignsLabel.Text = "Weinig";
                    break;

                case 2:
                    RoadSignsLabel.Text = "Veel";
                    break;
            }
        }

        private void RouteAsphaltedSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (RouteAsphaltedSwitch.IsToggled)
            {
                RouteAsphaltedLabel.Text = "Ja";
            }
            else
            {
                RouteAsphaltedLabel.Text = "Nee";
            }
        }

        private void MarshinessSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (MarshinessSwitch.IsToggled)
            {
                MarshinessLabel.Text = "Ja";
            }
            else
            {
                MarshinessLabel.Text = "Nee";
            }
        }
    }
}