using ProgMob.Models;
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
    public partial class PopupUpdateEx : Rg.Plugins.Popup.Pages.PopupPage
    {
        public PopupUpdateEx()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            string name = Name.Text;
            string description = Description.Text;
            string difficulty = Difficulty.Text;
            Exercise ex = new Exercise(name, description, difficulty);
            DatabaseExercise.UpdateExercise(ex);
        }
    }
}