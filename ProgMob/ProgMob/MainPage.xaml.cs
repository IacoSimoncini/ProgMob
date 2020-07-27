using ProgMob.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgMob
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : Shell
    {
        public MainPage()
        {
            InitializeComponent();
            Application.Current.Properties["logged"] = "true";
            Application.Current.SavePropertiesAsync();
            if (DatabaseProfile.GetProfile()) {
                TabBar.Items.RemoveAt(2);
                TabBar.Items.RemoveAt(3);
            }
            else {
                TabBar.Items.RemoveAt(0);
                TabBar.Items.RemoveAt(1);
            }
        }
    }
}