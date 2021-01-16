﻿using ProgMob.Models;
using ProgMob.ViewModel.Helpers;
using ProgMob.Views;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgMob.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupCard : Rg.Plugins.Popup.Pages.PopupPage
    {
        private string Day;
        private string UserId;
        public PopupCard(string Uid, string day)
        {
            InitializeComponent();
            Day = day;
            UserId = Uid;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if(Name.Text != null && Name.Text.Length < 10)
            {
                Card card = new Card();
                card.Path = Name.Text;
                card.Ref = UserId;
                card.Type = "A";
                if (DatabaseCards.InsertCard(card , Day))
                {
                    if( await DatabaseDaysInWeek.CheckDaysInWeek(UserId , "A",1))
                    {
                        _ = App.Current.MainPage.DisplayAlert("Entry successful", "Please, press OK", "OK");
                        //CardListPage.verify = 1;
                        CalendaryPage.verify = 1;
                    }
                    else
                    {
                        _ = App.Current.MainPage.DisplayAlert("Error", "Something went wrong", "OK");
                    }
                }
                else
                {
                    _ = App.Current.MainPage.DisplayAlert("Error", "The insertion was not successful", "OK");
                }
                PopupNavigation.PopAsync();
            } 
            else
            {
                _ = App.Current.MainPage.DisplayAlert("Error", "Enter up to 10 characters", "OK");
            }
        }
    }
}