using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Firestore;
using Java.Util;
using ProgMob.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(ProgMob.Droid.Dependencies.FireUser))]
namespace ProgMob.Droid.Dependencies
{
    class FireUser : Java.Lang.Object, ViewModel.Helpers.FireUser, IOnCompleteListener
    {

        private bool Admin;

        public FireUser()
        {
        }

        public async Task<bool> DeleteUser(string Uid)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                    .Document(Uid)
                    .Delete();
                return true;
            } catch (Exception e)
            {
                return false;
            }
        }

        public bool InsertUser(User User)
        {
            try
            {
                User.Id = Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid;
                HashMap mp = new HashMap();
                mp.Put("name", User.Name);
                mp.Put("surname", User.Surname);
                mp.Put("admin", "noAdmin");
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                    .Document(User.Id)
                    .Set(mp);
                return true;
            } catch (Exception e)
            {
                return false;
            }
        }

        public Task<bool> UpdateUser(User User)
        {
            throw new NotImplementedException();
        }

        public bool GetAdmin()
        {
            var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users").Document(Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid).Get().AddOnCompleteListener(this);
            return Admin;
            
        }

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                HashMap mp = new HashMap();
                mp = (HashMap)task.Result;
                if (mp.Get("admin").Equals("admin"))
                {
                    Admin = true;
                }
                else
                {
                    Admin = false;
                }
            }
        }
    }
}