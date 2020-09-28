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
        private string nameUser;
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
            Title = "Profile";
            BindingContext = this;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //Console.WriteLine("KOKOMANIDOWN"+DatabaseUserDetail.getUserData());
            this.LblText = "hi";
        }
    }
}