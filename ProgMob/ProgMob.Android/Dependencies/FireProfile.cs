using System;
using System.Threading.Tasks;
using Android.Gms.Tasks;
using Firebase.Firestore;
using Java.Util;

namespace ProgMob.Droid.Dependencies
{
    class FireProfile : Java.Lang.Object, ViewModel.Helpers.FireProfile, IOnCompleteListener
    {
        public string Name;
        public string Surname;
        public bool Admin;
        public bool GetProfile()
        {
            var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                .Document(Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid);
            collection.Get().AddOnCompleteListener(this);
            return Admin; 
        }

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                var document = (QuerySnapshot)task.Result;
                foreach (var doc in document.Documents)
                {
                    Name = doc.Get("name").ToString();
                    Surname = doc.Get("surname").ToString();
                    if (doc.Get("admin").ToString().Equals("Admin"))
                        Admin = true;
                    else
                        Admin = false;
                }
            }
        }
    }
}