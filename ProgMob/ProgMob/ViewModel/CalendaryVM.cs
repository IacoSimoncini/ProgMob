using ProgMob.Models;
using ProgMob.Popup;
using ProgMob.ViewModel.Helpers;
using ProgMob.Views;
using Rg.Plugins.Popup.Services;
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
        static public int verify = 0;
        public event PropertyChangedEventHandler PropertyChanged;
        private string selectedType;
        private string UserId;
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

                    PopupNavigation.PushAsync(new PopupLoading(UserId, selectedType, 2));

                    IsRefreshing = false;
                });
            }
        }

        public CalendaryVM()
        {
            ListWeek = new ObservableCollection<Week>();
            UserId = Application.Current.Properties["UID"].ToString();
            selectedType = Application.Current.Properties["ABC"].ToString();
            tapCommand = new Command(OnTapped);
        }

        public ICommand TapCommand
        {
            get { return tapCommand; }
        }


        
        async void OnTapped(object s)
        {
            var label = s as Label;

            var week = label.BindingContext as Week;

            DaysInWeek d = new DaysInWeek();
            d = getSelectedDay(label.Text, week);
            if (Application.Current.Properties["Admin"].ToString().Equals("true") && !d.ifSet) {
                Application.Current.Properties["selectedWeek"] = d.week;
                Application.Current.Properties["selectedDay"] = d.n;
                verify = 0;
                await PopupNavigation.PushAsync(new PopupCard(UserId,d.n, selectedType , d.week));  

                for (int i = 0; i < 1000; i++)
                {
                    await System.Threading.Tasks.Task.Delay(100);
                    if (verify != 0)
                        break;
                }
            }
            else {
                Application.Current.Properties["selectedWeek"] = d.week;
                Application.Current.Properties["selectedDay"] = d.n;
                Application.Current.SavePropertiesAsync();
                App.Current.MainPage.Navigation.PushAsync(new CardListPage());
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        
        public async void ListDaysInWeek()
        {
            ListWeek.Clear();
            var lv = DatabaseDaysInWeek.GetWeeks();
            foreach (var w in lv)
            {
                ListWeek.Add(w);
            }
        }

        public DaysInWeek getSelectedDay(string s, Week w)
        {
            DaysInWeek d = new DaysInWeek();
            switch (s)
            {
                case "LUN":
                    d = w.d1;
                    break;
                case "MAR":
                    d = w.d2;
                    break;
                case "MER":
                    d = w.d3;
                    break;
                case "GIO":
                    d = w.d4;
                    break;
                case "VEN":
                    d = w.d5;
                    break;
                case "SAB":
                    d = w.d6;
                    break;
                case "DOM":
                    d = w.d7;
                    break;
            }
            return d;
        }

        public string ChangeType()
        {
            switch (selectedType)
            {
                case "A":
                    Application.Current.Properties["ABC"] = "B";
                    Application.Current.SavePropertiesAsync();
                    selectedType = "B";
                    break;
                case "B":
                    Application.Current.Properties["ABC"] = "C";
                    Application.Current.SavePropertiesAsync();
                    selectedType = "C";
                    break;
                case "C":
                    Application.Current.Properties["ABC"] = "A";
                    selectedType = "A";
                    break;
            }
            return selectedType;
        }
    }
}
