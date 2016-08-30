using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text.Method;
using Android.Views;
using Android.Widget;
using LykkeColorex.CustomViews;
using LykkeColorex.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(LabelEx), typeof(LabelExRenderer))]
namespace LykkeColorex.Droid.CustomRenderers
{
    class LabelExRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Label> e)
        {

            base.OnElementChanged(e);

            if (Control != null)
            {
                var label = (LabelEx) e.NewElement;
                Typeface font = Typeface.CreateFromAsset(Forms.Context.Assets, $"{label.FontName}.ttf");
                Control.Typeface = font;
                //Control.SetLineSpacing(0, 1.5f);

            }
        }
    }
}