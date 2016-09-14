using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeColorex.Constants.Layouts;
using LykkeColorex.CustomViews;
using LykkeColorex.CustomViews.RegistrationSteps;
using Xamarin.Forms;

namespace LykkeColorex.Pages
{
    public partial class RegistrationPage : ContentPage
    {
        private StickyButton _button;
        private RegistrationProgressBar _registrationBar;
        private AbsoluteLayout al;
        private ClassInterface instance = DependencyService.Get<ClassInterface>();
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

                al = new AbsoluteLayout() { HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill };

                _registrationBar = new RegistrationProgressBar((int)(App.Dimensions.Width - 2 * LoginPageLayout.Padding), 5);

                al.Children.Add(_registrationBar,
                    new Rectangle(LoginPageLayout.Padding, RegistrationPageLayout.RegistrationProgressBarFromTop,
                        AbsoluteLayout.AutoSize, 3));
                _registrationBar.TranslationY = App.Dimensions.Height;

                _button = new StickyButton
                {
                    InputTransparent = false
                };
                 _button.SetState(StickyButtonState.Next, false);
                al.Children.Add(_button, new Rectangle(0, App.Dimensions.Height - 64, App.Dimensions.Width + 1, 64));
                _button.TranslationY = App.Dimensions.Height;

                var entry = new Entry
                {
                    HorizontalOptions = LayoutOptions.Fill,
                    BackgroundColor = Color.Red
                };

                //al.Children.Add(entry, new Rectangle(100,100, 200, 100));

                _currentRegStep = new EmailStep(_button);
                al.Children.Add(_currentRegStep, new Rectangle(23, 127 -25, App.Dimensions.Width - 2*23, AbsoluteLayout.AutoSize));
                _currentRegStep.TranslationY = App.Dimensions.Height;


                _loginLogoColorex = new Image
                {
                    Source = ImageSource.FromFile("logoColorex.png"),
                    Aspect = Aspect.AspectFit
                };
                al.Children.Add(_loginLogoColorex, new Rectangle(logoBounds.X, logoBounds.Y - App.Dimensions.Height, logoBounds.Width, logoBounds.Height));
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
                al.Children.Add(_loginSignUpLabel, new Rectangle(signUpLabelBounds.X, signUpLabelBounds.Y - App.Dimensions.Height, signUpLabelBounds.Width, signUpLabelBounds.Height));
                _loginSignUpLabel.TranslationY = App.Dimensions.Height;

                _loginSignInWLWButton = new ButtonOld
                {
                    TextColor = Color.White,
                    Text = "Sign In with Lykke Wallet",
                    HorizontalOptions = LayoutOptions.Fill,
                    HeightRequest = LoginPageLayout.SignInWLWButtonHeight,
                    FontSize = 17
                };
                al.Children.Add(
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
                al.Children.Add(_loginSignInButton, new Rectangle(signInButtonBounds.X, signInButtonBounds.Y - App.Dimensions.Height, signInButtonBounds.Width, signInButtonBounds.Height));
                _loginSignInButton.TranslationY = App.Dimensions.Height;


                Content = al;

                Content.SizeChanged += ContentOnSizeChanged;


                Device.StartTimer(TimeSpan.FromMilliseconds(300), () =>
                {
                    Debug.WriteLine(Content.Height);
                    return true;
                });
            }
            catch (Exception ex)
            {
                var a = 234;
            }
        }

        private void ContentOnSizeChanged(object sender, EventArgs eventArgs)
        {
            if (Content.Height < 400)
            {
                foreach (var child in al.Children)
                {
                    if (child != _button)
                        child.TranslateTo(0, -40, 100, Easing.CubicOut);
                }
            }
            else
            {

                foreach (var child in al.Children)
                {
                    if (child != _button)
                        child.TranslateTo(0, 0, 100, Easing.CubicOut);
                }
            }
            _button.Layout(new Rectangle(_button.Bounds.X, Content.Height - 64, _button.Bounds.Width, _button.Bounds.Height));
            DependencyService.Get<ClassInterface>().SetRect(_button.Bounds);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DependencyService.Get<ClassInterface>().SetAdjustResize(true);
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
            DependencyService.Get<ClassInterface>().SetRect(null);
            DependencyService.Get<ClassInterface>().SetAdjustResize(false);
        }
    }
}
