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
using LykkeColorex.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.Dependency(typeof(Class1))]
namespace LykkeColorex.Droid
{
    public class Class1 : INativeControls
    {
        public void SetAreaEntrySafe(Rectangle? rect)
        {
            MainActivityInstance.Instance.InsensitiveArea = rect;
        }

        public void SetRects(List<Rectangle> list)
        {
            MainActivityInstance.Instance.Areas = list;
        }

        public void SetAdjustResize(bool v)
        {
            MainActivityInstance.Instance.Window.SetSoftInputMode(v ? SoftInput.AdjustResize : SoftInput.AdjustPan);
        }
    }
} 