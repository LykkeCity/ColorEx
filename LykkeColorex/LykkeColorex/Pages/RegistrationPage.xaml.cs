using System;
using System.Diagnostics;
using System.Threading.Tasks;
using LykkeColorex.Constants.Layouts;
using LykkeColorex.CustomPages;
using LykkeColorex.CustomViews;
using LykkeColorex.CustomViews.RegistrationSteps;
using Xamarin.Forms;

namespace LykkeColorex.Pages
{
    public partial class RegistrationPage : ContentPageEx
    {
        private StickyButton _button;
        private RegistrationProgressBar _registrationBar;
        private RegistrationContext _registrationContext;
        private AbsoluteLayout _layout;
        private INativeControls _nativeControls = DependencyService.Get<INativeControls>();
        private LabelCx _loginSignUpLabel;
        private LabelCx _infoLabel;
        private ButtonOld _loginSignInWLWButton;
        private ButtonCx _loginSignInButton;
        private RegistrationStep _currentRegStep;
        private LabelCx _notNowLabel;
        private BackArrowCx _backArrow;

        private Image _loginLogoColorex;
        protected override bool OnBackButtonPressed()
        {
            Task.Run(async () =>
            {
                await AnimateBack();
                await Navigation.PopAsync(false);
            });
            return true;
        }

        private Task AnimateBack()
        {
            var a1 = _loginSignUpLabel.TranslateTo(0, App.Dimensions.Height, 500, Easing.CubicOut);
            var a2 = _loginSignInButton.TranslateTo(0, App.Dimensions.Height, 500, Easing.CubicOut);
            var a3 = _loginSignInWLWButton.TranslateTo(0, App.Dimensions.Height, 500, Easing.CubicOut);
            var a4 = _loginLogoColorex.TranslateTo(0, App.Dimensions.Height, 500, Easing.CubicOut);
            var a5 = _backArrow.TranslateTo(0, App.Dimensions.Height, 500, Easing.CubicOut);
            var a6 = _notNowLabel.TranslateTo(0, App.Dimensions.Height, 500, Easing.CubicOut);

            var a7 = _button.TranslateTo(0, App.Dimensions.Height, 500, Easing.CubicOut);

            var a8 = _currentRegStep.TranslateTo(0, App.Dimensions.Height, 500, Easing.CubicOut);
            var a9 = _registrationBar.TranslateTo(0, App.Dimensions.Height, 500, Easing.CubicOut);

            var a10 = _infoLabel.TranslateTo(0, App.Dimensions.Height, 500, Easing.CubicOut);

            return Task.WhenAll(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10);

        }

