using ProgMob.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgMob.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupTimer : Rg.Plugins.Popup.Pages.PopupPage
    {
        private readonly string CardId;
        private readonly string UserId;
        private readonly string Name;
        public PopupTimer(string Cid, string Uid, string name)
        {
            InitializeComponent();
            CardId = Cid;
            UserId = Uid;
            Name = name;
            Application.Current.Properties["value"] = "15";
            Application.Current.SavePropertiesAsync();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if(Difficulty.SelectedItem.ToString() != null)
            {
                if (Difficulty.SelectedItem.ToString().Equals("Hard"))
                {
                    Application.Current.Properties["value"] = "15";
                    Application.Current.SavePropertiesAsync();
                }
                else
                {
                    if (Difficulty.SelectedItem.ToString().Equals("Medium"))
                    {
                        Application.Current.Properties["value"] = "10";
                        Application.Current.SavePropertiesAsync();
                    }
                    else
                    {
                        if (Difficulty.SelectedItem.ToString().Equals("Easy"))
                        {
                            Application.Current.Properties["value"] = "5";
                            Application.Current.SavePropertiesAsync();
                        }
                    }
                }
                App.Current.MainPage.Navigation.PushAsync(new TimerPage(CardId, UserId, Name, Difficulty.SelectedItem.ToString()));
                PopupNavigation.PopAsync();
            }
            else
            {
                _ = App.Current.MainPage.DisplayAlert("No selection", "Please, select a difficulty", "OK");
            }
        }
    }
}