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
        public PopupLoading(string id, string type)
        {
            InitializeComponent();
            Console.WriteLine("inizio Startup");
            Startup(id, type);
            Console.WriteLine("fine Startup");
        }

        public async void Startup(string id, string type)
        {
            if (await DatabaseDaysInWeek.CheckDaysInWeek(id, type, 1)
                    && await DatabaseDaysInWeek.CheckDaysInWeek(id, type, 2)
                    && await DatabaseDaysInWeek.CheckDaysInWeek(id, type, 3)
                    && await DatabaseDaysInWeek.CheckDaysInWeek(id, type, 4))
            {
                Console.WriteLine("Calendario start");
                PopupNavigation.PopAsync();
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