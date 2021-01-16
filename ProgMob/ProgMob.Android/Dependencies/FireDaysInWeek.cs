using Android.Gms.Tasks;
using Firebase.Firestore;
using ProgMob.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ProgMob.Droid.Dependencies.FireDaysInWeek))]
namespace ProgMob.Droid.Dependencies
{
    class FireDaysInWeek : Java.Lang.Object, ViewModel.Helpers.FireDaysInWeek, IOnCompleteListener
    {
        
        DaysInWeek DaysInWeek;
        int value;
        string currentType;
        int currentIndex = 0;
        public FireDaysInWeek()
        {
            DaysInWeek = new DaysInWeek();
        }

        public async Task<bool> CheckDaysInWeek(string Uid, string type, int i)
        {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                .Document(Uid)
                .Collection(i.ToString());
                collection.Get().AddOnCompleteListener(this);

                for (int j = 0; j < 30; j++)
                {
                    await System.Threading.Tasks.Task.Delay(100);
                    if (value != 0)
                        break;
                }
            
            
            return true;

            /*currentIndex = 0;
            DaysInWeekList.Clear();
            currentType = type;
            int day = 0;
            while (day < 28)
            {
                value = 0;
                currentIndex = day+1;
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("Users")
                .Document(Uid)
                .Collection((currentIndex).ToString());
                collection.Get().AddOnCompleteListener(this);
                for (int i = 0; i < 30; i++)
                {
                    await System.Threading.Tasks.Task.Delay(100);
                    if (value != 0)
                        break;
                }
                day = day + 1;
            }
            return true;*/
        }

        public DaysInWeek GetDaysInWeek()
        {
            return DaysInWeek;
        }

        /*
        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                DayInWeek d = new DayInWeek();
                d.n=currentIndex;
                d.ifSet = false;
                var documents = (QuerySnapshot)task.Result;
                foreach (var doc in documents.Documents)
                {
                    if ((doc.Get("type") != null) && doc.Get("type").Equals(currentType))
                    {
                        d.ifSet = true;
                    }
                }
                DaysInWeekList.Add(d);
                value = 1;
            }
            else
            {
                value = 2;
            }
        }*/

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                var documents = (QuerySnapshot)task.Result;
                foreach(var doc in documents.Documents)
                {
                    DaysInWeek DayInWeek = new DaysInWeek();
                    if(!doc.Get("namecard").Equals(""))
                    {
                        DayInWeek.CardInWeek[doc.Id] = doc.Get("namecard").ToString();
                    }
                    else
                    {
                        DayInWeek.CardInWeek[doc.Id] = "no";
                    }
                    DaysInWeek = DayInWeek;
                }

            }
        }
    }  
}