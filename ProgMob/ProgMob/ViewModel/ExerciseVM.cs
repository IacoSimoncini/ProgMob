using ProgMob.Models;
using ProgMob.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        public ExerciseVM()
        {
            Exercises = new ObservableCollection<Exercise>();

            DeleteCommand = new Command<object>(Delete);
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

            var ex = await DatabaseExercise.GetExercises();
            foreach (var e in ex)
            {
                Exercises.Add(e);
            }
        }
    }
}
