﻿using ProgMob.ViewModel.Helpers;
using ProgMob.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgMob
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : Shell
    {
        /*
         *  Shell relativa all'utente non admin
         */
        public MainPage()
        {
            InitializeComponent();
            Application.Current.Properties["logged"] = "true";
            Application.Current.Properties["Admin"] = "false";
            Application.Current.Properties["UID"] = DatabaseProfile.GetUid();
            Application.Current.SavePropertiesAsync();
        }
    }
}
