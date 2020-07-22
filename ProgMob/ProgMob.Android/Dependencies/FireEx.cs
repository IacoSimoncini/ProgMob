using System;
using Xamarin.Forms;
using ProgMob.Models;
using System.Security.Policy;
using Java.Util;
using System.Threading.Tasks;
using System.Collections.Generic;
using Android.Gms.Tasks;
using Firebase.Firestore;

[assembly: Dependency(typeof(ProgMob.Droid.Dependencies.FireEx))]
namespace ProgMob.Droid.Dependencies
{
    class FireEx : Java.Lang.Object, ViewModel.Helpers.FireExercise, IOnCompleteListener
    {
        List<Exercise> exList;

        public FireEx()
        {
            exList = new List<Exercise>();
        }

        public bool InsertExercise(Exercise ex)
        {
            try
            {
                HashMap mp = new HashMap();
                mp.Put("name", ex.Name);
                mp.Put("description", ex.Description);
                mp.Put("difficulty", ex.Difficulty);
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Exercises")
                    .Document();
                collection.Set(mp);
                ex.Id = collection.Id.ToString();
                return true;
            } catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> DeleteExercise(string Eid)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Exercises")
                    .Document(Eid)
                    .Delete();
                return true;
            } catch (Exception e)
            {
                return false;
            }
        }

        public async Task<IList<Exercise>> ListExercise()
        {
            var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Exercises");
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
                    string name = doc.Get("name").ToString();
                    string description = doc.Get("description").ToString();
                    string difficulty = doc.Get("difficulty").ToString();
                    Exercise ex = new Exercise(name, description, difficulty);
                    ex.Id = doc.Id.ToString();
                    exList.Add(ex);
                }
            }
        }

        public async Task<bool> UpdateExercise(Exercise ex)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Exercises")
                    .Document(ex.Id)
                    .Update("name", ex.Name, "description", ex.Description, "difficulty", ex.Difficulty);
                return true;
            } catch (Exception e)
            {
                return false;
            }
        }

        
    }
}