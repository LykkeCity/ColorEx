using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeColorex.CustomViews
{
    public class NonDismissibleEntry : Entry
    {
        public static readonly BindableProperty IsPinProperty = BindableProperty.Create(nameof(IsPin), typeof(bool), typeof(NonDismissibleEntry), false);
        public bool IsPin
        {
            get { return (bool)GetValue(IsPinProperty); }
            set { SetValue(IsPinProperty, value); }
        }
    }
}
