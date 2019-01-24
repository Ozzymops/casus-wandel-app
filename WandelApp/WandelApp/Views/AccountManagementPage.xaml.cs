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

            if (curUser != null)
            {
                // Put SQLite data into existing labels
                LabelId.Text = curUser.Id.ToString();
                LabelName.Text = curUser.Name;
                LabelUsername.Text = curUser.Username;
                LabelPassword.Text = curUser.Password;
            }
            else
            {
                LabelId.Text = "Log a.u.b. in.";
            }

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
            // await DisplayAlert("Account verwijderen","Weet u het zeker?","Ja","Nee");
            Models.Database db = new Models.Database();
            List<Models.Route> routeList = await db.GetAllRoutes();
            int i = 0;
            foreach (Models.Route route in routeList)
            {
                i++;
                DisplayAlert("Route" + i, route.Id + " - " + route.Name, "Next!");
            }
        }
    }
}