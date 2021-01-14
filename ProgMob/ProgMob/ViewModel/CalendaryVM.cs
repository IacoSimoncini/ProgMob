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
        
        private Day selectedDay;
        public Day SelectedDay
        {
            get { return selectedDay; }
            set
            {
                selectedDay = value;
                /*if (selectedDay != null) {
                    Application.Current.Properties["selectedDay"] = selectedDay.n;
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

        public ObservableCollection<Day> Days { get; set; }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsRefreshing = true;
                    
                    ListDay(App.Current.Properties["UID"].ToString() , "A");

                    IsRefreshing = false;
                });
            }
        }

        public CalendaryVM()
        {
            Days = new ObservableCollection<Day>();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 

        public async void ListDay(string Uid , string Type)
        {
            
                Days.Clear();
                var days= await DatabaseCalendary.getDays();
                foreach (Day d in days)
                {
                Console.WriteLine("TAXI "+d.n.ToString());
                   Days.Add(d);
                }
        }
    }
}
