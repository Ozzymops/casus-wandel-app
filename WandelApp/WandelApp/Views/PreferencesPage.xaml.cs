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
	public partial class PreferencesPage : ContentPage
	{
        Models.Preferences tempPreferences;
		public PreferencesPage ()
		{
			InitializeComponent ();

            // Initialise temporary object for filling with data
            tempPreferences = new Models.Preferences();

            // Fill sliders and such with data
            Models.Database db = new Models.Database();
            Models.Preferences dbPref = db.GetPreferences();

            if (dbPref != null)
            {
                LengthSlider.Value = (double)dbPref.Length;
                HillSlider.Value = (double)dbPref.HillType;
                ForestSlider.Value = (double)dbPref.ForestDensity;
                FlatSlider.Value = (double)dbPref.RouteFlatness;
                SignSlider.Value = (double)dbPref.RoadSigns;
                AsphSwitch.IsToggled = dbPref.RouteAsphalted;
                MarshSwitch.IsToggled = dbPref.Marshiness;
            }
		}

        private void LengthSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            tempPreferences.Length = (decimal)LengthSlider.Value;
            LengthLabel.Text = LengthSlider.Value.ToString();
        }

        private void HillSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (HillSlider.Value >= 0 && HillSlider.Value < 1)
            {
                tempPreferences.HillType = Models.HillType.None;
                HillLabel.Text = "Geen";
            }
            else if (HillSlider.Value >= 1 && HillSlider.Value < 2)
            {
                tempPreferences.HillType = Models.HillType.Sloped;
                HillLabel.Text = "Geheld";
            }
            else if (HillSlider.Value >= 2)
            {
                tempPreferences.HillType = Models.HillType.Steep;
                HillLabel.Text = "Steil";
            }
        }

        private void ForestSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (ForestSlider.Value >= 0 && ForestSlider.Value < 1)
            {
                tempPreferences.ForestDensity = Models.ForestDensity.None;
                ForestLabel.Text = "Geen";
            }
            else if (ForestSlider.Value >= 1 && ForestSlider.Value < 2)
            {
                tempPreferences.ForestDensity = Models.ForestDensity.Thin;
                ForestLabel.Text = "Dun";
            }
            else if (ForestSlider.Value >= 2)
            {
                tempPreferences.ForestDensity = Models.ForestDensity.Thick;
                ForestLabel.Text = "Dik";
            }
        }

        private void FlatSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (FlatSlider.Value >= 0 && FlatSlider.Value < 1)
            {
                tempPreferences.RouteFlatness = Models.RouteFlatness.Flat;
                FlatLabel.Text = "Vlak";
            }
            else if (FlatSlider.Value >= 1 && FlatSlider.Value < 2)
            {
                tempPreferences.RouteFlatness = Models.RouteFlatness.Bumpy;
                FlatLabel.Text = "Hobbelig";
            }
        }

        private void SignSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (SignSlider.Value >= 0 && SignSlider.Value < 1)
            {
                tempPreferences.RoadSigns = Models.RoadSigns.None;
                SignLabel.Text = "Geen";
            }
            else if (SignSlider.Value >= 1 && SignSlider.Value < 2)
            {
                tempPreferences.RoadSigns = Models.RoadSigns.Some;
                SignLabel.Text = "Weinig";
            }
            else if (SignSlider.Value >= 2 && SignSlider.Value < 3)
            {
                tempPreferences.RoadSigns = Models.RoadSigns.Many;
                SignLabel.Text = "Veel";
            }
        }

        private void AsphSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            tempPreferences.RouteAsphalted = AsphSwitch.IsToggled;
            if (AsphSwitch.IsToggled)
            {
                AsphLabel.Text = "Ja";
            }
            else
            {
                AsphLabel.Text = "Nee";
            }
        }

        private void MarshSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            tempPreferences.Marshiness = MarshSwitch.IsToggled;
            if (MarshSwitch.IsToggled)
            {
                MarshLabel.Text = "Ja";
            }
            else
            {
                MarshLabel.Text = "Nee";
            }
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            Models.Database DbPreference = new Models.Database();

            DbPreference.WipePreferences();
            DbPreference.SavePreferences(tempPreferences);
            DisplayAlert("Status", "Voorkeuren succesvol opgeslagen!", "OK");
        }
    }
}