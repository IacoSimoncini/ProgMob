using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Gms.Extensions;
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
        public static string name;
        public static string surname;
        //Boolean flag;
        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                var documents = (DocumentSnapshot)task.Result;
                Console.WriteLine("1) HO ESTRATTO IL NOME: " + documents.Get("name").ToString() + "HO ESTRATTO IL COGNOME: " + documents.Get("surname").ToString());
                this.initializeUserData(documents.Get("name").ToString() , documents.Get("name").ToString());
                //flag = true;
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


        public void generateUserData()
        {
            //flag = false;
            string uid = (string)Application.Current.Properties["UID"];
            var document = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users").Document(uid);
            document.Get().AddOnCompleteListener(this);
            /*Console.WriteLine("FATTO"+userData.Name);
            while (!flag) { Console.WriteLine("aspetto"); }
            return userData;*/
        }

        public void initializeUserData(string n , string s )
        {
            name = null;
            surname = null;
            Console.WriteLine("2)PRIMA DI ASSEGNARE I VALORI IL NOME: " + name + " E IL COGNOME: " + surname);
            name = new string(n);
            surname = new string(s);
            Console.WriteLine("3)DOPO AVER ASSEGNATO I VALORI IL NOME: " + name + " E IL COGNOME: " + surname);

        }

        public User getUserData()
        {
            Console.WriteLine("4) CREO UTENTE CON NOME: " + name + " E COGNOME: " + surname);
            return new User(name, surname);
        }
    }
}