using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeColorex.Constants.Layouts;
using Xamarin.Forms;

namespace LykkeColorex.CustomViews.Popup
{
    public class PopupOrderSummary : ContentView
    {
        private AbsoluteLayout _layout;
        private LabelEx _labelTitle;
        private ButtonCx _buttonEmail;
        private int _padding = 16;

        public PopupOrderSummary()
        {
            try
            {
                _layout = new AbsoluteLayout();

                _labelTitle = new LabelEx
                {
                    Text = "Order Summary",
                    TextColor = Color.FromRgb(51, 51, 51),
                    FontSize = 20,
                    FontName = "Karla-Bold"
                    //BackgroundColor = Color.Red
                };


                _buttonEmail = new ButtonCx
                {
                    TextColor = Color.White,
                    Text = "Email Me",
                    HorizontalOptions = LayoutOptions.Fill,
                    FontSize = 17
                };


                _layout.Children.Add(new BoxView {Color = Color.White}, new Rectangle(1, 1, 1, 1),
                    AbsoluteLayoutFlags.All);

                _layout.Children.Add(_labelTitle,
                    new Rectangle(0.5, 15, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize),
                    AbsoluteLayoutFlags.XProportional);

                _layout.Children.Add(_buttonEmail,
                    new Rectangle(_padding + (App.Dimensions.Width - _padding - 8)/3 + 8, 0 - _padding - 60,
                        2*(App.Dimensions.Width - _padding - 8)/3 - _padding, 60));
                
                Content = _layout;

                Content.SizeChanged += delegate
                {
                    _buttonEmail.Layout(new Rectangle(_buttonEmail.X, 2 * App.Dimensions.Height / 3 - _padding - 60, _buttonEmail.Width,
                        _buttonEmail.Height));
                };

            }
            catch (Exception e)
            {
                var a = 234;
            }
        }
    }
}
