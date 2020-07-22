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
                    Console.WriteLine(selectedCard.Ref);
                    App.Current.MainPage.Navigation.PushAsync(new CardListDetailPage(selectedCard.Ref, selectedCard.Path));
                }
            }
        }

        public ObservableCollection<Card> Cards { get; set; }
        public Command DeleteCommand { get; set; }
        public CardVM()
        {
            Cards = new ObservableCollection<Card>();
            
            DeleteCommand = new Command(Delete);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void ListCard(string Uid)
        {
            var cards = await DatabaseCards.ListCard(Uid);
            Cards.Clear();
            foreach (var c in cards)
            {
                Cards.Add(c);
            }
        }

        private async void Delete(object obj)
        {
            Card card = obj as Card;
            bool deleted = await DatabaseCards.DeleteCard(card);
            if (deleted)
                await App.Current.MainPage.Navigation.PopAsync();
            else
                await App.Current.MainPage.DisplayAlert("Error", "An error has occurred", "Cancel");
        }

    }
}
