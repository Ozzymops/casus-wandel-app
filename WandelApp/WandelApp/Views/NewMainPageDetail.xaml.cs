using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WandelApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WandelApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewMainPageDetail : ContentPage
    {
        private Models.Preferences prefs;
        public NewMainPageDetail()
        {
            InitializeComponent();
            prefs = new Models.Preferences();

            // Preferences
            try
            {
                Models.Database db = new Models.Database();
                Models.Preferences preferences = db.GetPreferences();
                if (preferences != null)
                {
                    // Set up sliders and stuff
                    Slider_Hill.Value = (double)preferences.HillType;
                    Slider_Forest.Value = (double)preferences.ForestDensity;
                    Slider_Flatness.Value = (double)preferences.RouteFlatness;
                    Slider_Signs.Value = (double)preferences.RoadSigns;
                    Slider_Lenght.Value = (double)preferences.Length;
                    Switch_Asphalt.IsToggled = preferences.RouteAsphalted;
                    Switch_Marshiness.IsToggled = preferences.Marshiness;
                }
            }
            catch (Exception e)
            {
                Models.Logger l = new Models.Logger();
                l.WriteToLog(e.ToString());
            }

            // Set images
            HidePreferencesButton.Source = ImageSource.FromFile("down_chevron.png");
        }
        /// <summary>
        /// Alle sliders krijgen een waardes toegewezen. Wanneer een waarde gekozen wordt krijg hij een resultaat.
        /// dat resultaat word opgeslagen om preferences te kunnen opslaan.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slider_Hill_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (Slider_Hill.Value >= 0 && Slider_Hill.Value < 1)
            {
                prefs.HillType = Models.HillType.None;
                Label_Hill.Text = "Geen";
            }
            else if (Slider_Hill.Value >= 1 && Slider_Hill.Value < 2)
            {
                prefs.HillType = Models.HillType.Sloped;
                Label_Hill.Text = "Geheld";
            }
            else if (Slider_Hill.Value >= 2)
            {
                prefs.HillType = Models.HillType.Steep;
                Label_Hill.Text = "Steil";
            }
        }

        private void Slider_Forest_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (Slider_Forest.Value >= 0 && Slider_Forest.Value < 1)
            {
                prefs.ForestDensity = Models.ForestDensity.None;
                Label_Forest.Text = "Geen";
            }
            else if (Slider_Forest.Value >= 1 && Slider_Forest.Value < 2)
            {
                prefs.ForestDensity = Models.ForestDensity.Thin;
                Label_Forest.Text = "Dun";
            }
            else if (Slider_Forest.Value >= 2)
            {
                prefs.ForestDensity = Models.ForestDensity.Thick;
                Label_Forest.Text = "Dik";
            }
        }

        private void Slider_Flatness_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (Slider_Flatness.Value >= 0 && Slider_Flatness.Value < 1)
            {
                prefs.RouteFlatness = Models.RouteFlatness.Flat;
                Label_Flatness.Text = "Vlak";
            }
            else if (Slider_Flatness.Value >= 1 && Slider_Flatness.Value < 2)
            {
                prefs.RouteFlatness = Models.RouteFlatness.Bumpy;
                Label_Flatness.Text = "Hobbelig";
            }
        }

        private void Slider_Signs_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (Slider_Signs.Value >= 0 && Slider_Signs.Value < 1)
            {
                prefs.RoadSigns = Models.RoadSigns.None;
                Label_Signs.Text = "Geen";
            }
            else if (Slider_Signs.Value >= 1 && Slider_Signs.Value < 2)
            {
                prefs.RoadSigns = Models.RoadSigns.Some;
                Label_Signs.Text = "Weinig";
            }
            else if (Slider_Signs.Value >= 2 && Slider_Signs.Value < 3)
            {
                prefs.RoadSigns = Models.RoadSigns.Many;
                Label_Signs.Text = "Veel";
            }
        }


        private void HidePreferencesButton_Clicked(object sender, EventArgs e)
        {
            StackFilters.IsVisible = !StackFilters.IsVisible;
        }

        private void Switch_Asphalt_Toggled(object sender, ToggledEventArgs e)
        {
            prefs.RouteAsphalted = Switch_Asphalt.IsToggled;
            if (Switch_Asphalt.IsToggled)
            {
                label_asphalted.Text = "Ja";
            }
            else
            {
                label_asphalted.Text = "Nee";
            }
        }

        private void Switch_Marshiness_Toggled(object sender, ToggledEventArgs e)
        {
            prefs.Marshiness = Switch_Marshiness.IsToggled;
            if (Switch_Marshiness.IsToggled)
            {
                label_Marshiness.Text = "Ja";
            }
            else
            {
                label_Marshiness.Text = "Nee";
            }
        }

        private void SavePreferencesButton_Clicked(object sender, EventArgs e)
        {
            Models.Database DbPreference = new Models.Database();

            DbPreference.WipePreferences();
            DbPreference.SavePreferences(prefs);
            DisplayAlert("Status","Voorkeuren succesvol opgeslagen!","OK");
        }

        private void Slider_Lenght_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            prefs.Length = (decimal)Slider_Lenght.Value;
            Label_Lenght.Text = prefs.Length.ToString();
        }

        /// <summary>
        /// Wanneer de pagina geopend wordt, moet de list van routes getoond worden.
        /// </summary>
        protected override async void OnAppearing()
        {
            Models.Logger l = new Models.Logger();

            base.OnAppearing();

            try
            {
                Models.Database db = new Models.Database();
                Preferences preferences = db.GetPreferences();
                int diff = preferences.CalculateDifficulty(preferences);
                l.WriteToLog(diff.ToString());

                List<Route> routes = await db.GetAllRoutes();

                ObservableCollection<Route> routeCollection = new ObservableCollection<Route>();

                foreach (Route route in routes)
                {
                    l.WriteToLog("Route! " + route.Name);
                    routeCollection.Add(route);
                }

                ListOfRoutes.ItemsSource = routeCollection;
                ListOfRoutes.BindingContext = this.BindingContext;
            }
            catch (Exception e)
            {
                l.WriteToLog(e.ToString());
            }
        }

        /// <summary>
        /// Wanneer een route wordt geselecteerd dan moet hij worden ingeladen op de pagina MapPage. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ListOfRoutes_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new MapPage((Models.Route)ListOfRoutes.SelectedItem));
        }
    }
}