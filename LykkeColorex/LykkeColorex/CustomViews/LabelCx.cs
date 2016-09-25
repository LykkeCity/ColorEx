using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeColorex.CustomViews
{
    public class LabelCx : Label
    {

        public LabelCx()
        {

        }

        public static readonly BindableProperty FontNameProperty = BindableProperty.Create(nameof(FontName), typeof(string), typeof(ButtonOld), "Lato-Regular");
        public string FontName
        {
            get { return (string)GetValue(FontNameProperty); }
            set { SetValue(FontNameProperty, value); }
        }

        public static readonly BindableProperty ClickableSpanProperty = BindableProperty.Create(nameof(ClickableSpan), typeof(int), typeof(ButtonOld), 0);
        public int ClickableSpan
        {
            get { return (int)GetValue(ClickableSpanProperty); }
            set { SetValue(ClickableSpanProperty, value); }
        }

        public static readonly BindableProperty LineSpacingProperty = BindableProperty.Create(nameof(LineSpacing), typeof(int), typeof(LabelCx), 24);
        public int LineSpacing
        {
            get { return (int)GetValue(LineSpacingProperty); }
            set { SetValue(LineSpacingProperty, value); }
        }

        public static readonly BindableProperty ClickableSpanIndexProperty = BindableProperty.Create(nameof(ClickableSpanIndex), typeof(int), typeof(LabelCx), -1);
        public int ClickableSpanIndex
        {
            get { return (int)GetValue(ClickableSpanIndexProperty); }
            set { SetValue(ClickableSpanIndexProperty, value); }
        }

        public event EventHandler SpanClicked;

        public void InvokeSpanClicked()
        {
            SpanClicked?.Invoke(this, new EventArgs());
        }
        
    }
}
