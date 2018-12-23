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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.ForgotPasswordPage());
        }
    }
}