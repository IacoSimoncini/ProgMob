using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(ProgMob.Droid.Dependencies.FireWeek))]
namespace ProgMob.Droid.Dependencies
{
    class FireWeek : Java.Lang.Object, ViewModel.Helpers.FireWeek, IOnCompleteListener
    {

        public FireWeek()
        {
            
        }

        public void OnComplete(Task task)
        {
            throw new NotImplementedException();
        }
    }
}