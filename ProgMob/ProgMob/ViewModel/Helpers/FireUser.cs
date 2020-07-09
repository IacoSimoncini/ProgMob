using ProgMob.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProgMob.ViewModel.Helpers
{
    public interface FireUser
    {
        bool InsertUser(User User);
        Task<bool> DeleteUser(User User);
        Task<bool> UpdateUser(User User);
        
    }
    class DatabaseUser
    {
        private static FireUser firestore = DependencyService.Get<FireUser>();

        public static bool InsertUser(User User)
        {
            return firestore.InsertUser(User);
        }

        public static Task<bool> DeleteUser(User User)
        {
            return firestore.DeleteUser(User);
        }

        public static Task<bool> UpdateUser(User User)
        {
            return firestore.UpdateUser(User);
        }
        
    }
}
