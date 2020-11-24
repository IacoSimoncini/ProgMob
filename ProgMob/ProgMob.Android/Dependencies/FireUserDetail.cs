using Android.Gms.Tasks;
using Firebase.Firestore;
using ProgMob.Models;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ProgMob.Droid.Dependencies.FireUserDetail))]
namespace ProgMob.Droid.Dependencies
{
    class FireUserDetail : Java.Lang.Object, ViewModel.Helpers.FireUserDetail, IOnCompleteListener
    {
        int value;
        User user;

        public async Task<bool> generateUserData()
        {
            value = 0;
            string uid = Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid;
            var document = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users").Document(uid);
            document.Get().AddOnCompleteListener(this);

            for (int i = 0; i < 30; i++)
            {
                await System.Threading.Tasks.Task.Delay(100);
                if (value != 0)
                    break;
            }

            if (value == 1) return true;
            else return false;
        }


        public User getUserData()
        {
            return user;
        }

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                var document = (DocumentSnapshot)task.Result;
                user = new User(document.Get("name").ToString(),
                    document.Get("surname").ToString(),
                    document.Get("uri").ToString(),
                    document.Get("username").ToString(),
                    document.Get("email").ToString());
                value = 1;
            }
            else
                value = 2;
        }
    }
}