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
        public static TabBar Tab;
        public MainPage()
        {
            InitializeComponent();
            Application.Current.Properties["logged"] = "true";
            Application.Current.SavePropertiesAsync();
            Tab = AdminTabBar;
        }
    }
}