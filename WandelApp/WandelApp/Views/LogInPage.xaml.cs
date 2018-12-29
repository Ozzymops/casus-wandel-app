using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        public async void LogIn()
        {
            var baseAddress = new Uri("http://169.254.80.80:59258");
            var client = new HttpClient { BaseAddress = baseAddress };

            var request = new HttpRequestMessage(HttpMethod.Get, "/api/database?username=Justintje&password=abc");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var user = await response.Content.ReadAsStringAsync();
        }
    }
}