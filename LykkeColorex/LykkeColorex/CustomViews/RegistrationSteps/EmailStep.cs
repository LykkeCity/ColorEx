using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using LykkeColorex.CustomPages;
using Xamarin.Forms;

namespace LykkeColorex.CustomViews.RegistrationSteps
{
    public class EmailStep : RegistrationStep
    {
        private AbsoluteLayout _layout;
        private LabelEx _label;
        private EntryCx _entry;
        private IStickyButton _button;


        public EmailStep(IStickyButton button, IRegistrationContext context)
            : base(context)
        {
            _button = button;
            
            _layout = new AbsoluteLayout();

            _entry = new EntryCx
            {
                HorizontalOptions = LayoutOptions.Fill,
                FontSize = 17,
                TextColor = Color.FromRgb(63, 77, 96),
                PlaceholderUpperSize = 14,
                PlaceholderText = "Email",
            };

            //_entry.Entry.TextChanged += EntryOnTextChanged;

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
            _button.SetState(StickyButtonState.Loading, true);
            await Task.Delay(1500);
            if (_entry.Text != null && _entry.Text.Length > 5)
            {
                return true;
            }
            else
            {
                _entry.Entry.TextChanged += EntryOnTextChanged;
                _entry.SetError();
                return false;
            }
        }

        public override void Initalize()
        {
            _button.Clicked += ButtonOnClicked;
            _button.SetState(StickyButtonState.Next, true);
        }

        private void EntryOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            _entry.Entry.TextChanged -= EntryOnTextChanged;
            ResetState();
        }

        public override void Cleanup()
        {
            _button.Clicked -= ButtonOnClicked;
        }

        public override bool IsDismissible => true;
        public override Entry FirstFocusEntry => _entry.Entry;

        public override void ResetState()
        {
            _entry.SetNormal();
            _button.SetState(StickyButtonState.Next, true);
        }

        public override Task Minimize()
        {
            _isMinimized = true;
            return Task.Run(() => { });
        }

        public override Task Maximize()
        {
            _isMinimized = false;
            return Task.Run(() => { });
        }

        private bool _isMinimized = false;

        public override event EventHandler StepCompleted;

        protected virtual void OnStepCompleted()
        {
            StepCompleted?.Invoke(this, EventArgs.Empty);
        }
    }
}
