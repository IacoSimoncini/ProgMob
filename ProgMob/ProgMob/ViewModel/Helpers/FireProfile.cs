using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProgMob.ViewModel.Helpers
{
    public interface FireProfile
    {
        string ExtractProfileName();
    }
    class DatabaseProfile
    {
        private static FireProfile firestoreProfile = DependencyService.Get<FireProfile>();

        public static string ExtractProfileName()
        {
            return firestoreProfile.ExtractProfileName();
        }
    }
}
