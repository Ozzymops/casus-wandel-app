using System;
using System.Collections.Generic;
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
        }

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


        private void Button_Clicked(object sender, EventArgs e)
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
            
        }
    }
}