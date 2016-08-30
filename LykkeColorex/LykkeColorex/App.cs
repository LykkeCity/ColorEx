using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LykkeColorex.CustomViews;
using LykkeColorex.Pages;
using Xamarin.Forms;

namespace LykkeColorex
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            MainPage = new NavigationPage(new LoginPage());
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

        public static class Dimensions
        {
            public static double Height { set; get; }
            public static double Width { set; get; }
        }
    }
}
