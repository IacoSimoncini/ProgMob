using ProgMob.Popup;
using ProgMob.ViewModel;
using ProgMob.ViewModel.Helpers;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgMob.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardListPage : ContentPage
    {
        private readonly string UserId;
        CardVM CardVM;
        public CardListPage()
        {
            InitializeComponent();
            Title = "Cards";
            UserId = Application.Current.Properties["UID"].ToString();
            CardVM = Resources["CardViewModel"] as CardVM;

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
                    await DisplayAlert("Logout", "You have been disconnected", "Cancel");
                    await Application.Current.MainPage.Navigation.PushModalAsync(new RegisterPage(), true);
                }
                else await DisplayAlert("Logout", "It was not possible to disconnect", "Cancel");
            };
            this.ToolbarItems.Add(TBItem);

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CardVM.ListCard(UserId);
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            await PopupNavigation.PushAsync(new PopupCard(UserId));
        }
    }
}