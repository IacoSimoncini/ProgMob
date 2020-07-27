using ProgMob.Models;
using ProgMob.ViewModel;
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
        ExerciseVM ExVM;
        public PopupDetailCardl()
        {
            InitializeComponent();

            ExVM = Resources["ExVM"] as ExerciseVM;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ExVM.ListExercise();
        }

        
    }
}