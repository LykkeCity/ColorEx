using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeColorex.CustomViews.RegistrationSteps
{
    public class EmailStep : RegistrationStep
    {
        private AbsoluteLayout _layout;
        private LabelEx _label;
        private EntryCx _entry;
        private StickyButton _button;


        public EmailStep(StickyButton button)
        {
            _button = button;
            _button.SetState(StickyButtonState.Next, true);

            _button.Clicked += ButtonOnClicked;

            _layout = new AbsoluteLayout();

            _entry = new EntryCx
            {
                HorizontalOptions = LayoutOptions.Fill,
                FontSize = 17,
                TextColor = Color.FromRgb(63, 77, 96),
                PlaceholderUpperSize = 14,
                PlaceholderText = "Email"
            };

            _entry.Entry.TextChanged += EntryOnTextChanged;

            _label = new LabelEx
            {
                TextColor = Color.FromRgb(51, 51, 51),
                Text = "Okay, enter your email",
                FontSize = 27,
                HorizontalOptions = LayoutOptions.Fill,
                //        BackgroundColor = Color.Lime
            };
            _label.AnchorX = 0;

            _layout.Children.Add(_label, new Rectangle(0, 0, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional);
            _layout.Children.Add(_entry, new Rectangle(0, 32 + 21, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional);
            Content = _layout;
        }

        private void EntryOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            if (_button.State == StickyButtonState.Error)
            {
                _button.SetState(StickyButtonState.Next, true);
            }
        }

        private bool _blocked = false;

        private async void ButtonOnClicked(object sender, EventArgs eventArgs)
        {
            if (!_blocked)
            {
                _blocked = true;
                if (_button.State == StickyButtonState.Next)
                {
                    if (await Validate())
                    {
                        _button.SetState(StickyButtonState.Success, true);
                    }
                    else
                    {
                        _button.SetState(StickyButtonState.Error, true);
                    }
                }
                _blocked = false;
            }
        }

        public override async Task<bool> Validate()
        {
            if (_entry.Text != null && _entry.Text.Length > 5)
            {
                _button.SetState(StickyButtonState.Loading, true);
                await Task.Delay(1500);
                return true;
            }
            else
            {
                _button.SetState(StickyButtonState.Loading, true);
                await Task.Delay(1500);
                _entry.SetError();
                return false;
            }
        }
    }
}
