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
	public partial class AccountManagementPage : ContentPage
	{
		public AccountManagementPage ()
		{
			InitializeComponent ();

            // Set images
            DownArrowImage.Source = ImageSource.FromFile("SwipeDown.png");
        }

        private async void ChangePasswordButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.ChangePasswordPage());
        }

        private async void ChangeEmailButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.ChangeEmailPage());
        }

        private void Deletebutton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Account verwijderen","Weet u het zeker?","Ja","Nee");
        }
    }
}