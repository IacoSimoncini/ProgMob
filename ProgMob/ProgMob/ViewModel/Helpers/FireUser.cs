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
        Task<bool> DeleteUser(string Uid);
        Task<bool> UpdateUser(User User);
        Task<IList<User>> ListUser();

        
    }
    class DatabaseUser
    {
        private static FireUser firestoreUser = DependencyService.Get<FireUser>();

        public static bool InsertUser(User User)
        {
            return firestoreUser.InsertUser(User);
        }

        public static Task<bool> DeleteUser(string Uid)
        {
            return firestoreUser.DeleteUser(Uid);
        }

        public static Task<bool> UpdateUser(User User)
        {
            return firestoreUser.UpdateUser(User);
        }

        public static Task<IList<User>> ListUser()
        {
            return firestoreUser.ListUser();
        }
        
    }
}
