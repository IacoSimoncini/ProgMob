﻿using ProgMob.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProgMob.ViewModel.Helpers
{
    public interface FireUser
    {
        bool InsertUser(User User);
        Task<bool> DeleteUser(string Uid);
        Task<bool> UpdateUser(User User);
        Task<bool> ListUser();
        Task<IList<User>> GetUser();

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

        public static Task<bool> ListUser()
        {
            return firestoreUser.ListUser();
        }

        public static Task<IList<User>> GetUser()
        {
            return firestoreUser.GetUser();
        }


    }
}
