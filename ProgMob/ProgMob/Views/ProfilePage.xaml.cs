using ProgMob.Models;
using ProgMob.ViewModel.Helpers;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgMob.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        private User user;
        public ProfilePage()
        {
            InitializeComponent();

            Title = "Profile";
            BindingContext = this;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            StartUp();
        }

        public async void StartUp()
        {
            if (await DatabaseUserDetail.generateUserData())
            {
                user = DatabaseUserDetail.getUserData();
            }

            Uri uri = new Uri(user.Uri);
            ProfileImage.Source = ImageSource.FromUri(uri);
            name.Text = user.Name;
            User_Surname.Text = user.Surname;

            Console.WriteLine("UTENTE: " + user.Name + " " + user.Surname);
        }
    }
}