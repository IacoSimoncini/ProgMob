using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProgMob.ViewModel.Helpers
{
    public interface FireWeek
    {

    }
    class DatabaseWeek
    {
        private static FireWeek firestoreWeek = DependencyService.Get<FireWeek>();
    }
}
