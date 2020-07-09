using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgMob.Models;
using ProgMob.ViewModel.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgMob.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }
        async void SignIn(object sender , EventArgs e)
        {
            string email = Entry_Email.Text;
            string password = Entry_Password.Text;

            string token = await Auth.LoginToFirebase(email, password);

            if (token != "")
            {
                await DisplayAlert("Authentication successful", "Press OK to continue", "OK");
                //App.Current.MainPage = new MainPage();
            }
            else
            {
                Error();
            }

        }

        async private void Error()
        {
            if (string.IsNullOrEmpty(Entry_Email.Text) && string.IsNullOrEmpty(Entry_Password.Text))
            {
                await DisplayAlert("Authentication failed", "Please, fill all the fields", "Cancel");
            }
            else if (string.IsNullOrWhiteSpace(Entry_Password.Text))
            {
                await DisplayAlert("Authentication failed", "Please, insert your password", "Cancel");
            } 
            else if (string.IsNullOrWhiteSpace(Entry_Email.Text))
            {
                await DisplayAlert("Authentication failed", "Please, insert your email", "Cancel");
            }
            else
            {
                await DisplayAlert("Authentication failed", "E-mail or password are not correct", "Cancel");
            }
        }

        async void Register(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Register());
        }
    }
}