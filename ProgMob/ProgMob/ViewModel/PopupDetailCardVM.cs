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
            var exCards = await DatabaseDetailCard.GetExercises();
            var exAll = await DatabaseExercise.GetExercises();
            foreach (Exercise e in exAll)
            {
                foreach(var x in exCards)
                {
                    if (!e.Name.Equals(x.Name)) ListPopupEx.Add(e);
                }
            }
        }
    }
}
