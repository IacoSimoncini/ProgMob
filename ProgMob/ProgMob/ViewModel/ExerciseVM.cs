using ProgMob.Models;
using ProgMob.ViewModel.Helpers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ProgMob.ViewModel
{
    class ExerciseVM : INotifyPropertyChanged
    {
        //private ObservableCollection<Exercise> selectedExercises;
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
                    HandleSelectedExercises();
                }
            }
        }
        /*public ObservableCollection<Exercise> SelectedExercises
        {
            get { return selectedExercises; }
            private set { selectedExercises = value; OnpropertyChanged(""); }
        }*/

        private void HandleSelectedExercises()
        {
            Console.WriteLine(SelectedEx.Name);
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

        public async void ListExercise(String CardId, string UserId)
        {
            // List<Exercise> exsCard = (List<Exercise>)await DatabaseDetailCard.ListExercise(CardId, UserId);
            Exercises.Clear();
            if(await DatabaseDetailCard.ListExercise(UserId, CardId))  // Carica il vettore con gli esercizi lista
            {
                var exsCard = await DatabaseDetailCard.GetExercises();      // Metodo get per esercizi lista
                var ex = await DatabaseExercise.GetExercises();     // Metodo get per esercizi tutti
                foreach (var e in ex)
                {
                    if (!exsCard.Contains(e))
                    {
                        Console.WriteLine(e.Name + " NON PRESENTE");
                        Exercises.Add(e);
                    }
                    else { Console.WriteLine(e.Name + " PRESENTE"); 
                    }
                }
            }
            
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
