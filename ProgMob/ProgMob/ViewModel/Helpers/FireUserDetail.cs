using ProgMob.Models;
using Xamarin.Forms;
namespace ProgMob.ViewModel.Helpers
{
    public interface FireUserDetail
    {
        void generateUserData();
        void initializeUserData(string n, string s);
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
        /*
         public static void generateUserData()
         {
             DatabaseUserDetail.firestoreUserDetail.generateUserData();
         }*/

        public static User getUserData()
        {
            return DatabaseUserDetail.firestoreUserDetail.getUserData();
        }
        public static void generateUserData()
        {
            DatabaseUserDetail.firestoreUserDetail.generateUserData();
            //return 
        }




    }
}
