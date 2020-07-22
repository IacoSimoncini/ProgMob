using ProgMob.Popup;
using ProgMob.ViewModel;
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
        public CardListPage(string Uid)
        {
            InitializeComponent();
            Title = "Cards";
            UserId = Uid;
            CardVM = Resources["CardViewModel"] as CardVM;
            
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