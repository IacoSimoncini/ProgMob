using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Database;

namespace ProgMob.Droid.Helpers
{
    public static class AppData
    {
        public static FirebaseDatabase GetDatabase()
        {
            var app = FirebaseApp.InitializeApp(Application.Context);
            FirebaseDatabase database;

            if(app == null)
            {
                var option = new FirebaseOptions.Builder()
                    .SetApplicationId("progmobxam-af105")
                    .SetApiKey("AIzaSyDf7QAh_apHDta9HvV6ODs6jqLR-9nIlpw")
                    .SetDatabaseUrl("https://progmobxam-af105.firebaseio.com")
                    .SetStorageBucket("progmobxam-af105.appspot.com")
                    .Build();

                app = FirebaseApp.InitializeApp(Application.Context, option);
                database = FirebaseDatabase.GetInstance(app);
            }
            else
            {
                database = FirebaseDatabase.GetInstance(app);
            }

            return database;
        }
    }
}