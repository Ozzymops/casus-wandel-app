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
            Models.Database db = new Models.Database();
            var status = await db.LogIn(Username.Text, Password.Text);
            await DisplayAlert("Bliep!", status.ToString(), "OK");
        }
    }
}