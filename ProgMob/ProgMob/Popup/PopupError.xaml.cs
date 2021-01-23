using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgMob.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupError : Rg.Plugins.Popup.Pages.PopupPage
    {
        public PopupError()
        {
            InitializeComponent();
        }

        private void Redirect_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
            App.Current.MainPage.Navigation.PopAsync();
        }
    }
}