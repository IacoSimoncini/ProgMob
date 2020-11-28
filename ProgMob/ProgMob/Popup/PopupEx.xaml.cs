﻿using ProgMob.Models;
using ProgMob.ViewModel.Helpers;
using Rg.Plugins.Popup.Services;
using System;
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
            if(Name.Text != null && Description.Text != null && Difficulty.Text != null && Name.Text.Length < 10 && Description.Text.Length < 25) 
            {
                Exercise ex = new Exercise(Name.Text, Description.Text, Difficulty.Text);
                if (DatabaseExercise.InsertExercise(ex))
                {
                    await App.Current.MainPage.DisplayAlert("Insert completed", "Please, press OK to continue", "OK");
                    PopupNavigation.PopAsync();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Something went wrong", "OK");
                    PopupNavigation.PopAsync();
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Name up to 10 characters, description up to 25 characters", "OK");
            }
        }
    }
}