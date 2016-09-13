using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using Android.Views.InputMethods;
using LykkeColorex.CustomViews;
using LykkeColorex.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace LykkeColorex.Droid
{

    public class MainActivityInstance
    {
        public static MainActivity Instance { set; get; }
    }


    [Activity(Label = "LykkeColorex", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity
    {
        public bool CreepySolution { set; get; }

        protected override void OnCreate(Bundle bundle)
        {
            MainActivityInstance.Instance = this;

            base.OnCreate(bundle);

            Window.ClearFlags(WindowManagerFlags.TranslucentStatus);

            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            Window.SetStatusBarColor(Color.FromHex("999999").ToAndroid());

            Window.SetSoftInputMode(SoftInput.AdjustPan);


            global::Xamarin.Forms.Forms.Init(this, bundle);

            DisplayMetrics displayMetrics = Resources.DisplayMetrics;
            App.Dimensions.Height = displayMetrics.HeightPixels / displayMetrics.Density - 25;
            App.Dimensions.Width = displayMetrics.WidthPixels / displayMetrics.Density;

            LoadApplication(new App());

            CreepySolution = false;
        }

        public List<Rectangle> Areas { set; get; }

        public Rectangle? InsensitiveArea { set; get; }

        private bool ClickInsideRect(double x, double y, Rectangle? rect)
        {
            if (rect.HasValue)
            {
                DisplayMetrics displayMetrics = Resources.DisplayMetrics;
                var r = new Rectangle
                {
                    Width = rect.Value.Width * displayMetrics.Density,
                    Height = rect.Value.Height * displayMetrics.Density,
                    X = rect.Value.X * displayMetrics.Density,
                    Y = (rect.Value.Y + 25) * displayMetrics.Density
                };

                if (x > r.X && x < r.Width + r.X)
                    if (y > r.Y && y < r.Height + r.Y)
                        return true;
                return false;
            }
            return false;
        }

        private bool _lieAboutCurrentFocus;
        public override bool DispatchTouchEvent(MotionEvent ev)
        {
            var focused = CurrentFocus;
            bool customEntryRendererFocused = focused != null && focused.Parent is EntryRenderer && ClickInsideRect(ev.GetX(), ev.GetY(), InsensitiveArea);

            _lieAboutCurrentFocus = customEntryRendererFocused;
            var result = base.DispatchTouchEvent(ev);
            _lieAboutCurrentFocus = false;

            return result;
        }

        public override Android.Views.View CurrentFocus
        {
            get
            {
                if (_lieAboutCurrentFocus)
                {
                    return null;
                }
                return base.CurrentFocus;
            }
        }
    }

}



