using ProgMob.Models;
using ProgMob.ViewModel.Helpers;
using ProgMob.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace ProgMob.ViewModel
{
    public class UserVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private User selectedUser;
        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged("SelectedUser");
                if (selectedUser != null)
                {
                    Application.Current.Properties["UID"] = selectedUser.Id;
                    Application.Current.SavePropertiesAsync();
                    App.Current.MainPage.Navigation.PushAsync(new CardListPage());
                }
            }
        }
        public ObservableCollection<User> Users { get; set; }

        public UserVM()
        {
            Users = new ObservableCollection<User>();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void ListUser()
        {
            if (await DatabaseUser.ListUser())
            {
                Users.Clear();
                var users = await DatabaseUser.GetUser();
                foreach (var u in users)
                { 
                    Users.Add(u);
                }
            }
        }
    }
}
