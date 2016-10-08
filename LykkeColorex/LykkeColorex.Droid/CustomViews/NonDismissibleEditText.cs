using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace LykkeColorex.Droid.CustomViews
{

    public class NonDismissibleEditText : EditText
    {
        public NonDismissibleEditText(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public NonDismissibleEditText(Context context) : base(context)
        {
        }

        public NonDismissibleEditText(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public NonDismissibleEditText(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public NonDismissibleEditText(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }
        
        public override bool OnKeyPreIme(Keycode keyCode, KeyEvent e)
        {
            if (e.KeyCode == Keycode.Back || e.KeyCode == Keycode.Enter)
            {
                DispatchKeyEvent(e);
                return true;
            }
            return base.OnKeyPreIme(keyCode, e);
        }
    }
}