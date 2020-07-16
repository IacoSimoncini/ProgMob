using System;
using Android.Gms.Tasks;
using Firebase.Firestore;
using Java.Util;

namespace ProgMob.Droid.Dependencies
{
    class FireProfile : Java.Lang.Object, ViewModel.Helpers.FireProfile, IOnCompleteListener
    {
        public string Name;
        public string Surname;
        public string ExtractProfileName()
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                    .Document(Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid);
                collection.Get().AddOnCompleteListener(this);
                return Name;
            } catch (Exception e)
            {
                return " ";
            }
        }

        public void OnComplete(Task task)
        {
            var document = (QuerySnapshot)task.Result;
            foreach (var doc in document.Documents)
            {
                if (doc.Get("name").ToString() != null) Name = doc.Get("name").ToString();
                else Name = " ";
                if (doc.Get("surname").ToString() != null) Surname = doc.Get("surname").ToString();
                else Surname = " ";
            }
        }
    }
}