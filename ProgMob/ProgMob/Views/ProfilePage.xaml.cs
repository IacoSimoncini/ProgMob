using ProgMob.Models;
using ProgMob.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgMob.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        private string _lblText;
        private User user;
        public string LblText
        {
            get
            {
                return _lblText;
            }
            set
            {
                _lblText = value;
                OnPropertyChanged();
            }
        }
        public ProfilePage()
        {
            InitializeComponent();
            Console.WriteLine("A)GENERO I DATI");
            DatabaseUserDetail.generateUserData();
            Console.WriteLine("B)ASSEGNO I DATI");
            user = null;
            user = DatabaseUserDetail.getUserData();
            Title = "Profile";
            BindingContext = this;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
           
            //this.LblText = "hi";
        }
    }
}