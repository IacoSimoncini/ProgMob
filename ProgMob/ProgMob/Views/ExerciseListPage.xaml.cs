using ProgMob.Popup;
using ProgMob.ViewModel;
using ProgMob.ViewModel.Helpers;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgMob.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExerciseListPage : ContentPage
    {
        ExerciseVM ExVM;
        public ExerciseListPage()
        {
            InitializeComponent();
            Title = "Exercises";

            ExVM = Resources["ExVM"] as ExerciseVM;

            if (Application.Current.Properties["Admin"].ToString().Equals("true"))
            {
                Btn_AddEx.IsVisible = true;
            }
            else
            {
                Btn_AddEx.IsVisible = false;
            }

            ToolbarItem TBItem = new ToolbarItem
            {
                Text = "Logout",
                IconImageSource = ImageSource.FromFile("logout.png"),
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
            ExVM.ListAllExercise();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new PopupEx());
        }
    }
}