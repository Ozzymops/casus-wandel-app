using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WandelApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage : ContentPage
    {
        public LogInPage()
        {
            InitializeComponent();
        }

        private async void RegistrerenButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.RegisterPage());
        }

        private async void ForgotPassword_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.ForgotPasswordPage());
        }

        private async void LogIn_Clicked(object sender, EventArgs e)
        {
            await LogIn();

            // If/else check voor sessie
            // true --> doe niks, zeg dat sessie al ingenomen is.
            // false --> doe LogIn().
        }

        public async Task LogIn()
        {
            // Return User uit db
            var uri = new Uri(string.Format("http://192.168.56.1:45455/api/user/LogIn?username={0}&password={1}", Username.Text, Password.Text));

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(uri);

            // Verwerk response
            string statusString;
            if (response == null || response == "null")
            {
                statusString = "Fout bij het inloggen. Check uw gebruikersnaam en password en probeer het opnieuw.";
            }
            else
            {
                Models.User user = JsonConvert.DeserializeObject<Models.User>(response);

                // Save response naar SQLite
                Models.Database database = new Models.Database();
                database.WipeUsers();
                database.AddUser(user);

                statusString = "Succes! Welkom " + user.Name + "!";
            }

            await DisplayAlert("Bliep!", statusString, "OK");
        }
    }
}