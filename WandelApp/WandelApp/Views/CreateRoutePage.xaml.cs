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
        Models.Logger l = new Models.Logger();

        private int progStep = 0;
        private int step = 1;
        public Models.Route newRoute;
        private Models.CustomPin startPin;
        private Models.CustomPin endPin;
        private List<Models.CustomPin> stepPins;
        private List<Models.CustomPin> poiPins;

        public CreateRoutePage()
        {
            InitializeComponent();

            // Initialise other stuff
            newRoute = new Models.Route();
            stepPins = new List<Models.CustomPin>();
            poiPins = new List<Models.CustomPin>();
        }

        /// <summary>
        /// DrawPins - full
        /// </summary>
        /// <param name="route"></param>
        private void DrawPins(Models.Route route)
        {
            List<Models.CustomPin> pinList = CMap.CreatePinList(route);

            CMap.CustomPins = pinList;

            CMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(pinList[0].Position.Latitude, pinList[0].Position.Longitude), Distance.FromKilometers(1.0)));
        }

        /// <summary>
        /// DrawPins - single
        /// </summary>
        /// <param name="pin"></param>
        private void DrawSpecialPin()
        {
            l.WriteToLog("Route page - DrawPins(pin)");

            // Overwrite pins
            CMap.CustomPins.Clear();

            if (startPin != null)
            {
                CMap.CustomPins.Add(startPin);
            }

            if (endPin != null)
            {
                CMap.CustomPins.Add(endPin);
            }

            if (stepPins.Count != 0)
            {
                foreach (Models.CustomPin spin in stepPins)
                {
                    CMap.CustomPins.Add(spin);
                }
            }

            if (poiPins.Count != 0)
            {
                foreach (Models.CustomPin ppin in poiPins)
                {
                    CMap.CustomPins.Add(ppin);
                }
            }

            // Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(Map.CustomPins[0].Position.Latitude, Map.CustomPins[0].Position.Longitude), Distance.FromKilometers(1.0)));
        }

        public async void CreateRoute()
        {
            Models.Database db = new Models.Database();

            // Set variables
            try
            {
                int routeId = await db.GetLastId();
                newRoute.OwnerId = db.GetCurrentUser().Id;
                newRoute.Name = NameEntry.Text;
                newRoute.Length = (int)LengthStepper.Value;
                newRoute.HillType = (Models.HillType)HillStepper.Value;
                newRoute.ForestDensity = (Models.ForestDensity)ForestStepper.Value;
                newRoute.RouteFlatness = (Models.RouteFlatness)FlatStepper.Value;
                newRoute.RoadSigns = (Models.RoadSigns)SignStepper.Value;
                newRoute.Marshiness = MarshSwitch.IsToggled;
                newRoute.RouteAsphalted = AsphSwitch.IsToggled;

                newRoute.POIList = new List<Models.POI>();
                foreach (Models.CustomPin pin in poiPins)
                {
                    Models.POI tempPOI = new Models.POI()
                    {
                        RouteId = routeId,
                        Name = pin.Name,
                        Description = pin.Description,
                        Lat = (decimal)pin.Position.Latitude,
                        Long = (decimal)pin.Position.Longitude
                    };
                    newRoute.POIList.Add(tempPOI);
                }

                newRoute.SequenceList = new List<Models.RouteSequence>();
                foreach (Models.CustomPin pin in stepPins)
                {
                    Models.RouteSequence tempSeq = new Models.RouteSequence()
                    {
                        RouteId = routeId,
                        StepNumber = Convert.ToInt32(pin.Name.Split(null)[1]),
                        Lat = (decimal)pin.Position.Latitude,
                        Long = (decimal)pin.Position.Longitude
                    };
                    newRoute.SequenceList.Add(tempSeq);
                }

                newRoute.Difficulty = newRoute.CalculateDifficulty(newRoute);

                // Save POI
                // Save Steps
                await DisplayAlert("Status", await db.AddRoute(newRoute), "OK");
            }
            catch (Exception e)
            {
                Models.Logger l = new Models.Logger();
                l.WriteToLog(e.ToString());
            }
        }

        #region Buttons
        private void Map_Tapped(object sender, Models.MapTapEventArgs e)
        {
            //DisplayAlert("Tikkie!", "Jij bent hem, op: Long: " + e.Position.Longitude + ", Lat: " + e.Position.Latitude, "OK");

            // Data
            switch (progStep)
            {
                case 0:
                    l.WriteToLog("Map_Tapped 0");
                    newRoute.StartLat = (decimal)e.Position.Latitude;
                    newRoute.StartLong = (decimal)e.Position.Longitude;
                    startPin = new Models.CustomPin
                    {
                        Position = new Xamarin.Forms.Maps.Position(e.Position.Latitude, e.Position.Longitude),
                        Label = "Start",
                        Name = "Start",
                        Description = "Startpunt van de route.",
                        ImageId = 0,
                        Type = PinType.Generic
                    };
                    DrawSpecialPin();
                    break;

                case 1:
                    l.WriteToLog("Map_Tapped 1");
                    Models.CustomPin stepPin = new Models.CustomPin
                    {
                        Position = new Xamarin.Forms.Maps.Position(e.Position.Latitude, e.Position.Longitude),
                        Label = "Step",
                        Name = "Stap " + step.ToString(),
                        Description = "Tussenstap van de route.",
                        ImageId = 2,
                        Type = PinType.Generic
                    };
                    stepPins.Add(stepPin);
                    DrawSpecialPin();
                    step++;
                    break;

                case 2:
                    l.WriteToLog("Map_Tapped 2");
                    step = 1; // for POI
                    newRoute.EndLat = (decimal)e.Position.Latitude;
                    newRoute.EndLong = (decimal)e.Position.Longitude;
                    endPin = new Models.CustomPin
                    {
                        Position = new Xamarin.Forms.Maps.Position(e.Position.Latitude, e.Position.Longitude),
                        Label = "Eind",
                        Name = "Eind",
                        Description = "Eindpunt van de route.",
                        ImageId = 1,
                        Type = PinType.Generic
                    };
                    DrawSpecialPin();
                    break;

                case 3:
                    l.WriteToLog("Map_Tapped 3");
                    Models.CustomPin poiPin = new Models.CustomPin
                    {
                        Position = new Xamarin.Forms.Maps.Position(e.Position.Latitude, e.Position.Longitude),
                        Label = "POI",
                        Name = "POI " + step.ToString(),
                        Description = "Point-of-interest voor tijdens de route.",
                        ImageId = 3,
                        Type = PinType.Generic
                    };
                    poiPins.Add(poiPin);
                    DrawSpecialPin();
                    step++;
                    break;
            }
        }

        private void NextButton_Clicked(object sender, EventArgs e)
        {
            // Progression
            // Steps: 0 = start, 1 = steps, 2 = end, 3 = POI
            if (progStep < 4)
            {
                if (startPin == null && progStep == 0)
                {
                    DisplayAlert("Fout", "Voeg a.u.b. een startlocatie toe.", "OK");
                }
                else if (endPin == null && progStep == 2)
                {
                    DisplayAlert("Fout", "Voeg a.u.b. een eindlocatie toe.", "OK");
                }
                else
                {
                    progStep++;
                }
            }

            if (progStep >= 4)
            {
                MapGrid.IsVisible = false;
                ParameterLayout.IsVisible = true;
            }

            if (progStep == 1)
            {
                StepLabel.Text = "2. Plaats tussenstappen.";
            }
            else if (progStep == 2)
            {
                StepLabel.Text = "3. Kies een eindlocatie.";
            }
            else if (progStep == 3)
            {
                StepLabel.Text = "4. Plaats points-of-interest.";
            }
        }

        private void LengthStepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            LengthNumber.Text = LengthStepper.Value.ToString();
            if (LengthStepper.Value > 6)
            {
                LengthNumber.TextColor = Color.Red;
            }
            else if (LengthStepper.Value > 3 && LengthStepper.Value <= 6)
            {
                LengthNumber.TextColor = Color.DarkGoldenrod;
            }
            else if (LengthStepper.Value > 0 && LengthStepper.Value <= 3)
            {
                LengthNumber.TextColor = Color.DarkGreen;
            }
        }

        private void HillStepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            switch (HillStepper.Value)
            {
                case 0:
                    HillLabel.Text = "Vlak";
                    HillLabel.TextColor = Color.DarkGreen;
                    break;

                case 1:
                    HillLabel.Text = "Geheld";
                    HillLabel.TextColor = Color.DarkGoldenrod;
                    break;

                case 2:
                    HillLabel.Text = "Steil";
                    HillLabel.TextColor = Color.Red;
                    break;
            }
        }

        private void FlatStepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            switch (FlatStepper.Value)
            {
                case 0:
                    FlatLabel.Text = "Vlak";
                    FlatLabel.TextColor = Color.DarkGreen;
                    break;

                case 1:
                    FlatLabel.Text = "Hobbelig";
                    FlatLabel.TextColor = Color.DarkGoldenrod;
                    break;
            }
        }

        private void ForestStepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            switch (ForestStepper.Value)
            {
                case 0:
                    ForestLabel.Text = "Geen";
                    ForestLabel.TextColor = Color.DarkGreen;
                    break;

                case 1:
                    ForestLabel.Text = "Dun";
                    ForestLabel.TextColor = Color.DarkGoldenrod;
                    break;

                case 2:
                    ForestLabel.Text = "Dik";
                    ForestLabel.TextColor = Color.Red;
                    break;
            }
        }

        private void SignStepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            switch (SignStepper.Value)
            {
                case 0:
                    SignLabel.Text = "Geen";
                    SignLabel.TextColor = Color.Red;
                    break;

                case 1:
                    SignLabel.Text = "Weinig";
                    SignLabel.TextColor = Color.DarkGoldenrod;
                    break;

                case 2:
                    SignLabel.Text = "Veel";
                    SignLabel.TextColor = Color.DarkGreen;
                    break;
            }
        }

        private void AsphSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (AsphSwitch.IsToggled)
            {
                AsphLabel.Text = "Ja";
                AsphLabel.TextColor = Color.DarkGreen;
            }
            else
            {
                AsphLabel.Text = "Nee";
                AsphLabel.TextColor = Color.Red;
            }
        }

        private void MarshSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (MarshSwitch.IsToggled)
            {
                MarshLabel.Text = "Ja";
                MarshLabel.TextColor = Color.Red;
            }
            else
            {
                MarshLabel.Text = "Nee";
                MarshLabel.TextColor = Color.DarkGreen;
            }
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(NameEntry.Text))
            {
                if (LengthStepper.Value == 0)
                {
                    DisplayAlert("Fout", "Vul a.u.b. een geldige lengte (> 0) en naam in.", "OK");
                }
                else
                {
                    DisplayAlert("Fout", "Vul a.u.b. een geldige naam in.", "OK");
                }
            }
            else if (LengthStepper.Value == 0)
            {
                DisplayAlert("Fout", "Vul a.u.b. een geldige lengte (> 0) in.", "OK");
            }

            // Succes!
            if (!String.IsNullOrWhiteSpace(NameEntry.Text) && LengthStepper.Value > 0)
            {
                // opslaan en check
                CreateRoute();
            }
        }
        #endregion
    }
}