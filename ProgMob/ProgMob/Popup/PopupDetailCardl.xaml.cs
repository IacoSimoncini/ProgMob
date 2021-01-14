using ProgMob.Models;
using ProgMob.ViewModel;
using ProgMob.ViewModel.Helpers;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProgMob.Views;

namespace ProgMob.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupDetailCardl : Rg.Plugins.Popup.Pages.PopupPage
    {
        private readonly string UserId;
        private readonly string CardId;
        public List<string> selectedEx = new List<string>();
        PopupDetailCardVM PopupViewModel;
        public PopupDetailCardl(string Uid, string Cid)
        {
            InitializeComponent();
            UserId = Uid;
            CardId = Cid;
            PopupViewModel = Resources["PopupDetailCardViewModel"] as PopupDetailCardVM;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            PopupViewModel.LoadListEx(UserId, CardId);
        }

        private void Add_Exercise(object sender, EventArgs e)
        {
            foreach(var x in PopupViewModel.ListPopupEx)
            {
                if (x.IsChecked)
                {
                    x.IsChecked = false;
                    DatabaseDetailCard.InsertEx(UserId, CardId, x , "1");
                }
            }

            DisplayAlert("Exercises added to the card", "Please, refresh the list", "OK");
            PopupNavigation.PopAsync();
            CardListDetailPage.verify = 1;
        }

        private void ExercisesLV_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (sender is ListView lv) lv.SelectedItem = null;
        }
    }
}