using ProgMob.Models;
using ProgMob.Popup;
using ProgMob.ViewModel.Helpers;
using ProgMob.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
                    App.Current.MainPage.Navigation.PushAsync(new CardListDetailPage(selectedCard.Ref, selectedCard.Path, selectedCard.Name));
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
            ModifyCommand = new Command<object>(Modify);
            DeleteCommand = new Command<object>(Delete);
            PlayCommand = new Command<object>(Play);
            
        }

        private async void Play(object obj)
        {
            var card = obj as Card;

            await PopupNavigation.PushAsync(new PopupTimer(card.Path, card.Ref, card.Name));
        }
        
        
         private async void Modify(object obj)
        {
            var c = obj as Card;
            await PopupNavigation.PushAsync(new PopupUpdateCard(c.Ref, Application.Current.Properties["selectedDay"].ToString() , c.Path, Application.Current.Properties["selectedWeek"].ToString()));
        }
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
                if (!Cards.Any() && !Application.Current.Properties["Admin"].Equals("true"))
                {
                    await PopupNavigation.PushAsync(new PopupError());
                }
            }
        }
    }
}
