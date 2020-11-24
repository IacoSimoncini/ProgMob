using ProgMob.Models;
using ProgMob.Popup;
using ProgMob.ViewModel.Helpers;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProgMob.ViewModel
{
    class ExerciseVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Exercise selectedEx;
        
        public Exercise SelectedEx
        {
            get { return selectedEx; }
            set
            {
                if (selectedEx != value)
                {
                    selectedEx = value;
                    OnpropertyChanged("SelectedEx");
                    if (selectedEx != null)
                    {

                    }
                }
            }
        }

        public ObservableCollection<Exercise> Exercises { get; set; }

        public Command DeleteCommand { get; set; }
        public Command ModifyCommand { get; set; }
        
        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnpropertyChanged(nameof(IsRefreshing));
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsRefreshing = true;

                    ListAllExercise();

                    IsRefreshing = false;
                });
            }
        }


        public ExerciseVM()
        {
            Exercises = new ObservableCollection<Exercise>();
            ModifyCommand = new Command<object>(Modify);
            DeleteCommand = new Command<object>(Delete);
        }

        private async void Modify(object obj)
        {
            var ex = obj as Exercise;
            await PopupNavigation.PushAsync(new PopupUpdateEx(ex.Id));
        }

        private void OnpropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void Delete(object obj)
        {
            var ex = obj as Exercise;
            bool deleted = await DatabaseExercise.DeleteExercise(ex.Id);
            if (deleted)
            {
                Exercises.Remove(ex);
                _ = App.Current.MainPage.DisplayAlert("Succesfully deleted", "Plese, press OK", "OK");
            }
            else
                _ = App.Current.MainPage.DisplayAlert("Error", "Something went wrong", "OK");
        }

        public async void ListAllExercise()
        {
            Exercises.Clear();
            if(await DatabaseExercise.ListExercise())
            {
                var ex = await DatabaseExercise.GetExercises();
                foreach (var e in ex)
                {
                    Exercises.Add(e);
                }
            }
            
        }
    }
}
