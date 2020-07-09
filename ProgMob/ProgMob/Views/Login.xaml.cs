using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgMob.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgMob.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }
        void SignIn(object sender , EventArgs e)
        {

            DisplayAlert("login", "successful", "cancel");
        }

        async void Register(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Register());
        }
    }
}