using ProgMob.Models;
using ProgMob.Popup;
using ProgMob.ViewModel.Helpers;
using Rg.Plugins.Popup.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

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
                selectedEx = value;
                OnpropertyChanged("SelectedEx");
                if (selectedEx != null)
                {
                    PopupNavigation.PushAsync(new PopupUpdateEx());
                    selectedEx = null;
                }
            }
        }
        public ObservableCollection<Exercise> Exercises { get; set; }

        public ExerciseVM()
        {
            Exercises = new ObservableCollection<Exercise>();
        }

        private void OnpropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void ListExercise()
        {
            var ex = await DatabaseExercise.ListExercise();
            Exercises.Clear();
            foreach (var e in ex)
            {
                Exercises.Add(e);
            }
        }
    }
}
