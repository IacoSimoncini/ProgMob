﻿using ProgMob.Models;
using ProgMob.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace ProgMob.ViewModel
{
    class UserVM : INotifyPropertyChanged
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
                //if(selectedUser!=null) 
                // REDIRECT SCHEDE
            }
        }
        public ObservableCollection<User> Users { get; set;}

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
            var users = await DatabaseUser.ListUser();
            Users.Clear();
            foreach (var u in users)
            {
                Users.Add(u);
            }
        }
    }
}
