using System;
using System.Collections.Generic;
using System.Linq;
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
using Xamarin.Forms;

[assembly: Dependency(typeof(ProgMob.Droid.Dependencies.FireDetailCard))]
namespace ProgMob.Droid.Dependencies
{
    class FireDetailCard : Java.Lang.Object, ViewModel.Helpers.FireDetailCard, IOnCompleteListener
    {
        List<Exercise> exList;

        public FireDetailCard()
        {
            exList = new List<Exercise>();
        }

        public async Task<bool> DeleteExercise(string Uid, string Cid, Exercise ex)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                    .Document(Uid)
                    .Collection("Schede")
                    .Document(Cid)
                    .Collection("Ex")
                    .Document(ex.Name)
                    .Delete();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool InsertEx(string Uid, string Cid, Exercise ex)
        {
            try
            {
                HashMap mp = new HashMap();
                mp.Put("name", ex.Name);
                mp.Put("description", ex.Description);
                mp.Put("difficulty", ex.Difficulty);
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                    .Document(Uid)
                    .Collection("Schede")
                    .Document(Cid)
                    .Collection("Ex")
                    .Document(ex.Name);
                collection.Set(mp);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<IList<Exercise>> ListExercise(string Uid, string Cid)
        {
            var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                   .Document(Uid)
                   .Collection("Schede")
                   .Document(Cid)
                   .Collection("Ex");
            collection.Get().AddOnCompleteListener(this);
            return exList; 
        }

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                exList.Clear();
                var documents = (QuerySnapshot)task.Result;
                foreach (var doc in documents.Documents)
                {
                    Exercise ex = new Exercise(doc.Get("name").ToString(),
                        doc.Get("description").ToString(),
                        doc.Get("difficulty").ToString());
                    exList.Add(ex);
                }
            }
        }

        public Task<bool> UpdateExercise(string Uid, string Cid, Exercise ex)
        {
            throw new NotImplementedException();
        }
    }
}