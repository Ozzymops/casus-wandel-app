using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WandelApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RoutePage : ContentPage
	{
		public RoutePage ()
		{
			InitializeComponent ();

            FillRouteList();
        }

        public async void FillRouteList()
        {
            Models.Database db = new Models.Database();
            List<Models.Route> routeList = await db.GetAllRoutes();

            // Seperate List type as ItemsSource
            ObservableCollection<Models.Route> routeCollection = new ObservableCollection<Models.Route>();

            foreach (Models.Route route in routeList)
            {
                routeCollection.Add(route);
            }

            RouteList.ItemsSource = routeCollection;
            RouteList.BindingContext = this.BindingContext;
        }

        private async void RouteList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new MapPage((Models.Route)RouteList.SelectedItem));
        }
    }
}