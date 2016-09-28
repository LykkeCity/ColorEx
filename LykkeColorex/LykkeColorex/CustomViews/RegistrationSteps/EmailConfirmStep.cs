using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeColorex.CustomViews.RegistrationSteps
{
    class EmailConfirmStep : RegistrationStep
    {
        private AbsoluteLayout _layout;
        private LabelEx _labelPrimary;
        private LabelEx _labelSecondary;
        private LabelCx _labelCodeRecovery;
        private EntryCxPin _entry;
        private StickyButton _button;

        
        
        public EmailConfirmStep(StickyButton button, RegistrationContext context)
            : base(context)
        {
            try
            {
                _button = button;
                _button.Hide();

                _layout = new AbsoluteLayout();

                _labelPrimary = new LabelEx
                {
                    TextColor = Color.FromRgb(51, 51, 51),
                    Text = "Enter code",
                    FontSize = 27,
                    HorizontalOptions = LayoutOptions.Fill,
                    //        BackgroundColor = Color.Lime
                };

                _labelSecondary = new LabelEx
                {
                    TextColor = Color.FromRgb(51, 51, 51),
                    Text = $"Code was sent to email {Context.Email}",
                    FontSize = 15,
                    HorizontalOptions = LayoutOptions.Fill,
                    //        BackgroundColor = Color.Lime
                };

                _labelCodeRecovery = new LabelCx
                {
                    Text = "Haven't received the code?",
                    ClickableSpanIndex = 0,
                    TextColor = Color.FromRgb(63, 142, 253),
                    FontSize = 14,
                    HorizontalTextAlignment = TextAlignment.End
                };

                _entry = new EntryCxPin(4)
                {
                    HorizontalOptions = LayoutOptions.Fill,
                    FontSize = 17,
                    TextColor = Color.FromRgb(63, 77, 96),
                    IsPassword = true
                };
                //_entry.Entry.TextChanged += EntryOnTextChanged;
                _entry.Completed += EntryOnCompleted;

                _layout.Children.Add(_labelPrimary, new Rectangle(0, 0, 1, AbsoluteLayout.AutoSize),
                    AbsoluteLayoutFlags.WidthProportional);
                _layout.Children.Add(_labelSecondary, new Rectangle(0, 32 + 10, 1, AbsoluteLayout.AutoSize),
                    AbsoluteLayoutFlags.WidthProportional);
                _layout.Children.Add(_entry, new Rectangle(0, 32 + 53, 1, AbsoluteLayout.AutoSize),
                    AbsoluteLayoutFlags.WidthProportional);
                _layout.Children.Add(_labelCodeRecovery, new Rectangle(0, 32 + 53 + 80 + 22, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional);


                Content = _layout;
            }
            catch (Exception e)
            {
                var a = 234;
            }
        }

        private void EntryOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            var al = Parent as Layout;
            _button.Layout(new Rectangle(_button.Bounds.X, al.Height - 64, _button.Bounds.Width, _button.Bounds.Height)); // dirty, dirty, dirty hack
        }

        private bool _blocked = false;
        private async void EntryOnCompleted(object sender, EventArgs eventArgs)
        {
            if (!_blocked)
            {
                _blocked = true;
                _entry.IsEditable = false;
                _button.SetState(StickyButtonState.Loading, false);
                await _button.Show();

                if (await Validate())
                {
                    _button.SetState(StickyButtonState.Success, true);
                    Cleanup();
                    await Task.Delay(300);
                    //OnStepCompleted();
                }
                else
                {
                    _button.SetState(StickyButtonState.Error, true);
                    _entry.Entry.TextChanged += ResetButton;
                }
                _blocked = false;
                _entry.IsEditable = true;
            }
        }



        private async void ResetButton(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            _entry.Entry.TextChanged -= ResetButton;
            ResetState();
        }

        public override async Task<bool> Validate()
        {
            if (int.Parse(_entry.Text) == 1234)
            {
                await Task.Delay(1500);
                return true;
            }
            else
            {
                await Task.Delay(1500);
                return false;
            }
        }

        public override void Cleanup()
        {
            //_entry.Entry.TextChanged -= EntryOnTextChanged;
            _entry.Completed -= EntryOnCompleted;
        }

        public override bool IsDismissible => false;

        public override Entry FirstFocusEntry => _entry.Entry;

        public override async void ResetState()
        {
            await _button.Hide();
            _button.SetState(StickyButtonState.Next, false);
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
