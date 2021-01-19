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
        
        Week DaysInWeek1;
        Week DaysInWeek2;
        Week DaysInWeek3;
        Week DaysInWeek4;
        int value;
        string currentType;
        string currentWeek;
        public FireDaysInWeek()
        {
            DaysInWeek1 = new Week();
            DaysInWeek2 = new Week();
            DaysInWeek3 = new Week();
            DaysInWeek4 = new Week();
        }

        public async Task<bool> CheckDaysInWeek(string Uid, string type, int i)
        {
            currentWeek = i.ToString();
            currentType = type;
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
        }


        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                var documents = (QuerySnapshot)task.Result;
                foreach(var doc in documents.Documents)
                {
                    DaysInWeek d = new DaysInWeek();
                    d.n = doc.Id;
                    d.week = currentWeek;
                    d.ifSet = false;
                    if ( doc.Get(whichTypeControl(currentType))!= null && doc.Get(whichTypeControl(currentType)).Equals("True") )
                    {
                        d.ifSet = true;
                    }
                    whichDayAdd(d, GetWhichWeek(currentWeek));
                    value = 1;
                }
            }
        }

        public string whichTypeControl(string type)
        {
            if (type.Equals("A"))
            {
                return "ifSetA";
            }
            else if (type.Equals("B"))
            {
                return "ifSetB";
            }
            else if (type.Equals("C"))
            {
                return "ifSetC";
            }
            else
            {
                return null;
            }
        }

        public void whichDayAdd(DaysInWeek d, Week w)
        {
            if(d.n.Equals("LUN")) { w.d1 = d; }
            else if (d.n.Equals("MAR")) { w.d2 = d; }
            else if (d.n.Equals("MER")) { w.d3 = d; }
            else if (d.n.Equals("GIO")) { w.d4 = d; }
            else if (d.n.Equals("VEN")) { w.d5 = d; }
            else if (d.n.Equals("SAB")) { w.d6 = d; }
            else if (d.n.Equals("DOM")) { w.d7 = d; }
        }

        public Week GetWhichWeek(string week)
        {
            if (week.Equals("1")) { return DaysInWeek1; }
            else if (week.Equals("2")) { return DaysInWeek2; }
            else if (week.Equals("3")) { return DaysInWeek3; }
            else if (week.Equals("4")) { return DaysInWeek4; }
            else { return null; }
        }

    }  
}
