using ProgMob.Models;
using ProgMob.ViewModel.Helpers;
using ProgMob.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProgMob.ViewModel
{
    public class CalendaryVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Week selectedWeek;
        ICommand tapCommand;
        public Week SelectedWeek
        {
            get { return selectedWeek;  }
            set
            {
                selectedWeek = value;
                /*
                if (selectedWeek != null) {
                    Application.Current.Properties["selectedDay"] = selectedWeek;
                    Application.Current.SavePropertiesAsync();
                    App.Current.MainPage.Navigation.PushAsync(new CardListPage());
                } ;*/
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

       public ObservableCollection<Week> ListWeek { get; set; }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsRefreshing = true;
                    
                    ListDaysInWeek(App.Current.Properties["UID"].ToString() , "A");

                    IsRefreshing = false;
                });
            }
        }

        public CalendaryVM()
        {
            ListWeek  = new ObservableCollection<Week>();
            tapCommand = new Command(OnTapped);
        }

        public ICommand TapCommand
        {
            get { return tapCommand; }
        }

        void OnTapped(object s)
        {
            var label = s as Label;
            Console.WriteLine(label.Text);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        
        public async void ListDaysInWeek(string Uid , string Type)
        {
            ListWeek.Clear();
            for (int i = 1; i <= 4; i++)
            {
                if (await DatabaseDaysInWeek.CheckDaysInWeek(Uid, Type, i))
                {
                    Week w = DatabaseDaysInWeek.GetWhichWeek(i.ToString());
                    w.priority = i;
                    ListWeek.Add(w);
                }
            }
        }

    }
}
