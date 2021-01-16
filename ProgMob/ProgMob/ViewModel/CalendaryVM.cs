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
        private DaysInWeek selectedDay;
        public DaysInWeek SelectedDay
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

        public ObservableCollection<DaysInWeek> ListDays { get; set; }

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
            ListDays = new ObservableCollection<DaysInWeek>();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        
        public async void ListDaysInWeek(string Uid , string Type)
        {
            ListDays.Clear();
            for (int i = 1; i <= 4; i++)
            {
                if (await DatabaseDaysInWeek.CheckDaysInWeek(Uid, Type, i))
                {
                    DaysInWeek d = DatabaseDaysInWeek.GetDaysInWeek();
                    ListDays.Add(d);
                }
            }
        }
    }
}
