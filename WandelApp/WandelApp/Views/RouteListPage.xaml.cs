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
	public partial class RouteListPage : ContentPage
	{
        // Testing
        public class Route
        {
            public string RouteName { get; set; }
            public DateTime RouteDate { get; set; }
        }

        // Testing
        public ObservableCollection<Route> routeList { get; set; } = new ObservableCollection<Route>();

        public Command EditCommand => new Command<Route>((route) => EditTapped(route));
        public Command RemoveCommand => new Command<Route>((route) => RemoveTapped(route));
        public Command WalkCommand => new Command<Route>((route) => WalkTapped(route));

        private void EditTapped(Route route)
        {
            DisplayAlert("Edit!", route.RouteName, "k");
        }

        private void RemoveTapped(Route route)
        {
            DisplayAlert("Remove!", route.RouteName, "k");
        }

        private void WalkTapped(Route route)
        {
            DisplayAlert("Walk!", route.RouteName, "k");
        }

        public void Test()
        {
            //routeList = new List<Route>();
            for (int n = 0; n < 5; n++)
            {
                Route newRoute = new Route() { RouteName = "Route " + n, RouteDate = DateTime.Now };
                routeList.Add(newRoute);
            }
        }

        // KEEP THIS
		public RouteListPage ()
		{
			InitializeComponent ();
            
            // Testing
            Test();
            ListyView1.ItemsSource = routeList;
            BindingContext = this;
        }
    }
}