using ProgMob.Models;
using ProgMob.ViewModel.Helpers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace ProgMob.ViewModel
{
    public class CardDetailVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Exercise> Exercises { get; set; }
        //public Command<object> DeleteEx { get; set; }
        public CardDetailVM()
        {
            Exercises = new ObservableCollection<Exercise>();
            //DeleteEx = new Command<object>(Delete);
        }

        private Exercise selectedExercise;

        public Exercise SelectedExercise
        {
            get { return selectedExercise; }
            set
            {
                selectedExercise = value;
                OnPropertyChanged("SelectedExercise");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void ListEx(string Uid, string Cid)
        {
            var ex = await DatabaseDetailCard.ListExercise(Uid, Cid);
            Exercises.Clear();
            foreach (var e in ex)
            {
                Exercises.Add(e);
            }
        }

    }
}
