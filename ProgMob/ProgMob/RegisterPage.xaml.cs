﻿using ProgMob.Models;
using ProgMob.ViewModel.Helpers;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace ProgMob
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            Title = "Register";
        }
        async void SignUp(object sender, EventArgs e)
        {
            string username = Entry_Username.Text;
            string password = Entry_Password.Text;
            string confirmPassword = Entry_ConfirmPassword.Text;
            string email = Entry_Email.Text;
            string name = Entry_Name.Text;
            string surname = Entry_Surname.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname))
            {
                await DisplayAlert("Error", "Please, enter all the fields", "Cancel");
            }
            else if (string.IsNullOrWhiteSpace(email))
            {
                await DisplayAlert("Incorrect email", "Please, enter a valid email", "Cancel");
            }
            else if (string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Incorrect password", "Please, enter a valid password", "Cancel");
            }
            else if (password.Length < 8)
            {
                await DisplayAlert("Incorrect password", "The password must be at least 8 characters long", "Cancel");
            }
            else if (!password.Equals(confirmPassword))
            {
                await DisplayAlert("Incorrect password", "Passwords do not match", "Cancel");
            }
            else
            {
                // Firebase Authentication
                HelpStorage helpStorage = new HelpStorage(null);
                string uri = await helpStorage.GetDefaultPic();
                Console.WriteLine(uri);
                User user = new User(name, surname, uri, username, email);
                string Token = await Auth.RegisterToFirebase(username, email, password);
                if (Token != "" && DatabaseUser.InsertUser(user))
                {
                    await DisplayAlert("Registration successful", "Press OK to continue", "OK");
                    App.Current.MainPage = new NavigationPage(new Splash());
                }
                else
                {
                    await DisplayAlert("Registration unsuccesful", "Wrong email or password", "Cancel");
                }

            }

        }

        async void LoginRedirect(object sender, EventArgs e) => App.Current.MainPage = new NavigationPage(new LoginPage());

    }
}