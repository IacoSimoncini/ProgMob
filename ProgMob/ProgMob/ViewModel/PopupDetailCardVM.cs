using ProgMob.Models;
using ProgMob.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace ProgMob.ViewModel
{
    class PopupDetailCardVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Exercise selectedItem;
        public ObservableCollection<Exercise> ListPopupEx { get; set; }
        public Exercise SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    OnPropertyChanged("SelectedItem");

                }
            }
        }

        public PopupDetailCardVM()
        {
            ListPopupEx = new ObservableCollection<Exercise>();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void LoadListEx(string Uid, string Cid)
        {
            ListPopupEx.Clear();
            bool value = false;
            List<Exercise> exCards = (List<Exercise>)await DatabaseDetailCard.GetExercises();
            var exAll = await DatabaseExercise.GetExercises();
            foreach (Exercise e in exAll)
            {
                Console.WriteLine("e: " + e.Name);
                foreach (Exercise x in exCards)
                {
                    Console.WriteLine("x: " + x.Name);
                    if (x.Name.Equals(e.Name))
                    {
                        value = true;
                        break;
                    }
                    else
                        value = false;
                    Console.WriteLine("value: " + value.ToString());
                }
                if (!value)
                {
                    Console.WriteLine("Aggiungo esercizio");
                    ListPopupEx.Add(e);
                }
            }
        }
    }
}
