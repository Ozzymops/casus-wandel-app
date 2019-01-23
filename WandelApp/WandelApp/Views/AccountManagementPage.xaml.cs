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
        Models.User curUser;
		public AccountManagementPage ()
		{
			InitializeComponent ();

            // Return SQLite data
            Models.Database database = new Models.Database();
            curUser = database.GetCurrentUser();

            // Put SQLite data into existing labels
            LabelId.Text = curUser.Id.ToString();
            LabelName.Text = curUser.Name;
            LabelUsername.Text = curUser.Username;
            LabelPassword.Text = curUser.Password;

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

        private async void Deletebutton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Account verwijderen","Weet u het zeker?","Ja","Nee");
        }
    }
}