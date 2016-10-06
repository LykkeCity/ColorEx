using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using LykkeColorex.CustomViews;
using LykkeColorex.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(EntryEx), typeof(EntryExRenderer))]
namespace LykkeColorex.Droid.CustomRenderers
{
    public class EntryExRenderer : EntryRenderer
    {
        // Override the OnElementChanged method so we can tweak this renderer post-initial setup
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            try
            {

                base.OnElementChanged(e);
                if (e.OldElement == null)
                {
                    var entr = (EntryEx) e.NewElement;
                    Control.SetBackgroundResource(Resource.Drawable.entry_bg);
                    /*IntPtr IntPtrtextViewClass = JNIEnv.FindClass(typeof (TextView));
                    IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(IntPtrtextViewClass, "mCursorDrawableRes", "I");
                    JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, Resource.Drawable.custom_cursor);*/
                        // replace 0 with a Resource.Drawable.my_cursor*/
                    Typeface font = Typeface.CreateFromAsset(Forms.Context.Assets, $"{entr.FontName}.ttf");
                    Control.Typeface = font;
                }
            }
            catch (Exception ex)
            {
                var a = 234;
            }
        }
    }
}