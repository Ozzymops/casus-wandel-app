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

        private void LogIn_Clicked(object sender, EventArgs e)
        {
            LogIn();
        }

        public async Task LogIn()
        {
            var uri = new Uri(string.Format("http://192.168.1.69:45455/api/user/LogIn?username={0}&password={1}", Username.Text, Password.Text));

            HttpClient client = new HttpClient();
            var response = await client.GetAsync(uri);
            await DisplayAlert("Test!", response.ToString(), "OK");
        }
    }
}