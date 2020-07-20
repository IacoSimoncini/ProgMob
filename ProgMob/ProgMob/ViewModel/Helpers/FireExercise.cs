using ProgMob.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProgMob.ViewModel.Helpers
{
    public interface FireExercise
    {
        bool InsertExercise(Exercise ex);
        Task<bool> DeleteExercise(string Eid);
        Task<bool> UpdateExercise(Exercise ex);
        Task<IList<Exercise>> ListExercise();
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

        public static Task<IList<Exercise>> ListExercise()
        {
            return firestoreEx.ListExercise();
        }

        public static Task<bool> UpdateExercise(Exercise ex)
        {
            return firestoreEx.UpdateExercise(ex);
        }
    }
}
