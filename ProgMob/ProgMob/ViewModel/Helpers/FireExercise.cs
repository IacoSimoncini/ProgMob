using ProgMob.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProgMob.ViewModel.Helpers
{
    public interface FireExercise
    {
        bool InsertExercise(Exercise ex);
        Task<bool> DeleteExercise(string Eid);
        Task<bool> UpdateExercise(Exercise ex);
        Task<bool> ListExercise();
        Task<IList<Exercise>> GetExercises();
    }
    class DatabaseExercise
    {
        private static FireExercise firestoreEx = DependencyService.Get<FireExercise>();

        public static bool InsertExercise(Exercise ex)
        {
            return firestoreEx.InsertExercise(ex);
        }

        public static Task<bool> DeleteExercise(string Eid)
        {
            return firestoreEx.DeleteExercise(Eid);
        }

        public static Task<bool> ListExercise()
        {
            return firestoreEx.ListExercise();
        }

        public static Task<bool> UpdateExercise(Exercise ex)
        {
            return firestoreEx.UpdateExercise(ex);
        }

        public static Task<IList<Exercise>> GetExercises()
        {
            return firestoreEx.GetExercises();
        }

    }
}
