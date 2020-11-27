using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProgMob.ViewModel.Helpers
{
    public interface FireProfile
    {
        Task<string> GetPic(string Uid);
        Task<string> GetDefaultPic();
        Task<string> UploadFile(Stream fileStream, string Uid);
        Task<bool> GetProfile();
        string GetUid();
    }
    class DatabaseProfile
    {
        private static FireProfile firestoreProfile = DependencyService.Get<FireProfile>();

        public static Task<string> GetPic(string Uid)
        {
            return firestoreProfile.GetPic(Uid);
        }
        public static Task<string> GetDefaultPic()
        {
            return firestoreProfile.GetDefaultPic();
        }
        public static Task<bool> GetProfile()
        {
            return firestoreProfile.GetProfile();
        }
        public static string GetUid()
        {
            return firestoreProfile.GetUid();
        }
        public static Task<string> UploadFile(Stream fileStream, string Uid)
        {
            return firestoreProfile.UploadFile(fileStream, Uid);
        }
    }
}
