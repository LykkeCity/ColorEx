using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeColorex.CustomViews
{
    public class ButtonCx : Button
    {
        public static readonly BindableProperty FontNameProperty = BindableProperty.Create("FontName", typeof(string), typeof(ButtonCx), "Karla-Bold");
        public string FontName
        {
            get { return (string)GetValue(FontNameProperty); }
            set { SetValue(FontNameProperty, value); }
        }

        public static readonly BindableProperty ColorProperty = BindableProperty.Create("Color", typeof(string), typeof(ButtonCx), "#3F8EFD");
        public string Color
        {
            get { return (string)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly BindableProperty ColorPressedProperty = BindableProperty.Create("ColorPressed", typeof(string), typeof(ButtonCx), "#408CF2");
        public string ColorPressed
        {
            get { return (string)GetValue(ColorPressedProperty); }
            set { SetValue(ColorPressedProperty, value); }
        }

        public static readonly BindableProperty ColorDisabledProperty = BindableProperty.Create("ColorDisabled", typeof(string), typeof(ButtonCx), "#FF0000");
        public string ColorDisabled
        {
            get { return (string)GetValue(ColorDisabledProperty); }
            set { SetValue(ColorDisabledProperty, value); }
        }

        public static readonly BindableProperty RadiusProperty = BindableProperty.Create("Radius", typeof(int), typeof(ButtonCx), 10);
        public int Radius
        {
            get { return (int)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }
    }
}
