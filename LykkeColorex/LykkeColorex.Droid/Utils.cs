using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace LykkeColorex.Droid
{
    public static class Utils
    {
        public static float ConvertPtToPixels(float pt, Context context)
        {
            Resources resources = context.Resources;
            var metrics = resources.DisplayMetrics;
            float px = pt * ((float)metrics.DensityDpi / (float)Android.Util.DisplayMetricsDensity.Default);
            return px;
        }
    }
}