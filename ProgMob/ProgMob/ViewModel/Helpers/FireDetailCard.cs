using ProgMob.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProgMob.ViewModel.Helpers
{
    public interface FireDetailCard
    {
        bool InsertEx(string Uid, string Cid, Exercise ex, string day);
        Task<bool> DeleteExercise(string Uid, string Cid, Exercise ex, string day);
        Task<bool> UpdateExercise(string Uid, string Cid, Exercise ex);
        Task<bool> ListExercise(string Uid, string Cid, string day);
        Task<IList<Exercise>> GetExercises();
    }
    class DatabaseDetailCard
    {
        private static FireDetailCard firestoreDetailCard = DependencyService.Get<FireDetailCard>();

        public static bool InsertEx(string Uid, string Cid, Exercise ex , string day)
        {
            return firestoreDetailCard.InsertEx(Uid, Cid, ex , day);
        }

        public static Task<bool> DeleteExercise(string Uid, string Cid, Exercise ex, string day)
        {
            return firestoreDetailCard.DeleteExercise(Uid, Cid, ex , day);
        }

        public static Task<bool> ListExercise(string Uid, string Cid, string day)
        {
            return firestoreDetailCard.ListExercise(Uid, Cid, day);
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
