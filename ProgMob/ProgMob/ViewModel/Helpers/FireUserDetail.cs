using ProgMob.Models;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace ProgMob.ViewModel.Helpers
{
    public interface FireUserDetail
    {
        Task<bool> generateUserData();
        User getUserData();
    }
    class DatabaseUserDetail
    {
        private static FireUserDetail firestoreUserDetail = DependencyService.Get<FireUserDetail>();
        public static User getUserData()
        {
            return DatabaseUserDetail.firestoreUserDetail.getUserData();
        }
        public static Task<bool> generateUserData()
        {
            return DatabaseUserDetail.firestoreUserDetail.generateUserData();
        }




    }
}
