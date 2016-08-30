using System;

using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using Xamarin.Forms.Platform.Android;

namespace LykkeColorex.Droid
{
    [Activity(Label = "LykkeColorex", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Window.AddFlags(WindowManagerFlags.TranslucentStatus);
            global::Xamarin.Forms.Forms.Init(this, bundle);

            DisplayMetrics displayMetrics = Resources.DisplayMetrics;
            App.Dimensions.Height = displayMetrics.HeightPixels / displayMetrics.Density;
            App.Dimensions.Width = displayMetrics.WidthPixels / displayMetrics.Density;

            LoadApplication(new App());


            /*DisplayMetrics displayMetrics = context.getResources().getDisplayMetrics();    
                float dpHeight = displayMetrics.heightPixels / displayMetrics.density;
                float dpWidth = displayMetrics.widthPixels / displayMetrics.density; */
        }
    }
}

