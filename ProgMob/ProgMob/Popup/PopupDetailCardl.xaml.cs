﻿using ProgMob.Models;
using ProgMob.ViewModel;
using ProgMob.ViewModel.Helpers;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            foreach(var y in selectedEx)
            {
                Console.WriteLine("selectedEx: " + y);
                foreach(var x in PopupViewModel.ListPopupEx)
                {
                    Console.WriteLine("Esercizio in ListPopupEx: " + x.Name);
                    if (x.Name.Equals(y))
                    {
                        DatabaseDetailCard.InsertEx(UserId, CardId, x);
                    }
                }
            }
            selectedEx.Clear();
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = e.SelectedItem as Exercise;
            if (!selectedEx.Contains(selectedItem.Name))
            {
                selectedEx.Add(selectedItem.Name);
                Console.WriteLine("AGGIUNTO: " + selectedItem.Name);
            }
            else
            {
                selectedEx.Remove(selectedItem.Name);
                Console.WriteLine("RIMOSSO: " + selectedItem.Name);
            }
        }
    }
}