using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MSAApp
{
    public class App : Application
    {
        public static NavigationPage NavigationPage { get; private set; }

        public App()
        {
            NavigationPage = new NavigationPage(new LoginPage());

            MainPage = NavigationPage;
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
