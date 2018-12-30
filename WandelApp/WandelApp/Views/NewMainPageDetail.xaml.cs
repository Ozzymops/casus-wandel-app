using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        }

        private void Slider_Signs_ValueChanged(object sender, ValueChangedEventArgs e)
        {

        }

        //private void Switch_Toggled(object sender, ToggledEventArgs e)
        //{

        //}
    }
}