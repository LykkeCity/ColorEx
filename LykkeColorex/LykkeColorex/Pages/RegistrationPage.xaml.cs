using System;
using System.Diagnostics;
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
        private AbsoluteLayout _layout;
        private INativeControls _nativeControls = DependencyService.Get<INativeControls>();
        private LabelCx _loginSignUpLabel;
        private ButtonOld _loginSignInWLWButton;
        private ButtonCx _loginSignInButton;
        private RegistrationStep _currentRegStep;

        private Image _loginLogoColorex;
        
        public RegistrationPage(Rectangle logoBounds, Rectangle signInButtonBounds, Rectangle signInWLWButtonBounds, Rectangle signUpLabelBounds)
        {
            try
            {
                NavigationPage.SetHasNavigationBar(this, false);

                BackgroundColor = Color.White;

                _layout = new AbsoluteLayout() { HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill };

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
                

                _currentRegStep = new EmailStep(_button);
                _layout.Children.Add(_currentRegStep, new Rectangle(23, 127 -25, App.Dimensions.Width - 2*23, AbsoluteLayout.AutoSize));
                _currentRegStep.TranslationY = App.Dimensions.Height;


                _loginLogoColorex = new Image
                {
                    Source = ImageSource.FromFile("logoColorex.png"),
                    Aspect = Aspect.AspectFit
                };
                _layout.Children.Add(_loginLogoColorex, new Rectangle(logoBounds.X, logoBounds.Y - App.Dimensions.Height, logoBounds.Width, logoBounds.Height));
                _loginLogoColorex.TranslationY = App.Dimensions.Height;


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
            if(_currentRegStep.IsDismissible)
                _nativeControls.SetAreaEntrySafe(_button.Bounds);
            else
                _nativeControls.SetAreaEntrySafe(new Rectangle(0,0,Content.Width, Content.Height));
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
