using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeColorex.CustomViews.RegistrationSteps
{
    public class NameStep : RegistrationStep
    {
        private AbsoluteLayout _layout;
        private LabelEx _labelPrimary;
        private EntryCx _entryName;
        private EntryCx _entrySurname;
        private StickyButton _button;


        public NameStep(StickyButton button, RegistrationContext context)
            : base(context)
        {
            _button = button;
            _button.SetState(StickyButtonState.Next, true);

            _button.Clicked += ButtonOnClicked;

            _layout = new AbsoluteLayout();

            _labelPrimary = new LabelEx
            {
                TextColor = Color.FromRgb(51, 51, 51),
                Text = "Complete your profile",
                FontSize = 27,
                HorizontalOptions = LayoutOptions.Fill,
                //        BackgroundColor = Color.Lime
            };

            _entryName = new EntryCx
            {
                HorizontalOptions = LayoutOptions.Fill,
                FontSize = 17,
                TextColor = Color.FromRgb(63, 77, 96),
                PlaceholderText = "Name",
                PlaceholderUpperSize = 14,
                IsPassword = true
            };

            _entrySurname = new EntryCx
            {
                HorizontalOptions = LayoutOptions.Fill,
                FontSize = 17,
                TextColor = Color.FromRgb(63, 77, 96),
                PlaceholderUpperSize = 14,
                PlaceholderText = "Surname",
                IsPassword = true
            };


            _layout.Children.Add(_labelPrimary, new Rectangle(0, 0, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional);
            _layout.Children.Add(_entryName, new Rectangle(0, 32 + 21, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional);
            _layout.Children.Add(_entrySurname, new Rectangle(0, 32 + 21 + 80, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional);
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
                        await Task.Delay(300);
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
            if (_entryName.Text != null && _entryName.Text.Length > 5)
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

        public override void Cleanup()
        {
            _button.Clicked -= ButtonOnClicked;
        }

        public override bool IsDismissible => true;
        public override Entry FirstFocusEntry => _entryName.Entry;

        public override void ResetState()
        {
            throw new NotImplementedException();
        }

        public override void Minimize()
        {

        }

        public override void Maximize()
        {

        }

        public override event EventHandler StepCompleted;

        protected virtual void OnStepCompleted()
        {
            StepCompleted?.Invoke(this, EventArgs.Empty);
        }
    }
}
