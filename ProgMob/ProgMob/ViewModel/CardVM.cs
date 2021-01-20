﻿using ProgMob.Models;
using ProgMob.ViewModel.Helpers;
using ProgMob.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProgMob.ViewModel
{
    public class CardVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Card selectedCard;
        public Card SelectedCard
        {
            get { return selectedCard; }
            set
            {
                selectedCard = value;
                OnPropertyChanged("SelectedCard");
                if (selectedCard != null)
                {
                    Application.Current.Properties["CID"] = selectedCard.Path;
                    Application.Current.SavePropertiesAsync();
                    App.Current.MainPage.Navigation.PushAsync(new CardListDetailPage(selectedCard.Ref, selectedCard.Path));
                }
            }
        }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }
        public ObservableCollection<Card> Cards { get; set; }
        public Command DeleteCommand { get; set; }
        public Command ModifyCommand { get; set; }
        public Command PlayCommand { get; set; }
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsRefreshing = true;

                    ListCard(App.Current.Properties["UID"].ToString() , Application.Current.Properties["selectedDay"].ToString(), Application.Current.Properties["selectedWeek"].ToString(), Application.Current.Properties["ABC"].ToString());

                    IsRefreshing = false;
                });
            }
        }
        public CardVM()
        {
            Cards = new ObservableCollection<Card>();
            //ModifyCommand = new Command<object>(Modify);
            DeleteCommand = new Command<object>(Delete);
            PlayCommand = new Command<object>(Play);
            
        }

        private void Play(object obj)
        {
            var card = obj as Card;
            App.Current.MainPage.Navigation.PushAsync(new TimerPage(card.Path, card.Ref));
        }
        
        /*
         private async void Modify(object obj)
        {

            var ex = obj as Exercise;
            await PopupNavigation.PushAsync(new PopupUpdateEx(ex.Id));
        }*/
        private async void Delete(object obj)
        {
            var card = obj as Card;
            bool deleted = await DatabaseCards.DeleteCard(card, 
                Application.Current.Properties["selectedDay"].ToString(),
                Application.Current.Properties["selectedWeek"].ToString());
            if (deleted)
            {
                Cards.Remove(card);
                _ = App.Current.MainPage.DisplayAlert("Successfully deleted", "Please, press OK", "OK");
            }
            else
                _ = App.Current.MainPage.DisplayAlert("Error", "Something went wrong", "OK");
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void ListCard(string Uid , string Day, string Week, string Type)
        {
            if (await DatabaseCards.ListCard(Uid, Day , Week ,Type))
            {
                Cards.Clear();
                var card = await DatabaseCards.GetCard();
                foreach (var c in card)
                {
                    Cards.Add(c);
                }
            }
        }
    }
}
