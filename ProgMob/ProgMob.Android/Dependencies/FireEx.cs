using Android.Gms.Tasks;
using Firebase.Firestore;
using Java.Util;
using ProgMob.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ProgMob.Droid.Dependencies.FireEx))]
namespace ProgMob.Droid.Dependencies
{
    class FireEx : Java.Lang.Object, ViewModel.Helpers.FireExercise, IOnCompleteListener
    {
        List<Exercise> exList;
        int value;
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
            }
            catch (Exception e)
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
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> ListExercise()
        {
            value = 0;
            var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Exercises");
            collection.Get().AddOnCompleteListener(this);

            for(int i = 0; i < 30; i++)
            {
                await System.Threading.Tasks.Task.Delay(100);
                if (value != 0)
                    break;
            }

            if (value == 1)
                return true;
            else
                return false;

        }

        public async Task<IList<Exercise>> GetExercises()
        {
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
                value = 1;
            }
            else
            {
                value = 2;
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
            }
            catch (Exception e)
            {
                return false;
            }
        }

       
    }
}