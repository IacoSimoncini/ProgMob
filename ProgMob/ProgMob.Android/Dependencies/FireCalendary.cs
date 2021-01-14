using Android.Gms.Tasks;
using Firebase.Firestore;
using ProgMob.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ProgMob.Droid.Dependencies.FireCalendary))]
namespace ProgMob.Droid.Dependencies
{
    class FireCalendary : Java.Lang.Object, ViewModel.Helpers.FireCalendary, IOnCompleteListener
    {
        List<Day> daysList ;
        int value;
        string currentType;
        int currentIndex = 0;
        public FireCalendary()
        {
            daysList = new List<Day>();
        }
        public async Task<bool> ControlDay(string Uid, string type)
        {
            Console.WriteLine(Uid + " " + type);
            currentIndex = 0;
            daysList.Clear();
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
            return true;
        }

        public async Task<IList<Day>> getDays()
        {
            return daysList;
        }

        
        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                Day d = new Day();
                d.n=currentIndex;
                d.ifSet = false;
                var documents = (QuerySnapshot)task.Result;
                foreach (var doc in documents.Documents)
                {
                    if ((doc.Get("type") != null) && doc.Get("type").Equals(currentType))
                    {
                        d.ifSet= true;
                    }
                }
                Console.WriteLine("Corrente: " + currentIndex.ToString() + " Valore: " + d.n.ToString());
                daysList.Add(d);
                value = 1;
            }
            else
            {
                value = 2;
            }
        }
    }


   
}