using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using LykkeColorex.CustomViews;
using Xamarin.Forms;

namespace LykkeColorex.Pages
{
    public class Page1 : ContentPage
    {
        private AbsoluteLayout _layout;
        private NonDismissibleEntry _entry;
        private Button _button;
        public Page1()
        {
            _layout = new AbsoluteLayout();

            _entry = new NonDismissibleEntry { IsPin = true };

            _button = new Button { Text = "Focus!" };
            _button.Clicked += delegate
            {
                _entry.Focus();
            };

            _layout.Children.Add(_entry, new Rectangle(50, 50, 150, 50));
            _layout.Children.Add(_button, new Rectangle(50, 200, 150, 50));

            Content = _layout;
        }
    }
}
