using ProgMob.ViewModel.Helpers;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgMob
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Title = "Login";
        }
        async void SignIn(object sender, EventArgs e)
        {
            string email = Entry_Email.Text;
            string password = Entry_Password.Text;

            string token = await Auth.LoginToFirebase(email, password);

            if (token != "")
            {
                if (await DatabaseProfile.GetProfile())
                {
                    App.Current.MainPage = new MainPageAdmin();
                }
                else
                {
                    App.Current.MainPage = new MainPage();
                }
                await DisplayAlert("Authentication successful", "Press OK to continue", "OK");
                await Navigation.PopAsync();
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

        private void Register(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new RegisterPage());
        }
    }
}