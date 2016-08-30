using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeColorex.CustomViews
{
    public class LabelEx : Label
    {
        public static readonly BindableProperty FontNameProperty = BindableProperty.Create("FontName", typeof(string), typeof(LabelEx), "Karla-Regular");
        public string FontName
        {
            get { return (string)GetValue(FontNameProperty); }
            set { SetValue(FontNameProperty, value); }
        }
    }

}
