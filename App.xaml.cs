using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace DzalekaNotifierFinal
{
    public partial class App : Application
    {
        public static string currentUser = "";
        public static string currentUserName = "";
        public static string currentAddress = "";
        public static string currentPhoneNumber = "";
        public static string currentEmail = "";
        public static string currentImage;
        public static string userOrOrganization = "";

        //public static string dates = "3/21/2018 7:00:00 PM";
        public static string newsType;


        public static bool isNetworkAvailable = true;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage ( new DzalekaNotifierFinal.Login());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
