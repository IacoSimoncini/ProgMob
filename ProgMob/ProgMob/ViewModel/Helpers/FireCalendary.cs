using ProgMob.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProgMob.ViewModel.Helpers
{
    public interface FireCalendary
    {
        Task<bool> ControlDay(string Uid, string type);
        Task<IList<Day>> getDays();
    }

    class DatabaseCalendary
    {
        private static FireCalendary firestoreCalendary = DependencyService.Get<FireCalendary>();
        public static Task<bool> ControlDay(string Uid, string type)
        {
            return firestoreCalendary.ControlDay(Uid, type);
        }
        public static Task<IList<Day>> getDays() 
        {
            return firestoreCalendary.getDays();
        }
    }
}
