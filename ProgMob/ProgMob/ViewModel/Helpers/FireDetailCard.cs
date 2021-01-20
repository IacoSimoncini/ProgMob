using ProgMob.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProgMob.ViewModel.Helpers
{
    public interface FireDetailCard
    {
        bool InsertEx(string Uid, string Cid, Exercise ex, string day, string week);
        Task<bool> DeleteExercise(string Uid, string Cid, Exercise ex, string day, string week);
        Task<bool> UpdateExercise(string Uid, string Cid, Exercise ex);
        Task<bool> ListExercise(string Uid, string Cid, string day, string week);
        Task<IList<Exercise>> GetExercises();
    }
    class DatabaseDetailCard
    {
        private static FireDetailCard firestoreDetailCard = DependencyService.Get<FireDetailCard>();

        public static bool InsertEx(string Uid, string Cid, Exercise ex , string day, string week)
        {
            return firestoreDetailCard.InsertEx(Uid, Cid, ex , day, week);
        }

        public static Task<bool> DeleteExercise(string Uid, string Cid, Exercise ex, string day, string week)
        {
            return firestoreDetailCard.DeleteExercise(Uid, Cid, ex , day, week);
        }

        public static Task<bool> ListExercise(string Uid, string Cid, string day, string week)
        {
            return firestoreDetailCard.ListExercise(Uid, Cid, day, week);
        }

        public static Task<bool> UpdateExercise(string Uid, string Cid, Exercise ex)
        {
            return firestoreDetailCard.UpdateExercise(Uid, Cid, ex);
        }

        public static Task<IList<Exercise>> GetExercises()
        {
            return firestoreDetailCard.GetExercises();
        }
    }
}
