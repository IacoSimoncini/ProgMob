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
        bool GetProfile();
    }
    class DatabaseProfile
    {
        private static FireProfile firestoreProfile = DependencyService.Get<FireProfile>();

        public static bool GetProfile()
        {
            return firestoreProfile.GetProfile();
        }
    }
}
