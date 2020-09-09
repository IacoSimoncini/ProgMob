using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProgMob.ViewModel.Helpers
{
    public interface FireProfile
    {
        Task<bool> GetProfile();
        string GetUid();
    }
    class DatabaseProfile
    {
        private static FireProfile firestoreProfile = DependencyService.Get<FireProfile>();

        public static Task<bool> GetProfile()
        {
            return firestoreProfile.GetProfile();
        }
        public static string GetUid()
        {
            return firestoreProfile.GetUid();
        }
    }
}
