using ProgMob.Models;
using ProgMob.ViewModel.Helpers;
using ProgMob.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
                OnPropertyChanged("SelectedUser");
                if (selectedCard != null)
                {
                    App.Current.MainPage.Navigation.PushAsync(new CardListDetailPage(selectedCard.Ref, selectedCard.Path));
                }
            }
        }

        public ObservableCollection<Card> Cards { get; set; }
        public Command DeleteCommand { get; set; }
        public CardVM()
        {
            Cards = new ObservableCollection<Card>();

            DeleteCommand = new Command<object>(Delete);
        }


        private async void Delete(object obj)
        {
            var card = obj as Card;
            bool deleted = await DatabaseCards.DeleteCard(card);
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

        public async void ListCard(string Uid)
        {
            if (await DatabaseCards.ListCard(Uid))
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
