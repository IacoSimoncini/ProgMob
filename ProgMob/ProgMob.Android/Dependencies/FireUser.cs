using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Gms.Tasks;
using Firebase.Firestore;
using Java.Util;
using ProgMob.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(ProgMob.Droid.Dependencies.FireUser))]
namespace ProgMob.Droid.Dependencies
{
    class FireUser : Java.Lang.Object, ViewModel.Helpers.FireUser, IOnCompleteListener
    {

        List<User> userList;
        int value;
        public FireUser()
        {
            userList = new List<User>();
        }

        public async Task<bool> DeleteUser(string Uid)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                    .Document(Uid)
                    .Delete();
                return true;
            } catch (System.Exception e)
            {
                return false;
            }
        }

        public async Task<IList<User>> GetUser()
        {
            return userList;
        }

        public bool InsertUser(User User)
        {
            try
            {
                HashMap mp = new HashMap();
                mp.Put("name", User.Name);
                mp.Put("surname", User.Surname);
                mp.Put("admin", "noAdmin");
                mp.Put("uri", "user.png");
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                    .Document(Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid)
                    .Set(mp);
                return true;
            } catch (System.Exception e)
            {
                return false;
            }
        }

        public async Task<bool> ListUser()
        {
            value = 0;
            var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users");
            collection.Get().AddOnCompleteListener(this);
            
            for(int i = 0; i < 30; i++)
            {
                await System.Threading.Tasks.Task.Delay(100);
                if (value != 0)
                    break;
            }

            if(value == 1) { return true; }
            else { return false; }
        }

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                userList.Clear();
                var documents = (QuerySnapshot)task.Result;
                foreach (var doc in documents.Documents)
                {
                    if (!doc.Id.Equals(Application.Current.Properties["UID"])) 
                    { 
                        string name = doc.Get("name").ToString();
                        string surname = doc.Get("surname").ToString();
                        User user = new User(name, surname);
                        user.Id = doc.Id.ToString();
                        user.Uri = doc.Get("uri").ToString();
                        userList.Add(user);
                    }
                }
                value = 1;
            } 
            else
            {
                Console.WriteLine(task.Exception);
                value = 2;
            }
        }

       

        public Task<bool> UpdateUser(User User)
        {
            throw new NotImplementedException();
        }

    }
}