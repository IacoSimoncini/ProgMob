using Firebase.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using ProgMob.Models;
using ProgMob.ViewModel.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgMob.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        MediaFile file;
        HelpStorage helpStorage;
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

                helpStorage = new HelpStorage(user);

                ProfileImage.Source = user.Uri;
                name.Text = user.Name;
                surname.Text = user.Surname;
                email.Text = user.Email;
                Console.WriteLine("URI: " + user.Uri);
            }

        }

        private async void Btn_PickImage_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                if (file == null)
                    return;
                ProfileImage.Source = ImageSource.FromStream(() =>
                {
                    var imageStram = file.GetStream();
                    return imageStram;
                });
                helpStorage.UploadFile(file.GetStream());
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        
    }
}