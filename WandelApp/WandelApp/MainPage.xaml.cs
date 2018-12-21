using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WandelApp
{
    public partial class MainPage : ContentPage
    {
        private Models.Preferences prefs;
        public MainPage()
        {
            InitializeComponent();
            prefs = new Models.Preferences();
        }

        private void Slider_Hill_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (Slider_Hill.Value >= 0 && Slider_Hill.Value < 1)
            {
                prefs.hills = Models.Hills.None;
            }
            else if (Slider_Hill.Value >= 1 && Slider_Hill.Value < 2)
            {
                prefs.hills = Models.Hills.Sloped;
            }
            else if (Slider_Hill.Value >= 2)
            {
                prefs.hills = Models.Hills.Steep;
            }

            Label_Hill.Text = prefs.hills.ToString();
        }

        private void Slider_Forest_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (Slider_Forest.Value >= 0 && Slider_Forest.Value < 1)
            {
                prefs.forestDensity = Models.ForestDensity.None;
            }
            else if (Slider_Forest.Value >= 1 && Slider_Forest.Value < 2)
            {
                prefs.forestDensity = Models.ForestDensity.Thin;
            }
            else if (Slider_Forest.Value >= 2)
            {
                prefs.forestDensity = Models.ForestDensity.Thick;
            }

            Label_Forest.Text = prefs.forestDensity.ToString();
        }

        private void Slider_Flatness_ValueChanged(object sender, ValueChangedEventArgs e)
        {

        }

        private void Slider_Signs_ValueChanged(object sender, ValueChangedEventArgs e)
        {

        }
    }
}
