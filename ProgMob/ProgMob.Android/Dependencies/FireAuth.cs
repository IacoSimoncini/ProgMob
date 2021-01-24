using Firebase.Auth;
using ProgMob.Models;
using ProgMob.ViewModel.Helpers;
using System;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(ProgMob.Droid.Dependencies.FireAuth))]
namespace ProgMob.Droid.Dependencies
{
    class FireAuth : InterfaceFireAuth
    {
        public FireAuth()
        {
        }

        /**
         * Login with Firebase method
         * 
         */
        public async Task<string> LoginToFirebase(string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                var token = await user.User.GetIdTokenAsync(false);
                return token.Token;
            }
            catch (FirebaseAuthInvalidUserException notFound)
            {
                notFound.PrintStackTrace();
                return "";
            }

        }

        /**
         * Register with Firebase method
         * 
         */
        public async Task<string> RegisterToFirebase(string username, string email, string password)
        {
            try
            {
                var create = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                var profileUpdates = new Firebase.Auth.UserProfileChangeRequest.Builder();
                profileUpdates.SetDisplayName(username);

                var user = Firebase.Auth.FirebaseAuth.Instance.CurrentUser;
                await user.UpdateProfileAsync(profileUpdates.Build());
                var token = await create.User.GetIdTokenAsync(false);
                return token.Token;
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public async Task<bool> UpdateEmailToFirebase(User user)
        {
            try
            {
                var profileUpdates = new Firebase.Auth.UserProfileChangeRequest.Builder();
                profileUpdates.SetDisplayName(user.Username);
                var us = Firebase.Auth.FirebaseAuth.Instance.CurrentUser;
                var token = us.UpdateEmailAsync(user.Email);
                return token.IsCompleted;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /**
         * Logout with Firebase method
         * 
         */
        public async Task<bool> Logout()
        {
            try
            {
                Firebase.Auth.FirebaseAuth.Instance.SignOut();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}