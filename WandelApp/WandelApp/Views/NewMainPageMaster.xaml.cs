using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WandelApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewMainPageMaster : ContentPage
    {
        public ListView ListView;

        public NewMainPageMaster()
        {
            InitializeComponent();

            BindingContext = new NewMainPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class NewMainPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<NewMainPageMenuItem> MenuItems { get; set; }
            
            public NewMainPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<NewMainPageMenuItem>(new[]
                {
                    new NewMainPageMenuItem { Id = 0, Title = "Hoofdpagina"},
                    new NewMainPageMenuItem { Id = 1, Title = "Log in", TargetType = typeof(LogInPage) },
                    new NewMainPageMenuItem { Id = 2, Title = "Account", TargetType = typeof(AccountManagementPage) },
                    new NewMainPageMenuItem { Id = 3, Title = "Downloads", TargetType = typeof(DownloadPage) },
                    new NewMainPageMenuItem { Id = 4, Title = "Route aanmaken", TargetType = typeof(CreateRoutePage) },
                    new NewMainPageMenuItem { Id = 5, Title = "Mijn routelijst", TargetType = typeof(RouteListPage) },
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}