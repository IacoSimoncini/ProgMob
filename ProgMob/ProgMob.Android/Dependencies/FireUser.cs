﻿using System;
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

        public async Task<IList<User>> ListUser()
        {
            var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users");
            collection.Get().AddOnCompleteListener(this);
            return userList;
        }

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                userList.Clear();
                var documents = (QuerySnapshot)task.Result;
                foreach (var doc in documents.Documents)
                {
                    string name = doc.Get("name").ToString();
                    string surname = doc.Get("surname").ToString();
                    string id = doc.Id.ToString();
                    User user = new User(name, surname, id);
                    userList.Add(user);
                    Console.WriteLine(name + " " + surname + " " + id);
                }
            }
        }

        public Task<bool> UpdateUser(User User)
        {
            throw new NotImplementedException();
        }

        
    }
}