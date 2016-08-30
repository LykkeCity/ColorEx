using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LykkeColorex.CustomViews;
using LykkeColorex.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(ButtonOld), typeof(ButtonOldRenderer))]
namespace LykkeColorex.Droid.CustomRenderers
{
    class ButtonOldRenderer : ButtonRenderer
    {
        private GradientDrawable _normal, _pressed, _disabled;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            try
            {
                base.OnElementChanged(e);

                if (Control != null)
                {
                    var button = (ButtonOld)e.NewElement;

                    Control.Gravity = GravityFlags.Center;

                    //create drawable for normal state 
                    //_normal = new GradientDrawable(GradientDrawable.Orientation.LeftRight, new int[] { Color.ParseColor("#255779"), Color.ParseColor("#3e7492"), Color.ParseColor("#a6c0cd") });
                    _normal = new GradientDrawable(GradientDrawable.Orientation.LeftRight, new int[] { Color.ParseColor("#E19100"), Color.ParseColor("#E10029"), Color.ParseColor("#AB00E1") });
                    _normal.SetCornerRadius(Utils.ConvertPtToPixels(button.Radius, Context));

                    // create drawable for pressed state
                    _pressed = new GradientDrawable(GradientDrawable.Orientation.LeftRight, new int[] { Color.ParseColor("#AB00E1"), Color.ParseColor("#E10029"), Color.ParseColor("#E19100") });
                    _pressed.SetCornerRadius(Utils.ConvertPtToPixels(button.Radius, Context));

                    //create drawable for disabled state
                    _disabled = new GradientDrawable();
                    _disabled.SetCornerRadius(Utils.ConvertPtToPixels(button.Radius, Context));

                    Typeface font = Typeface.CreateFromAsset(Forms.Context.Assets, $"{button.FontName}.ttf");
                    Control.Typeface = font;

                    // Add the drawables to a state list and assign the state list to the button
                    var sld = new StateListDrawable();
                    sld.AddState(new int[] { Android.Resource.Attribute.StatePressed }, _pressed);
                    sld.AddState(new int[] { Android.Resource.Attribute.StateEnabled }, _normal);
                    sld.AddState(new int[] { }, _disabled);
                    if((int)Android.OS.Build.VERSION.SdkInt >= 21)
                        Control.StateListAnimator = null;
                    Control.TransformationMethod = null;
                    Control.SetBackground(sld);
                }
            }
            catch (Exception ex)
            {
                var a = 234;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var button = (ButtonOld)sender;

            if (_normal != null && _pressed != null)
            {
                if (e.PropertyName == "Radius")
                {
                    _normal.SetCornerRadius(Utils.ConvertPtToPixels(button.Radius, Context));
                    _disabled.SetCornerRadius(Utils.ConvertPtToPixels(button.Radius, Context));
                    _pressed.SetCornerRadius(Utils.ConvertPtToPixels(button.Radius, Context));
                }
                else if (e.PropertyName == "Color" || _normal != null)
                {
                    //_normal.SetColor(Color.ParseColor(button.Color));
                }
                else if (e.PropertyName == "ColorDisabled" || _disabled != null)
                {
                    //_disabled.SetColor(Color.ParseColor(button.ColorDisabled));
                }
                else if (e.PropertyName == "ColorPressed" || _pressed != null)
                {
                   // _pressed.SetColor(Color.ParseColor(button.Color));
                }
                else if (e.PropertyName == "FontName")
                {
                    Typeface font = Typeface.CreateFromAsset(Forms.Context.Assets, $"{button.FontName}.ttf");
                    Control.Typeface = font;
                }

            }
        }
    }
}