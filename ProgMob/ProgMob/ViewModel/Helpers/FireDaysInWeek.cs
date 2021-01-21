using ProgMob.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProgMob.ViewModel.Helpers
{
    public interface FireDaysInWeek
    {
        List<Week> GetWeeks();
        Task<bool> CheckDaysInWeek(string Uid, string type, int i);
        Week GetWhichWeek(string week);
        //string whichControl(string type);
    }

    class DatabaseDaysInWeek
    {
        private static FireDaysInWeek firestoreCalendary = DependencyService.Get<FireDaysInWeek>();
        public static Task<bool> CheckDaysInWeek(string Uid, string type, int i)
        {
            return firestoreCalendary.CheckDaysInWeek(Uid, type, i);
        }
        public static List<Week> GetWeeks()
        {
            return firestoreCalendary.GetWeeks();
        }
        public static Week GetWhichWeek(string week) 
        {
            return firestoreCalendary.GetWhichWeek(week);
        }
    }
}
