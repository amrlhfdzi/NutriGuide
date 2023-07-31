using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


namespace NutriGuide.Droid
{
    [Activity(Label = "NutriGuide", Icon = "@drawable/nutriguide", Theme = "@style/Splash", MainLauncher = true)]
    public class SplashActivity:Activity
    {
        public SplashActivity()
        {
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(typeof(MainActivity));
            Finish();
        }


    }
}