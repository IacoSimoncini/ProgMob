using ProgMob.ViewModel;
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
    }
}