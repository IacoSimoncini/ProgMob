﻿using Android.Gms.Tasks;
using Firebase.Firestore;
using Java.Util;
using ProgMob.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public bool DeleteUser(string Uid)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                    .Document(Uid)
                    .Delete();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
                mp.Put("uri", User.Uri);
                mp.Put("username", User.Username);
                mp.Put("email", User.Email);
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                    .Document(Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid);
                collection.Set(mp);
                HashMap mp2 = new HashMap();
                mp2.Put(1, "LUN");
                mp2.Put(2, "MAR");
                mp2.Put(3, "MER");
                mp2.Put(4, "GIO");
                mp2.Put(5, "VEN");
                mp2.Put(6, "SAB");
                mp2.Put(7, "DOM");
                HashMap mp3 = new HashMap();
                mp3.Put("ifSetA", "False");
                mp3.Put("ifSetB", "False");
                mp3.Put("ifSetC", "False");
                for (int i = 1; i <= 4; i++)
                {
                    for(int j = 1; j <= 7; j++)
                    {
                        collection.Collection(i.ToString()).Document(mp2.Get(j).ToString()).Set(mp3);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> ListUser()
        {
            value = 0;
            var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users");
            collection.Get().AddOnCompleteListener(this);

            for (int i = 0; i < 30; i++)
            {
                await System.Threading.Tasks.Task.Delay(100);
                if (value != 0)
                    break;
            }

            if (value == 1) { return true; }
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
                    if (!doc.Id.Equals(Application.Current.Properties["MyUID"]) && !doc.Get("admin").Equals("Admin"))
                    {
                        string name = doc.Get("name").ToString();
                        string surname = doc.Get("surname").ToString();
                        string uri = doc.Get("uri").ToString();
                        string username = doc.Get("username").ToString();
                        string email = doc.Get("email").ToString();
                        User user = new User(name, surname, uri, username, email);
                        user.Id = doc.Id.ToString();
                        userList.Add(user);
                    }
                }
                value = 1;
            }
            else
            {
                value = 2;
            }
        }

        public bool Update(User user)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                    .Document(user.Id)
                    .Update("name", user.Name,
                        "email", user.Email,
                        "surname", user.Surname,
                        "username", user.Username);
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            
        }

        public bool UpdateUserPic(User User, string uri)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                    .Document(User.Id)
                    .Update("uri", uri);
                return true;
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

    }
}