using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Gms.Tasks;
using Firebase.Firestore;
using Java.Util;
using ProgMob.Models;
using Xamarin.Forms;
[assembly: Dependency(typeof(ProgMob.Droid.Dependencies.FireUserDetail))]
namespace ProgMob.Droid.Dependencies
{
    class FireUserDetail : Java.Lang.Object, ViewModel.Helpers.FireUserDetail, IOnCompleteListener
    {
        User userData;
        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                userData = null;
                var documents = (DocumentSnapshot)task.Result;
                string name = documents.Get("name").ToString();
                string surname = documents.Get("surname").ToString();
                //Console.WriteLine("Name:" + name + "  Surename:" + surname+ "  Id:" +documents.Id);
                userData = new User(name, surname);
                Console.WriteLine("Name:" + userData.Name + "  Surename:" + userData.Surname);
            }
        }
            //string name = (QuerySnapshot)task.Result.Get("name").ToString();
            //string surname = (QuerySnapshot)task.Result.Get("surname").ToString();
            /*var documents = (QuerySnapshot)task.Result;
            foreach (var doc in documents.Documents)
            {
                string name = doc.Get("name").ToString();
                string surname = doc.Get("surname").ToString();
                userData = new User(name, surname);
                /*user.Id = doc.Id.ToString();
                user.Uri = doc.Get("uri").ToString();
                userList.Add(user);*/
            // }

        public User getUserData()
        {
            string uid = (string)Application.Current.Properties["UID"];
            var document = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users").Document(uid);
            document.Get().AddOnCompleteListener(this);
            return userData;
        }


}
}