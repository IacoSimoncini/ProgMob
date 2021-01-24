using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgMob.Models;
using ProgMob.ViewModel.Helpers;
using Rg.Plugins.Popup.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgMob.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupUpdateUser : Rg.Plugins.Popup.Pages.PopupPage
    {
        private User user;
        public PopupUpdateUser(User User)
        {
            InitializeComponent();
            user = User;
        }

        private async void Submit(object sender, EventArgs e)
        {
            if(Email.Text != null)
            {
                user.Email = Email.Text;
            }
            if(Surname.Text != null)
            {
                user.Surname = Surname.Text;
            }
            if(Username.Text != null)
            {
                user.Username = Username.Text;
            }
            if(Name.Text != null)
            {
                user.Name = Name.Text;
            }
            if (DatabaseUser.Update(user))
            {
                _ = App.Current.MainPage.DisplayAlert("Updated successful", "Please, press OK", "OK");
                Auth.UpdateEmailToFirebase(user);
                PopupNavigation.PopAsync();
            }
            else
            {
                _ = App.Current.MainPage.DisplayAlert("Error", "Something went wrong", "OK");
                PopupNavigation.PopAsync();
            }
            
        }
    }
}