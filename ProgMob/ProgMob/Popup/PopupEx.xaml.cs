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
    public partial class PopupEx : Rg.Plugins.Popup.Pages.PopupPage
    {
        public PopupEx()
        {
            InitializeComponent();
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync(true);
        }

    }
}