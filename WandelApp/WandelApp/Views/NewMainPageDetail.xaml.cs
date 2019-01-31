using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WandelApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WandelApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewMainPageDetail : TabbedPage
    {
        private Models.Preferences prefs;
        public NewMainPageDetail()
        {
            InitializeComponent();
        }
    }
}