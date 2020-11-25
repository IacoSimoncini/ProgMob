using ProgMob.Models;
using Rg.Plugins.Popup.Services;
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
    public partial class PopupDetailExercise : Rg.Plugins.Popup.Pages.PopupPage
    {
        public PopupDetailExercise(Exercise ex)
        {
            InitializeComponent();
            name.Text = ex.Name;
            description.Text = ex.Description;
            difficulty.Text = ex.Difficulty;
        }


        private void Button_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }
    }
}