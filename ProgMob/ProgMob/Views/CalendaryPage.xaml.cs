
using ProgMob.Models;
using ProgMob.Popup;
using ProgMob.ViewModel;
using ProgMob.ViewModel.Helpers;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgMob.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendaryPage : ContentPage
    {
        static public int verify = 0;
        public CalendaryVM CalendaryVM;
        private string UserId;
        private string type;
        public CalendaryPage()
        {
            InitializeComponent();
            Title = "Calendary";
            type = Application.Current.Properties["ABC"].ToString();
            UserId = Application.Current.Properties["MyUID"].ToString();
            CalendaryVM = Resources["CalendaryViewModel"] as CalendaryVM;
            ToolbarItem TBItemLogout = new ToolbarItem
            {
                Text = "Logout",
                IconImageSource = ImageSource.FromFile("logout.png"),
                Order = ToolbarItemOrder.Secondary,
                Priority = 0
            };

            TBItemLogout.Clicked += async (sender, args) =>
            {
                bool logout = await Auth.Logout();
                if (logout)
                {
                    Application.Current.Properties["logged"] = "false";
                    await Application.Current.SavePropertiesAsync();
                    await DisplayAlert("Logout", "You have been disconnected, the app will be closed", "Cancel");
                    await System.Threading.Tasks.Task.Delay(1000);
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
                else await DisplayAlert("Logout", "It was not possible to disconnect", "Cancel");
            };
            this.ToolbarItems.Add(TBItemLogout);
        }


        protected override void OnAppearing()
        {
            CalendaryVM.ListDaysInWeek(UserId, type);
            base.OnAppearing();
        }

        private void ChangeTypeButton_Clicked(object sender, EventArgs e)
        {
            type = CalendaryVM.ChangeType();
            CalendaryVM.ListDaysInWeek(UserId, type);
            Console.WriteLine(type);
        }

    }
}