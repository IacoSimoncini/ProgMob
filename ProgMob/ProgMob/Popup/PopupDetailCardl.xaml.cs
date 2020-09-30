
using ProgMob.Models;
using ProgMob.ViewModel;
using ProgMob.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgMob.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupDetailCardl : Rg.Plugins.Popup.Pages.PopupPage
    {
        private readonly string UserId;
        private readonly string CardId;
        ExerciseVM ExVM;
        public List<String> selectedExs;
        public PopupDetailCardl(string Uid, string Cid)
        {
            InitializeComponent();
            ExVM = Resources["ExVM"] as ExerciseVM;
            selectedExs = new List<String>();
            UserId = Uid;
            CardId = Cid;
        }

        protected override void OnAppearing()
        {
           base.OnAppearing();
            ExVM.ListExercise(CardId, UserId);
            BindingContext = this;
            Console.WriteLine("PopupDC : 1)"+UserId," 2)"+ CardId);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            foreach (var y in selectedExs) {
                foreach (var x in ExVM.Exercises)
                {
                    if (x.Name.Equals(y)){
                        Console.WriteLine("Aggiungo " + y);
                        DatabaseDetailCard.InsertEx(UserId, CardId, x);
                    }
                }
            }
            selectedExs.Clear();
        }

        public void ClickedEx(object sender, EventArgs e)
        {
            var v = sender as Button;
            var s = v.Text;
            if (!selectedExs.Contains(s)) {
                selectedExs.Add(s);
                Console.WriteLine(s + "aggiunto");
            }
            else { selectedExs.Remove(s);
                Console.WriteLine(s + "rimosso");
            }
            
        }

    }
}