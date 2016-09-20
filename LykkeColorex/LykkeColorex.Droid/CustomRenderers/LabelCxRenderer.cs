using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Text.Method;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.Util;
using LykkeColorex.CustomViews;
using LykkeColorex.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;
using ListView = Xamarin.Forms.ListView;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(LabelCx), typeof(LabelCxRenderer))]
namespace LykkeColorex.Droid.CustomRenderers
{

    public class WordSpan : ClickableSpan
    {
        public Action<View> Click;

        private int id;
        private TextPaint textpaint;

        public WordSpan()
        {

        }

        public override void UpdateDrawState(TextPaint ds)
        {
            textpaint = ds;
            textpaint.SetARGB(255, 63, 142, 253);
            ds.UnderlineText = false;

        }
        public override void OnClick(View widget)
        {
            if (Click != null)
                Click(widget);
        }
    }


    public class MyClickableSpan : ClickableSpan
    {
        public Action<View> Click;

        public override void OnClick(View widget)
        {
            if (Click != null)
                Click(widget);
        }
    }

    class LabelCxRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Label> e)
        {

            base.OnElementChanged(e);

            if (Control != null)
            {
                var label = (LabelCx)e.NewElement;
                Control.SetLineSpacing(0, 1.5f);
                Init();
                Control.MovementMethod = new LinkMovementMethod();

            }


        }

        void Init()
        {
            if (Control != null)
            {
                var label = (LabelCx)Element;
                if (label.FontName != null)
                {
                    if (label.FormattedText != null)
                    {
                        SpannableStringBuilder ssb = new SpannableStringBuilder();

                        for (int i = 0; i < label.FormattedText.Spans.Count; i++)
                        {
                            var span = label.FormattedText.Spans[i];
                            ssb.Append(span.Text);
                            if (label.ClickableSpanIndex == i)
                            {
                                var cs = new WordSpan();
                                cs.Click += delegate (View view)
                                {
                                    label.InvokeSpanClicked();
                                };
                                ssb.SetSpan(cs, ssb.Length() - span.Text.Length, ssb.Length(), SpanTypes.ExclusiveExclusive);
                            }
                            else
                            {
                                ForegroundColorSpan foregroundColorSpan = new ForegroundColorSpan(span.ForegroundColor.ToAndroid());
                                ssb.SetSpan(foregroundColorSpan, ssb.Length() - span.Text.Length, ssb.Length(), SpanTypes.ExclusiveExclusive);
                            }
                        }

                        Typeface font = Typeface.CreateFromAsset(Forms.Context.Assets, $"{label.FontName}.ttf");
                        Control.Typeface = font;
                        Control.TextSize = (float)label.FontSize;
                        Control.SetText(ssb, TextView.BufferType.Normal);
                    }
                    else
                    {
                        Typeface font = Typeface.CreateFromAsset(Forms.Context.Assets, $"{label.FontName}.ttf");
                        Control.Typeface = font;
                        Control.TextSize = (float)label.FontSize;
                    }
                }


            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var label = (LabelCx)sender;
            if (e.PropertyName == "FontName")
            {
                Typeface font = Typeface.CreateFromAsset(Forms.Context.Assets, $"{label.FontName}.ttf");
                Control.Typeface = font;
            }
            if (e.PropertyName == "InputTransparent")
            {
                
            }
        }
    }
}