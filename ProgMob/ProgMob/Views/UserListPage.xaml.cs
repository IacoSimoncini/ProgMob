using ProgMob.ViewModel;
using ProgMob.ViewModel.Helpers;
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
    public partial class UserListPage : ContentPage
    {
        UserVM UserVM;
        public UserListPage()
        {
            InitializeComponent();
            Title = "Users";

            UserVM = Resources["UserViewModel"] as UserVM;

            ToolbarItem TBItem = new ToolbarItem
            {
                Text = "Logout",
                IconImageSource = ImageSource.FromFile("logout.png"),
                Order = ToolbarItemOrder.Secondary,
                Priority = 0
            };

            TBItem.Clicked += async (sender, args) =>
            {
                bool logout = await Auth.Logout();
                if (logout)
                {
                    Application.Current.Properties["logged"] = "false";
                    await Application.Current.SavePropertiesAsync();
                    await DisplayAlert("Logout", "You have been disconnected", "Cancel");
                    await Application.Current.MainPage.Navigation.PushModalAsync(new RegisterPage(), true);
                }
                else await DisplayAlert("Logout", "It was not possible to disconnect", "Cancel");
            };
            this.ToolbarItems.Add(TBItem);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UserVM.ListUser();
        }
    }
}