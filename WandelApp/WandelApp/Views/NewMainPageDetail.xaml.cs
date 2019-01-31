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
            }
            else if (Slider_Hill.Value >= 1 && Slider_Hill.Value < 2)
            {
                prefs.HillType = Models.HillType.Sloped;
            }
            else if (Slider_Hill.Value >= 2)
            {
                prefs.HillType = Models.HillType.Steep;
            }

            Label_Hill.Text = prefs.HillType.ToString();
        }

        private void Slider_Forest_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (Slider_Forest.Value >= 0 && Slider_Forest.Value < 1)
            {
                prefs.ForestDensity = Models.ForestDensity.None;
            }
            else if (Slider_Forest.Value >= 1 && Slider_Forest.Value < 2)
            {
                prefs.ForestDensity = Models.ForestDensity.Thin;
            }
            else if (Slider_Forest.Value >= 2)
            {
                prefs.ForestDensity = Models.ForestDensity.Thick;
            }

            Label_Forest.Text = prefs.ForestDensity.ToString();
        }

        private void Slider_Flatness_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (Slider_Flatness.Value >= 0 && Slider_Flatness.Value < 1)
            {
                prefs.RouteFlatness = Models.RouteFlatness.Flat;
            }
            else if (Slider_Flatness.Value >= 1 && Slider_Flatness.Value < 2)
            {
                prefs.RouteFlatness = Models.RouteFlatness.Bumpy;
            }
            Label_Flatness.Text = prefs.RouteFlatness.ToString();
        }

        private void Slider_Signs_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (Slider_Signs.Value >= 0 && Slider_Signs.Value < 1)
            {
                prefs.RoadSigns = Models.RoadSigns.None;
            }
            else if (Slider_Signs.Value >= 1 && Slider_Signs.Value < 2)
            {
                prefs.RoadSigns = Models.RoadSigns.Some;
            }
            else if (Slider_Signs.Value >= 2 && Slider_Signs.Value < 3)
            {
                prefs.RoadSigns = Models.RoadSigns.Many;
            }
            Label_Signs.Text = prefs.RoadSigns.ToString();
        }


        private void HidePreferencesButton_Clicked(object sender, EventArgs e)
        {
            StackFilters.IsVisible = !StackFilters.IsVisible;

        }

        private void Switch_Asphalt_Toggled(object sender, ToggledEventArgs e)
        {
            label_asphalted.Text = Switch_Asphalt.IsToggled.ToString();
        }

        private void Switch_Marshiness_Toggled(object sender, ToggledEventArgs e)
        {
            label_Marshiness.Text = Switch_Marshiness.IsToggled.ToString();
        }

        private void SavePreferencesButton_Clicked(object sender, EventArgs e)
        {
            Models.Database DbPreference = new Models.Database();

            DbPreference.WipePreferences();
            DbPreference.SavePreferences(prefs);
            DisplayAlert("Melding","Voorkeuren opgeslagen","k");
        }

        private void Slider_Lenght_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            prefs.Length = (decimal)Slider_Lenght.Value /10;
            Label_Lenght.Text = prefs.Length.ToString();
        }

        /// <summary>
        /// Wanneer de pagina geopend wordt, moet de list van routes getoond worden.
        /// </summary>
        protected override async void OnAppearing()
        {
            Models.Logger l = new Models.Logger();

            base.OnAppearing();

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

        /// <summary>
        /// Wanneer een route wordt geselecteerd dan moet hij worden ingeladen op de pagina MapPage. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ListOfRoutes_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new MapPage());
        }
    }
}