using System;
using System.Threading.Tasks;
using Android.Content.Res;
using Android.Gms.Tasks;
using Firebase.Firestore;
using Java.Lang;
using Java.Util;
using Xamarin.Forms;

[assembly: Dependency(typeof(ProgMob.Droid.Dependencies.FireProfile))]
namespace ProgMob.Droid.Dependencies
{
    class FireProfile : Java.Lang.Object, ViewModel.Helpers.FireProfile, IOnCompleteListener
    {
        public bool Admin = true;
        public bool Read;
        public async Task<bool> GetProfile()
        {
            Read = false;
            var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                .Document(Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid);
            collection.Get().AddOnCompleteListener(this);
            for (int i = 0; i < 25; i++)
            {
                await System.Threading.Tasks.Task.Delay(1000);
                if (Read == true)
                    break;
            }
            if (Read == false)
                return false;
            else
                return Admin;                
        }

        public string GetUid()
        {
            return Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid;
        }

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                var document = (DocumentSnapshot)task.Result;
                Console.WriteLine(document.Get("admin").ToString());
                if (document.Get("admin").ToString().Equals("Admin")) {
                    Admin = true;
                }
                else
                {
                    Admin = false;
                }
                Read = true;
               
            }
        }
    }
}