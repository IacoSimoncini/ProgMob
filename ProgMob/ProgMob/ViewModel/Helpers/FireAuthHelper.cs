using ProgMob.Models;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProgMob.ViewModel.Helpers
{
    public interface InterfaceFireAuth
    {
        Task<string> LoginToFirebase(string email, string password);
        Task<string> RegisterToFirebase(string username, string email, string password);
        Task<bool> Logout();
        Task<bool> UpdateEmailToFirebase(User user);

    }

    public class Auth
    {
        private static InterfaceFireAuth auth = DependencyService.Get<InterfaceFireAuth>();

        public static async Task<string> LoginToFirebase(string email, string password)
        {
            try
            {
                return await auth.LoginToFirebase(email, password);
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public static async Task<bool> UpdateEmailToFirebase(User user)
        {
            try
            {
                return await auth.UpdateEmailToFirebase(user);
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "Cancel");
                return false; ;
            }
        }

        public static async Task<string> RegisterToFirebase(string username, string email, string password)
        {
            try
            {
                return await auth.RegisterToFirebase(username, email, password);
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public static async Task<bool> Logout()
        {
            try
            {
                return await auth.Logout();
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "Cancel");
                return false;
            }
        }
    }
}
