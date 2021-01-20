using ProgMob.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProgMob.ViewModel.Helpers
{
    public interface FireUser
    {
        bool InsertUser(User User);
        bool DeleteUser(string Uid);
        bool UpdateUserPic(User User, string uri);
        Task<bool> ListUser();
        Task<IList<User>> GetUser();
        bool Update(User user);

    }
    class DatabaseUser
    {
        private static FireUser firestoreUser = DependencyService.Get<FireUser>();

        public static bool InsertUser(User User)
        {
            return firestoreUser.InsertUser(User);
        }

        public static bool DeleteUser(string Uid)
        {
            return firestoreUser.DeleteUser(Uid);
        }

        public static bool UpdateUserPic(User User, string uri)
        {
            return firestoreUser.UpdateUserPic(User, uri);
        }

        public static Task<bool> ListUser()
        {
            return firestoreUser.ListUser();
        }

        public static Task<IList<User>> GetUser()
        {
            return firestoreUser.GetUser();
        }

        public static bool Update(User user)
        {
            return firestoreUser.Update(user);
        }


    }
}
