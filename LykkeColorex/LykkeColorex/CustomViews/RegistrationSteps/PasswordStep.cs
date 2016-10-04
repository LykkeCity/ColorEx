using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeColorex.CustomViews.RegistrationSteps
{
    public class PasswordStep : RegistrationStep
    {
        private AbsoluteLayout _layout;
        private LabelEx _labelPrimary;
        private LabelEx _labelSecondary;
        private EntryCx _entryPass1;
        private EntryCx _entryPass2;
        private EntryCx _entryHint;
        private IStickyButton _button;


        public PasswordStep(IStickyButton button, IRegistrationContext context) 
            : base(context)
        {
            _button = button;


            _layout = new AbsoluteLayout();

            _labelPrimary = new LabelEx
            {
                TextColor = Color.FromRgb(51, 51, 51),
                Text = "Now create a password",
                FontSize = 27,
                HorizontalOptions = LayoutOptions.Fill,
                //        BackgroundColor = Color.Lime
            };
            _labelSecondary = new LabelEx
            {
                TextColor = Color.FromRgb(51, 51, 51),
                Text = "Password must contain at min 6 characters",
                FontSize = 15,
                HorizontalOptions = LayoutOptions.Fill,
                //        BackgroundColor = Color.Lime
            };


            _entryPass1 = new EntryCx
            {
                HorizontalOptions = LayoutOptions.Fill,
                FontSize = 17,
                TextColor = Color.FromRgb(63, 77, 96),
                PlaceholderUpperSize = 14,
                PlaceholderText = "Password",
                IsPassword = true
            };

            _entryPass2 = new EntryCx
            {
                HorizontalOptions = LayoutOptions.Fill,
                FontSize = 17,
                TextColor = Color.FromRgb(63, 77, 96),
                PlaceholderUpperSize = 14,
                PlaceholderText = "Repeat password",
                IsPassword = true
            };

            _entryHint = new EntryCx
            {
                HorizontalOptions = LayoutOptions.Fill,
                FontSize = 17,
                TextColor = Color.FromRgb(63, 77, 96),
                PlaceholderUpperSize = 14,
                PlaceholderText = "Hint",
                IsPassword = true
            };



            _layout.Children.Add(_labelPrimary, new Rectangle(0, 0, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional);
            _layout.Children.Add(_labelSecondary, new Rectangle(0, 32 + 10, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional);
            _layout.Children.Add(_entryPass1, new Rectangle(0, 32 + 53, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional);
            _layout.Children.Add(_entryPass2, new Rectangle(0, 32 + 53 + 80, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional);
            _layout.Children.Add(_entryHint, new Rectangle(0, 32 + 53 + 80 + 80, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional);
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
                        await Task.Delay(1000);
                        OnStepCompleted();
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
            if (_entryPass1.Text != null && _entryPass1.Text.Length > 5)
            {
                _button.SetState(StickyButtonState.Loading, true);
                await Task.Delay(1500);
                return true;
            }
            else
            {
                _button.SetState(StickyButtonState.Loading, true);
                await Task.Delay(1500);
                return false;
            }
        }

        public override void Initalize()
        {
            _button.Clicked += ButtonOnClicked;
            _button.SetState(StickyButtonState.Next, true);
        }

        public override void Cleanup()
        {
            _button.Clicked -= ButtonOnClicked;
        }

        public override bool IsDismissible => true;
        public override Entry FirstFocusEntry => _entryPass1.Entry;

        public override void ResetState()
        {
            throw new NotImplementedException();
        }

        public override Task Minimize()
        {
            if (!_isMinimized)
            {
                _isMinimized = true;
                return Task.WhenAll(
                    _labelSecondary.TranslateTo(0, -(32 + 53), 150, Easing.CubicOut),
                    _labelSecondary.FadeTo(0, 150, Easing.CubicOut),
                    _labelPrimary.TranslateTo(0, -(32 + 53), 150, Easing.CubicOut),
                    _labelPrimary.FadeTo(0, 150, Easing.CubicOut),
                    _entryPass1.TranslateTo(0, -(32 + 53) - 15, 150, Easing.CubicOut),
                    _entryPass2.TranslateTo(0, -(32 + 53) - 15, 150, Easing.CubicOut),
                    _entryHint.TranslateTo(0, -(32 + 53) - 15, 150, Easing.CubicOut));
            }
            return Task.Run(() => { });
        }

        public override Task Maximize()
        {
            if (_isMinimized)
            {
                _isMinimized = false;
                return Task.WhenAll(
                _labelSecondary.TranslateTo(0, 0, 150, Easing.CubicOut),
                _labelSecondary.FadeTo(1, 150, Easing.CubicOut),
                _labelPrimary.TranslateTo(0, 0, 150, Easing.CubicOut),
                _labelPrimary.FadeTo(1, 150, Easing.CubicOut),
                _entryPass1.TranslateTo(0, 0, 150, Easing.CubicOut),
                _entryPass2.TranslateTo(0, 0, 150, Easing.CubicOut),
                _entryHint.TranslateTo(0, 0, 150, Easing.CubicOut));
            }
            return Task.Run(() => {});
        }


        private bool _isMinimized = false;

        public override event EventHandler StepCompleted;

        protected virtual void OnStepCompleted()
        {
            StepCompleted?.Invoke(this, EventArgs.Empty);
        }
    }
}
