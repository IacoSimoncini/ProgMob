using Android.Gms.Tasks;
using Firebase.Firestore;
using Java.Util;
using ProgMob.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ProgMob.Droid.Dependencies.FireDetailCard))]
namespace ProgMob.Droid.Dependencies
{
    class FireDetailCard : Java.Lang.Object, ViewModel.Helpers.FireDetailCard, IOnCompleteListener
    {
        List<Exercise> exList;
        int value;
        public FireDetailCard()
        {
            exList = new List<Exercise>();
        }

        public async Task<bool> DeleteExercise(string Uid, string Cid, Exercise ex, string day)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                    .Document(Uid)
                    .Collection(day)
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

        public async Task<IList<Exercise>> GetExercises()
        {
            return exList;
        }

        public bool InsertEx(string Uid, string Cid, Exercise ex, string day)
        {
            try
            {
                HashMap mp = new HashMap();
                mp.Put("name", ex.Name);
                mp.Put("description", ex.Description);
                mp.Put("difficulty", ex.Difficulty);
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                    .Document(Uid)
                    .Collection(day)
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

        public async Task<bool> ListExercise(string Uid, string Cid , string day)
        {
            value = 0;
            var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                   .Document(Uid)
                   .Collection(day)
                   .Document(Cid)
                   .Collection("Ex");
            collection.Get().AddOnCompleteListener(this);

            for(int i = 0; i < 30; i++)
            {
                await System.Threading.Tasks.Task.Delay(100);
                if (value != 0)
                    break;
            }

            if (value == 1) return true;
            else return false;
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
                value = 1;
            }
            else
            {
                value = 2;
            }
        }

        public Task<bool> UpdateExercise(string Uid, string Cid, Exercise ex)
        {
            throw new NotImplementedException();
        }
    }
}