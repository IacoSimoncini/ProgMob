using ProgMob.ViewModel;
using ProgMob.ViewModel.Helpers;
using ProgMob.Views;
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
    public partial class PopupLoading : Rg.Plugins.Popup.Pages.PopupPage
    {
        public PopupLoading(string id, string type, int value)
        {
            InitializeComponent();
            Startup(id, type, value);
        }

        public async void Startup(string id, string type, int value)
        {
            if(value == 1)
            {
                if (await DatabaseDaysInWeek.CheckDaysInWeek(id, type, 1)
                                    && await DatabaseDaysInWeek.CheckDaysInWeek(id, type, 2)
                                    && await DatabaseDaysInWeek.CheckDaysInWeek(id, type, 3)
                                    && await DatabaseDaysInWeek.CheckDaysInWeek(id, type, 4))
                {
                    PopupNavigation.PopAsync();
                    App.Current.MainPage.Navigation.PushAsync(new CalendaryPage());
                }
                else
                {
                    await DisplayAlert("Error", "Something went wrong", "OK");
                    PopupNavigation.PopAsync();
                }
            }
            else
            {
                if (await DatabaseDaysInWeek.CheckDaysInWeek(id, type, 1)
                                    && await DatabaseDaysInWeek.CheckDaysInWeek(id, type, 2)
                                    && await DatabaseDaysInWeek.CheckDaysInWeek(id, type, 3)
                                    && await DatabaseDaysInWeek.CheckDaysInWeek(id, type, 4))
                {
                    PopupNavigation.PopAsync();
                    App.Current.MainPage.Navigation.PopAsync();
                    App.Current.MainPage.Navigation.PushAsync(new CalendaryPage());
                }
                else
                {
                    await DisplayAlert("Error", "Something went wrong", "OK");
                    PopupNavigation.PopAsync();
                }
            }
            
        }

    }
}