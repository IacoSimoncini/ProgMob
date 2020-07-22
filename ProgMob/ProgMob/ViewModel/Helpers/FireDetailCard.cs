using ProgMob.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProgMob.ViewModel.Helpers
{
    public interface FireDetailCard
    {
        bool InsertEx(string Uid, string Cid, Exercise ex);
        Task<bool> DeleteExercise(string Uid, string Cid, Exercise ex);
        Task<bool> UpdateExercise(string Uid, string Cid, Exercise ex);
        Task<IList<Exercise>> ListExercise(string Uid, string Cid);
    }
    class DatabaseDetailCard
    {
        private static FireDetailCard firestoreDetailCard = DependencyService.Get<FireDetailCard>();

        public static bool InsertEx(string Uid, string Cid, Exercise ex)
        {
            return firestoreDetailCard.InsertEx(Uid, Cid, ex);
        }

        public static Task<bool> DeleteExercise(string Uid, string Cid, Exercise ex)
        {
            return firestoreDetailCard.DeleteExercise(Uid, Cid, ex);
        }

        public static Task<IList<Exercise>> ListExercise(string Uid, string Cid)
        {
            return firestoreDetailCard.ListExercise(Uid, Cid);
        }

        public static Task<bool> UpdateExercise(string Uid, string Cid, Exercise ex)
        {
            return firestoreDetailCard.UpdateExercise(Uid, Cid, ex);
        }
    }
}
