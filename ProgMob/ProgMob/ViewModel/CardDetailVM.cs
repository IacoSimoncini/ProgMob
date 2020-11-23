using ProgMob.Models;
using ProgMob.ViewModel.Helpers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProgMob.ViewModel
{
    public class CardDetailVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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

                    ListEx(App.Current.Properties["UID"].ToString(),
                        App.Current.Properties["CID"].ToString());

                    IsRefreshing = false;
                });
            }
        }


        public ObservableCollection<Exercise> Exercises { get; set; }
        public Command<object> DeleteCommand { get; set; }
        public CardDetailVM()
        {
            Exercises = new ObservableCollection<Exercise>();
            DeleteCommand = new Command<object>(Delete);
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
            Exercises.Clear();
            if(await DatabaseDetailCard.ListExercise(Uid, Cid)) 
            {
                var ex = await DatabaseDetailCard.GetExercises();
                foreach (var e in ex)
                {
                    Exercises.Add(e);
                }
            }
            
        }

        private async void Delete(object obj)
        {
            var ex = obj as Exercise;
            bool deleted = await DatabaseDetailCard.DeleteExercise(Application.Current.Properties["UID"].ToString(),
                Application.Current.Properties["CID"].ToString(),
                ex);
            if (deleted)
            {
                Exercises.Remove(ex);
                _ = App.Current.MainPage.DisplayAlert("Successfully deleted", "Please, press OK", "OK");
            }
            else
                _ = App.Current.MainPage.DisplayAlert("Error", "Something went wrong", "OK");
        }

    }
}
