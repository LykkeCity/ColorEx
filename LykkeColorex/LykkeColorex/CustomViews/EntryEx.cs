using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeColorex.CustomViews
{
    public class EntryEx : Entry
    {
        public static readonly BindableProperty FontNameProperty = BindableProperty.Create(nameof(FontName), typeof(string), typeof(EntryEx), "Lato-Regular");
        public string FontName
        {
            get { return (string)GetValue(FontNameProperty); }
            set { SetValue(FontNameProperty, value); }
        }

    }
}
