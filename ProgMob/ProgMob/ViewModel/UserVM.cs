using ProgMob.Models;
using ProgMob.ViewModel.Helpers;
using ProgMob.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
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
                    App.Current.MainPage.Navigation.PushAsync(new CalendaryPage());
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

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsRefreshing = true;

                    ListUser();

                    IsRefreshing = false;
                });
            }
        }

        public ObservableCollection<User> Users { get; set; }

        public Command DeleteCommand { get; set; }

        public UserVM()
        {
            Users = new ObservableCollection<User>();
            DeleteCommand = new Command<object>(Delete);
        }

        private async void Delete(object obj)
        {
            var user = obj as User;
            bool deleted = DatabaseUser.DeleteUser(user.Id);
            if (deleted)
            {
                Users.Remove(user);
                _ = App.Current.MainPage.DisplayAlert("Successfully deleted", "Please, press OK", "OK");
            }
            else
                _ = App.Current.MainPage.DisplayAlert("Error", "Something went wrong", "OK");
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
