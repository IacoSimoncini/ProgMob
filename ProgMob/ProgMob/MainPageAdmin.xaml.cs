using ProgMob.ViewModel.Helpers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgMob
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageAdmin : Shell
    {
        /*
         *  Shell relativa all'utente admin
         */
        public MainPageAdmin()
        {
            InitializeComponent();
            Application.Current.Properties["logged"] = "true";
            Application.Current.Properties["Admin"] = "true";
            Application.Current.Properties["UID"] = DatabaseProfile.GetUid();
            Application.Current.SavePropertiesAsync();

        }
    }
}