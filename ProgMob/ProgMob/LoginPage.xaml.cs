﻿using ProgMob.ViewModel.Helpers;
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

            var Signup_Tap = new TapGestureRecognizer();
            Signup_Tap.Tapped += (s,e) =>
            {
                App.Current.MainPage = new NavigationPage(new RegisterPage());
            };
            Lbl_Register.GestureRecognizers.Add(Signup_Tap);

        }

        async void SignIn(object sender, EventArgs e)
        {
            string email = Entry_Email.Text;
            string password = Entry_Password.Text;

            string token = await Auth.LoginToFirebase(email, password);

            if (token != "")
            {
                await DisplayAlert("Authentication successful", "Press OK to continue", "OK");
                App.Current.MainPage = new Splash();
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
        
    }
}