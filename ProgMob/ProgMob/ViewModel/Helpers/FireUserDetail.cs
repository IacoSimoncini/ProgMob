using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProgMob.Models;
using Xamarin.Forms;
namespace ProgMob.ViewModel.Helpers
{
    public interface FireUserDetail
    {
        User getUserData();

    }
    class DatabaseUserDetail
    {
        private static FireUserDetail firestoreUserDetail = DependencyService.Get<FireUserDetail>();
        /*public static string getUserName()
        {
            return DatabaseUserDetail.firestoreUserDetail.getUserData().Name;
        }

        public static string getUserSurname()
        {
            return DatabaseUserDetail.firestoreUserDetail.getUserData().Surname;
        }*/
        public static User getUserData()
        {
            return DatabaseUserDetail.firestoreUserDetail.getUserData();
        }




    }
}
