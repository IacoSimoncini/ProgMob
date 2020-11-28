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
        private User user;
        public ProfilePage()
        {
            InitializeComponent();

            Title = "Profile";

            ToolbarItem TBItem = new ToolbarItem
            {
                Text = "Logout",
                IconImageSource = "logout.png",
                Order = ToolbarItemOrder.Secondary,
                Priority = 0
            };

            TBItem.Clicked += async (sender, args) =>
            {
                bool logout = await Auth.Logout();
                if (logout)
                {
                    Application.Current.Properties["logged"] = "false";
                    await Application.Current.SavePropertiesAsync();
                    await DisplayAlert("Logout", "You have been disconnected, the app will be closed", "Cancel");
                    await System.Threading.Tasks.Task.Delay(1000);
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
                else await DisplayAlert("Logout", "It was not possible to disconnect", "Cancel");
            };
            this.ToolbarItems.Add(TBItem);


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

                ProfileImage.Source = user.Uri;
                name.Text = user.Name;
                surname.Text = user.Surname;
                email.Text = user.Email;
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
                string uri = await DatabaseProfile.UploadFile(file.GetStream(), user.Id);
                if (DatabaseUser.UpdateUserPic(user, uri))
                {
                    await DisplayAlert("Profile picture updated", "Press OK to continue", "OK");
                    Console.WriteLine("Propic Updated");
                }
                else
                {
                    await DisplayAlert("Error, profile picture update failure", "Press OK to continue", "OK");
                    Console.WriteLine("Error Propic");
                }
                StartUp();
            } 
            catch (Exception ex)
            {
                Console.WriteLine("Exception in ProfilePage: " + ex.Message);
            }
        }

        
    }
}