        public RegistrationPage(Rectangle logoBounds, Rectangle signInButtonBounds, Rectangle signInWLWButtonBounds, Rectangle signUpLabelBounds, Rectangle infoLabelBounds)
        {
            try
            {
                NavigationPage.SetHasNavigationBar(this, false);

                BackgroundColor = Color.White;

                _registrationContext = new RegistrationContext();

                _layout = new AbsoluteLayout() { HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill };


                _backArrow = new BackArrowCx(false);
                _backArrow.Clicked += async delegate
                {
                    await AnimateBack();
                    await Navigation.PopAsync(false);
                };

                _layout.Children.Add(_backArrow, new Rectangle(24, Device.OnPlatform(Android: 43 + 5 - 25, iOS: 43, WinPhone: 43), 28, 35));

                _backArrow.TranslationY = App.Dimensions.Height;

                _notNowLabel = new LabelCx
                {
                    Text = "Not now",
                    TextColor = Color.FromRgb(140, 148, 160),
                    FontSize = 16

                };

                _layout.Children.Add(_notNowLabel, new Rectangle(App.Dimensions.Width - 24 - 62, 32 - 25 + 5 + 9, AbsoluteLayout.AutoSize, 35));

                _notNowLabel.TranslationY = App.Dimensions.Height;

                _registrationBar = new RegistrationProgressBar((int)(App.Dimensions.Width - 2 * LoginPageLayout.Padding), Registration.Steps.Length, 0);

                _layout.Children.Add(_registrationBar,
                    new Rectangle(LoginPageLayout.Padding, RegistrationPageLayout.RegistrationProgressBarFromTop,
                        AbsoluteLayout.AutoSize, 3));
                _registrationBar.TranslationY = App.Dimensions.Height;

                _button = new StickyButton
                {
                    InputTransparent = false
                };
                _button.SetState(StickyButtonState.Next, false);
                _button.TranslationY = App.Dimensions.Height;


                _currentRegStep = new EmailStep(_button, _registrationContext);
                _layout.Children.Add(_currentRegStep, new Rectangle(23, 127 - 25, App.Dimensions.Width - 2 * 23, AbsoluteLayout.AutoSize));
                _currentRegStep.TranslationY = App.Dimensions.Height;


                _loginLogoColorex = new Image
                {
                    Source = ImageSource.FromFile("logoColorex.png"),
                    Aspect = Aspect.AspectFit
                };
                _layout.Children.Add(_loginLogoColorex, new Rectangle(logoBounds.X, logoBounds.Y - App.Dimensions.Height, logoBounds.Width, logoBounds.Height));
                _loginLogoColorex.TranslationY = App.Dimensions.Height;

                _infoLabel = new LabelCx
                {
                    TextColor = Color.FromRgb(140, 148, 160),
                    Text = "If you already have a login and password, log in to Colorex",
                    FontSize = 15,
                    HorizontalTextAlignment = TextAlignment.Center,
                    LineSpacing = 12
                };
                _layout.Children.Add(_infoLabel, new Rectangle(infoLabelBounds.X, infoLabelBounds.Y - App.Dimensions.Height, infoLabelBounds.Width, infoLabelBounds.Height));
                _infoLabel.TranslationY = App.Dimensions.Height;



                _loginSignUpLabel = new LabelCx
                {
                    FormattedText = new FormattedString
                    {
                        Spans = {
                        new Span {
                            Text = "Don't have an account?  ",
                            ForegroundColor = Color.FromRgb(63, 77, 96)
                        },
                        new Span { Text = "Sign Up",
                            ForegroundColor = Color.FromRgb(63, 142, 253)
                        }
                    }
                    },
                    FontSize = 16,
                    ClickableSpanIndex = 1
                };
                _layout.Children.Add(_loginSignUpLabel, new Rectangle(signUpLabelBounds.X, signUpLabelBounds.Y - App.Dimensions.Height, signUpLabelBounds.Width, signUpLabelBounds.Height));
                _loginSignUpLabel.TranslationY = App.Dimensions.Height;

                _loginSignInWLWButton = new ButtonOld
                {
                    TextColor = Color.White,
                    Text = "Sign In with Lykke Wallet",
                    HorizontalOptions = LayoutOptions.Fill,
                    HeightRequest = LoginPageLayout.SignInWLWButtonHeight,
                    FontSize = 17
                };
                _layout.Children.Add(
                    _loginSignInWLWButton, new Rectangle(signInWLWButtonBounds.X, signInWLWButtonBounds.Y - App.Dimensions.Height, signInWLWButtonBounds.Width, signInWLWButtonBounds.Height));
                _loginSignInWLWButton.TranslationY = App.Dimensions.Height;
                // Sign in button positioning

                _loginSignInButton = new ButtonCx
                {
                    TextColor = Color.White,
                    Text = "Sign In",
                    HorizontalOptions = LayoutOptions.Fill,
                    HeightRequest = LoginPageLayout.SignInButtonHeight,
                    FontSize = 17
                };
                _layout.Children.Add(_loginSignInButton, new Rectangle(signInButtonBounds.X, signInButtonBounds.Y - App.Dimensions.Height, signInButtonBounds.Width, signInButtonBounds.Height));
                _loginSignInButton.TranslationY = App.Dimensions.Height;



                _layout.Children.Add(_button, new Rectangle(0, App.Dimensions.Height - 64, App.Dimensions.Width + 1, 64));

                Content = _layout;

                Content.SizeChanged += ContentOnSizeChanged;


                Device.StartTimer(TimeSpan.FromMilliseconds(300), () =>
                {
                    //Debug.WriteLine(_button.Bounds.Y + " " + _button.TranslationY);
                    //Debug.WriteLine(Content.Height);
                    return true;
                });

                this.MeasureInvalidated += (sender, args) =>
                {
                    Debug.WriteLine("Invalidated!!");

                };

            }
            catch (Exception ex)
            {
                var a = 234;
            }
        }

        private void ContentOnSizeChanged(object sender, EventArgs eventArgs)
        {
            if (Content.Height < 380) // 380
            {
                foreach (var child in _layout.Children)
                {
                    if (child != _button)
                        child.TranslateTo(0, -40, 100, Easing.CubicOut);
                }
                _currentRegStep.Minimize();
            }
            else
            {

                foreach (var child in _layout.Children)
                {
                    if (child != _button)
                        child.TranslateTo(0, 0, 100, Easing.CubicOut);
                }
                _currentRegStep.Maximize();
            }
            _button.Layout(new Rectangle(_button.Bounds.X, Content.Height - 64, _button.Bounds.Width, _button.Bounds.Height));
            if (_currentRegStep.IsDismissible)
                _nativeControls.SetAreaEntrySafe(_button.Bounds);
            else
                _nativeControls.SetAreaEntrySafe(new Rectangle(0, 0, Content.Width, Content.Height));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _nativeControls.SetAdjustResize(true);
            try
            {
                _loginSignUpLabel.TranslateTo(0, 0, 500, Easing.CubicOut);
                _loginSignInButton.TranslateTo(0, 0, 500, Easing.CubicOut);
                _loginSignInWLWButton.TranslateTo(0, 0, 500, Easing.CubicOut);
                _loginLogoColorex.TranslateTo(0, 0, 500, Easing.CubicOut);
                _infoLabel.TranslateTo(0, 0, 500, Easing.CubicOut);

                _backArrow.TranslateTo(0, 0, 500, Easing.CubicOut);
                _notNowLabel.TranslateTo(0, 0, 500, Easing.CubicOut);
                _button.TranslateTo(0, 0, 500, Easing.CubicOut);
                _currentRegStep.TranslateTo(0, 0, 500, Easing.CubicOut);
                _registrationBar.TranslateTo(0, 0, 500, Easing.CubicOut);


            }
            catch (Exception ex)
            {
                var a = 234;
            }

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _nativeControls.SetAreaEntrySafe(null);
            _nativeControls.SetAdjustResize(false);
        }
    }
}
