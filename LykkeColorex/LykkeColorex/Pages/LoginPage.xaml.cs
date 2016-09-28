using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeColorex.Constants.Layouts;
using LykkeColorex.CustomPages;
using LykkeColorex.CustomViews;
using Xamarin.Forms;

namespace LykkeColorex.Pages
{
    public partial class LoginPage : ContentPageEx
    {
        private AbsoluteLayout _mainLayout;
        private Image _logoColorex;
        private LabelCx _infoLabel;
        private ButtonCx _singInButton;
        private LabelCx _signInLabel;
        private ButtonOld _sinInWLWButton;
        private LabelCx _signUpLabel;
        private LabelCx _forgotPasswordLabel;
        private BoxView _emailEntryLine;
        private BoxView _passwordEntryLine;
        private LabelEx _emailLabel;
        private LabelEx _passwordLabel;
        private LabelCx _infoLabelWLW;



        private bool _cameBackFromSignInPage = false;
        private bool _cameBackFromSignInWLWPage = false;
        public bool CameBackFromSignInPage
        {
            get { return _cameBackFromSignInPage; }
            set
            {
                _cameBackFromSignInPage = value;
                _cameBackFromSignInWLWPage = !value;
            }
        }

        public bool CameBackFromSignInWLWPage
        {
            get { return _cameBackFromSignInWLWPage; }
            set
            {
                _cameBackFromSignInPage = !value;
                _cameBackFromSignInWLWPage = value;
            }
        }

        private Rectangle _oldSignInButtonBounds;
        private Rectangle _oldSignInWLWButtonBounds;
        private Rectangle _oldSignUpLabelBounds;
        private Rectangle _oldInfoLabelBounds;
        private Rectangle _oldLogoBounds;
        private Rectangle _oldSignInLabelBounds;
        private Rectangle _oldForgotPasswordLabelBounds;
        private Rectangle _oldEmailEntryLineBounds;
        private Rectangle _oldPasswordEntryLineBounds;
        private Rectangle _oldEmailLabelBounds;
        private Rectangle _oldPasswordLabelBounds;
        private Rectangle _oldInfoLabelWLWBounds;

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (CameBackFromSignInPage)
            {
                var animation1 = _singInButton.TranslateTo(0, 0, 400, Easing.CubicOut);

                var animation2 = _sinInWLWButton.TranslateTo(0, 0, 400, Easing.CubicOut);
                var animation3 = _signUpLabel.TranslateTo(0, 0, 400, Easing.CubicOut);

                var animation4 = _infoLabel.TranslateTo(0, 0, 400, Easing.CubicOut);
                var animation5 = _infoLabel.FadeTo(1, 400, Easing.CubicOut);

                var animation6 = _logoColorex.TranslateTo(0, 0, 400, Easing.CubicOut);
                var animation7 = _logoColorex.FadeTo(1, 400, Easing.CubicOut);


                var animation9 = _signInLabel.TranslateTo(0, 0, 400, Easing.CubicOut);
                var animation10 = _signInLabel.FadeTo(0, 300);

                var animation11 = _forgotPasswordLabel.TranslateTo(0, 0, 300, Easing.SinIn);
                var animation12 = _forgotPasswordLabel.FadeTo(0, 300);

                var animation13 = _emailEntryLine.FadeTo(0, 300, Easing.SinIn);


                var animation15 = _passwordEntryLine.FadeTo(0, 300, Easing.SinIn);
                var animation16 = _passwordEntryLine.TranslateTo(0, 0, 300, Easing.SinIn);

                _emailLabel.Opacity = 0;
                _emailLabel.TranslationY = 0;

                _passwordLabel.Opacity = 0;
                _passwordLabel.TranslationY = 0;

                await Task.WhenAll(animation1, animation2, animation3, animation4, animation5, animation6, animation7, animation9, animation10, animation11, animation12,
                    animation13, animation15, animation16);


            }
            if (CameBackFromSignInWLWPage)
            {
                var animation1 = _singInButton.TranslateTo(0, 0, 400, Easing.CubicOut);
                var animation2 = _singInButton.FadeTo(1, 400, Easing.CubicOut);

                var animation3 = _sinInWLWButton.TranslateTo(0, 0, 400, Easing.CubicOut);
                var animation4 = _signUpLabel.TranslateTo(0, 0, 400, Easing.CubicOut);

                var animation5 = _infoLabel.TranslateTo(0, 0, 400, Easing.CubicOut);
                var animation6 = _infoLabel.FadeTo(1, 400, Easing.CubicOut);

                var animation7 = _logoColorex.TranslateTo(0, 0, 400, Easing.CubicOut);
                var animation8 = _logoColorex.FadeTo(1, 400, Easing.CubicOut);


                var animation9 = _signInLabel.TranslateTo(0, 0, 400, Easing.CubicOut);
                var animation10 = _signInLabel.FadeTo(0, 300);

                var animation11 = _forgotPasswordLabel.TranslateTo(0, 0, 300, Easing.SinIn);
                var animation12 = _forgotPasswordLabel.FadeTo(0, 300);

                var animation13 = _emailEntryLine.FadeTo(0, 300, Easing.CubicIn);
                var animation14 = _emailEntryLine.TranslateTo(0, 0, 300, Easing.SinIn);

                var animation15 = _passwordEntryLine.FadeTo(0, 300, Easing.SinIn);
                var animation16 = _passwordEntryLine.TranslateTo(0, 0, 300, Easing.SinIn);

                var animation17 = _infoLabelWLW.FadeTo(0, 200, Easing.CubicIn);
                var animation18 = _infoLabelWLW.TranslateTo(0, 0, 400, Easing.CubicOut);

                _emailLabel.Opacity = 0;
                _emailLabel.TranslationY = 0;

                _passwordLabel.Opacity = 0;
                _passwordLabel.TranslationY = 0;

                await Task.WhenAll(animation1, animation2, animation3, animation4, animation5, animation6, animation7, animation8, animation9, animation10, animation11, animation12,
                    animation13, animation14, animation15, animation16, animation17, animation18);
            }
        }

        public LoginPage()
        {

            NavigationPage.SetHasNavigationBar(this, false);

            BackgroundColor = Color.White;

            _mainLayout = new AbsoluteLayout
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill
            };


            // Seeting up logo positioning

            _logoColorex = new Image
            {
                Source = ImageSource.FromFile("logoColorex.png"),
                Aspect = Aspect.AspectFit
            };
            _mainLayout.Children.Add(_logoColorex,
                new Rectangle(0.5,
                              (App.Dimensions.Height - 300) / 2 - LoginPageLayout.LogoHeight / 2,
                              LoginPageLayout.LogoWidth,
                              LoginPageLayout.LogoHeight),
                AbsoluteLayoutFlags.XProportional);


            //Setting up Signup label positioning

            _signUpLabel = new LabelCx
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
            _signUpLabel.SpanClicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new RegistrationPage(_logoColorex.Bounds, _singInButton.Bounds, _sinInWLWButton.Bounds, _signUpLabel.Bounds, _infoLabel.Bounds), false);
            };
            _mainLayout.Children.Add(
                _signUpLabel,
                new Rectangle(0.5,
                              App.Dimensions.Height - LoginPageLayout.SignUpLabelFromBottom,
                              AbsoluteLayout.AutoSize,
                              AbsoluteLayout.AutoSize),
                AbsoluteLayoutFlags.XProportional);

            // Setting up Sign in WLW Button positioning

            _sinInWLWButton = new ButtonOld
            {
                TextColor = Color.White,
                Text = "Sign In with Lykke Wallet",
                HorizontalOptions = LayoutOptions.Fill,
                HeightRequest = LoginPageLayout.SignInWLWButtonHeight,
                FontSize = 17
            };
            _mainLayout.Children.Add(
                _sinInWLWButton,
                new Rectangle(0.5,
                              App.Dimensions.Height - LoginPageLayout.SignInWLWButtonFromBottom - LoginPageLayout.SignInWLWButtonHeight,
                              App.Dimensions.Width - 2 * LoginPageLayout.Padding,
                              AbsoluteLayout.AutoSize),
                AbsoluteLayoutFlags.XProportional);

            _sinInWLWButton.Clicked += async delegate
            {
                
                var list = new List<Tuple<string, string>>()
                {
                    Tuple.Create("asdf", "Dog"),
                    Tuple.Create("a", "Cat"),
                    Tuple.Create("d", "Mouse"),
                    Tuple.Create("e", "Rat"),
                    Tuple.Create("r", "Hamster"),
                    Tuple.Create("2", "Elephant"),
                    Tuple.Create("344", "Lion"),
                    Tuple.Create("ascv", "Tiger"),
                    Tuple.Create("vv", "Buffalo"),
                };
                var selected = await PopupSelect(true, "Select a pet", list, s => s.Item2, true, list[5]);

                Debug.WriteLine("Selected values were: ");
                foreach (var item in selected)
                {
                    Debug.WriteLine(" - " + item.Item2);
                }

                
                _forgotPasswordLabel.IsVisible = true;
                await SignInWLWLowerAnimation();
                await
                    Navigation.PushAsync(new SignInWLWPage(_sinInWLWButton.GetRealPosition(), _signInLabel.GetRealPosition(),
                        _forgotPasswordLabel.GetRealPosition(), _emailEntryLine.GetRealPosition(), _passwordEntryLine.GetRealPosition(),
                        _infoLabelWLW.GetRealPosition()), false);
            };

            // Sign in button positioning

            _singInButton = new ButtonCx
            {
                TextColor = Color.White,
                Text = "Sign In",
                HorizontalOptions = LayoutOptions.Fill,
                HeightRequest = LoginPageLayout.SignInButtonHeight,
                FontSize = 17
            };
            _mainLayout.Children.Add(
                _singInButton,
                new Rectangle(0.5,
                              App.Dimensions.Height - LoginPageLayout.SignInButtonFromBottom - LoginPageLayout.SignInButtonHeight,
                              App.Dimensions.Width - 2 * LoginPageLayout.Padding,
                              AbsoluteLayout.AutoSize),
                AbsoluteLayoutFlags.XProportional);

            _singInButton.Clicked += async delegate
            {
                _forgotPasswordLabel.IsVisible = true;
                await SignInLowerAnimation();
                await Navigation.PushAsync(new SignInPage(_singInButton.GetRealPosition(), _signInLabel.GetRealPosition(), _forgotPasswordLabel.GetRealPosition(),
                    _emailEntryLine.GetRealPosition(), _passwordEntryLine.GetRealPosition()), false);
            };


            // Info label positioning

            _infoLabel = new LabelCx
            {
                TextColor = Color.FromRgb(140, 148, 160),
                Text = "If you already have a login and password, log in to Colorex",
                FontSize = 15,
                HorizontalTextAlignment = TextAlignment.Center,
                LineSpacing = 12
            };
            _mainLayout.Children.Add(
                _infoLabel,
                new Rectangle(0.5,
                              App.Dimensions.Height - LoginPageLayout.InfoLabelFromBottom - 48,
                              App.Dimensions.Width - 2 * LoginPageLayout.InfoLabelPadding,
                              AbsoluteLayout.AutoSize),
                AbsoluteLayoutFlags.XProportional);


            // Setting up Sign In label for future pages
            _signInLabel = new LabelCx
            {
                FontSize = 27,
                TextColor = Color.FromRgb(51, 51, 51),
                Text = "Sign In",
                Opacity = 0
            };
            _mainLayout.Children.Add(_signInLabel, new Rectangle(SignInPageLayout.Padding - 1, SignInPageLayout.SignInLabelFromTop + 90, 90, 50));

            _forgotPasswordLabel = new LabelCx
            {
                FormattedText = new FormattedString
                {
                    Spans =
                    {
                        new Span
                        {
                            Text = "Forgot your password?",
                            ForegroundColor = Color.Black//Color.FromRgb(63, 142, 253)
                        }
                    }
                },
                ClickableSpanIndex = 0,
                FontSize = 14,
                //BackgroundColor = Color.Red,
                Opacity = 0,
                IsVisible = false
            };
            _mainLayout.Children.Add(_forgotPasswordLabel, new Rectangle(App.Dimensions.Width - SignInPageLayout.Padding - 146, SignInPageLayout.ForgotPasswordFromTop + 100, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            _emailLabel = new LabelEx { Text = "Email", FontSize = 17, TextColor = Color.FromHex("#8C94A0"), Opacity = 0 };
            _mainLayout.Children.Add(_emailLabel, new Rectangle(5 + 22, 35.5 + 185 + 100, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            _emailEntryLine = new BoxView
            {
                BackgroundColor = Color.FromRgb(222, 225, 228),
                Opacity = 0,
                InputTransparent = true
            };
            _mainLayout.Children.Add(_emailEntryLine, new Rectangle(SignInPageLayout.Padding, SignInPageLayout.EmailEntryLineFromTop + 100, App.Dimensions.Width - SignInPageLayout.Padding * 2, 5));

            _passwordLabel = new LabelEx { Text = "Password", FontSize = 17, TextColor = Color.FromHex("#8C94A0"), Opacity = 0 };
            _mainLayout.Children.Add(_passwordLabel, new Rectangle(5 + 22, 35.5 + 260 + 100, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            _passwordEntryLine = new BoxView
            {
                BackgroundColor = Color.FromRgb(222, 225, 228),
                Opacity = 0,
                InputTransparent = true
            };
            _mainLayout.Children.Add(_passwordEntryLine, new Rectangle(SignInPageLayout.Padding, SignInPageLayout.PasswordEntryLineFromTop + 150, App.Dimensions.Width - SignInPageLayout.Padding * 2, 5));

            _infoLabelWLW = new LabelCx
            {
                FormattedText = new FormattedString
                {
                    Spans =
                    {
                        new Span
                        {
                            Text = "If you already have a login and password from ",
                            ForegroundColor = Color.FromRgb(63, 77, 96)
                        },
                        new Span
                        {
                            Text = "Lykke Wallet",
                            ForegroundColor = Color.FromRgb(63, 142, 253)

                        },
                        new Span
                        {
                            Text = ", enter them",
                            ForegroundColor = Color.FromRgb(63, 77, 96)
                        }
                    }
                },
                ClickableSpanIndex = 1,
                FontSize = 15,

                Opacity = 0
            };
            _mainLayout.Children.Add(_infoLabelWLW, new Rectangle(SignInWLWPageLayout.Padding, SignInWLWPageLayout.InfoLabelFromTop, App.Dimensions.Width - 2 * SignInWLWPageLayout.Padding, AbsoluteLayout.AutoSize));

            Content = _mainLayout;
        }

        private Task SignInLowerAnimation()
        {
            _oldSignInButtonBounds = _singInButton.Bounds;
            _oldSignInWLWButtonBounds = _sinInWLWButton.Bounds;
            _oldSignUpLabelBounds = _signUpLabel.Bounds;
            _oldInfoLabelBounds = _infoLabel.Bounds;
            _oldLogoBounds = _logoColorex.Bounds;

            _oldSignInLabelBounds = new Rectangle(SignInPageLayout.Padding - 1, SignInPageLayout.SignInLabelFromTop + 90, 90, 50);
            _oldForgotPasswordLabelBounds = new Rectangle(App.Dimensions.Width - SignInPageLayout.Padding - 146, SignInPageLayout.ForgotPasswordFromTop + 100, _forgotPasswordLabel.Width, 17);
            _oldEmailEntryLineBounds = new Rectangle(SignInPageLayout.Padding, SignInPageLayout.EmailEntryLineFromTop + 100, App.Dimensions.Width - SignInPageLayout.Padding * 2, 0.5);
            _oldPasswordEntryLineBounds = new Rectangle(SignInPageLayout.Padding, SignInPageLayout.PasswordEntryLineFromTop + 150, App.Dimensions.Width - SignInPageLayout.Padding * 2, 0.5);
            _oldEmailLabelBounds = new Rectangle(4 + 22, 35.5 + 2.5 + 185 + 100 - 25, _emailLabel.Width, _emailLabel.Height);
            _oldPasswordLabelBounds = new Rectangle(4 + 22, 35.5 + 7.5 + 260 + 100 - 25, _passwordLabel.Width, _passwordLabel.Height);
            double lower = (App.Dimensions.Height - 24) - (_oldSignInButtonBounds.Height + _oldSignInButtonBounds.Y);

            _signInLabel.SetRealPosition(_oldSignInLabelBounds);
            _forgotPasswordLabel.SetRealPosition(_oldForgotPasswordLabelBounds);
            _emailEntryLine.SetRealPosition(_oldEmailEntryLineBounds);
            _passwordEntryLine.SetRealPosition(_oldPasswordEntryLineBounds);
            _emailLabel.SetRealPosition(_oldEmailLabelBounds);
            _passwordLabel.SetRealPosition(_oldPasswordLabelBounds);
            //_infoLabelWLW.SetRealPosition(_oldInfoLabelWLWBounds);

            var animation1 = _singInButton.TranslateTo(0, lower, 400, Easing.CubicOut);

            var animation2 = _sinInWLWButton.TranslateTo(0, lower + 20, 400, Easing.CubicOut);
            var animation3 = _signUpLabel.TranslateTo(0, lower + 40, 400, Easing.CubicOut);

            var animation4 = _infoLabel.TranslateTo(0, -30, 400, Easing.CubicOut);
            var animation5 = _infoLabel.FadeTo(0, 400, Easing.CubicOut);

            var animation6 = _logoColorex.TranslateTo(0, -60, 400, Easing.CubicOut);
            var animation7 = _logoColorex.FadeTo(0, 400, Easing.CubicOut);

            var animation9 = _signInLabel.TranslateTo(0, -90, 400, Easing.CubicOut);
            var animation10 = _signInLabel.FadeTo(1, 300, Easing.CubicOut);

            var animation11 = _forgotPasswordLabel.TranslateTo(0, -100, 400, Easing.CubicOut);
            var animation12 = _forgotPasswordLabel.FadeTo(1, 300);

            var animation13 = _emailEntryLine.FadeTo(1, 400, Easing.CubicOut);
            var animation14 = _emailEntryLine.TranslateTo(0, -100, 400, Easing.CubicOut);

            var animation15 = _passwordEntryLine.FadeTo(1, 400, Easing.CubicOut);
            var animation16 = _passwordEntryLine.TranslateTo(0, -150, 400, Easing.CubicOut);

            var animation17 = _emailLabel.FadeTo(1, 400, Easing.CubicOut);
            var animation18 = _emailLabel.TranslateTo(0, -100, 400, Easing.CubicOut);

            var animation19 = _passwordLabel.FadeTo(1, 400, Easing.CubicOut);
            var animation20 = _passwordLabel.TranslateTo(0, -100, 400, Easing.CubicOut);

            return Task.WhenAll(animation1, animation2, animation3, animation4, animation5, animation6, animation7, animation9, animation10, animation11, animation12, animation13, animation14,
                animation15, animation16, animation17, animation18, animation19, animation20);
        }

        private Task SignInWLWLowerAnimation()
        {

            _oldSignInButtonBounds = _singInButton.Bounds;
            _oldSignInWLWButtonBounds = _sinInWLWButton.Bounds;
            _oldSignUpLabelBounds = _signUpLabel.Bounds;
            _oldInfoLabelBounds = _infoLabel.Bounds;
            _oldLogoBounds = _logoColorex.Bounds;

            _oldSignInLabelBounds = new Rectangle(SignInWLWPageLayout.Padding - 1, SignInWLWPageLayout.SignInLabelFromTop + 90, 90, 35);
            _oldForgotPasswordLabelBounds = new Rectangle(App.Dimensions.Width - SignInWLWPageLayout.Padding - 146, SignInWLWPageLayout.ForgotPasswordFromTop + 100, _forgotPasswordLabel.Width, 17);
            _oldEmailEntryLineBounds = new Rectangle(SignInWLWPageLayout.Padding, SignInWLWPageLayout.EmailEntryLineFromTop + 100 - 1, App.Dimensions.Width - SignInWLWPageLayout.Padding * 2, 0.5);
            _oldPasswordEntryLineBounds = new Rectangle(SignInWLWPageLayout.Padding, SignInWLWPageLayout.PasswordEntryLineFromTop + 150 - 1, App.Dimensions.Width - SignInWLWPageLayout.Padding * 2, 0.5);
            _oldEmailLabelBounds = new Rectangle(4 + 22, 35.5 + 1.5 + 185 + 100 + 60 - 25, _emailLabel.Width, _emailLabel.Height);
            _oldPasswordLabelBounds = new Rectangle(4 + 22, 35.5 + 6.5 + 260 + 100 + 60 - 25, _passwordLabel.Width, _passwordLabel.Height);
            _oldInfoLabelWLWBounds = new Rectangle(SignInWLWPageLayout.Padding, SignInWLWPageLayout.InfoLabelFromTop + 150, App.Dimensions.Width - 2 * SignInWLWPageLayout.Padding, _infoLabelWLW.Height);

            double lower = (App.Dimensions.Height - 24) - (_oldSignInWLWButtonBounds.Height + _oldSignInWLWButtonBounds.Y);

            _signInLabel.SetRealPosition(_oldSignInLabelBounds);
            _forgotPasswordLabel.SetRealPosition(_oldForgotPasswordLabelBounds);
            _emailEntryLine.SetRealPosition(_oldEmailEntryLineBounds);
            _passwordEntryLine.SetRealPosition(_oldPasswordEntryLineBounds);
            _emailLabel.SetRealPosition(_oldEmailLabelBounds);
            _passwordLabel.SetRealPosition(_oldPasswordLabelBounds);
            _infoLabelWLW.SetRealPosition(_oldInfoLabelWLWBounds);

            var animation1 = _singInButton.TranslateTo(0, -20, 400, Easing.CubicOut);

            var animation111 = _singInButton.FadeTo(0, 400, Easing.CubicOut);

            var animation2 = _sinInWLWButton.TranslateTo(0, lower, 400, Easing.CubicOut);

            var animation3 = _signUpLabel.TranslateTo(0, lower + 30, 400, Easing.CubicOut);

            var animation4 = _infoLabel.TranslateTo(0, -30, 400, Easing.CubicOut);

            var animation5 = _infoLabel.FadeTo(0, 400, Easing.CubicOut);

            var animation6 = _logoColorex.TranslateTo(0, -60, 400, Easing.CubicOut);

            var animation7 = _logoColorex.FadeTo(0, 400, Easing.CubicOut);

            var animation9 = _signInLabel.TranslateTo(0, -90, 400, Easing.CubicOut);

            var animation10 = _signInLabel.FadeTo(1, 300, Easing.CubicOut);

            var animation11 = _forgotPasswordLabel.TranslateTo(0, -100, 400, Easing.CubicOut);
            var animation12 = _forgotPasswordLabel.FadeTo(1, 300);

            var animation13 = _emailEntryLine.FadeTo(1, 400, Easing.CubicOut);
            var animation14 = _emailEntryLine.TranslateTo(0, -100, 400, Easing.CubicOut);

            var animation15 = _passwordEntryLine.FadeTo(1, 400, Easing.CubicOut);
            var animation16 = _passwordEntryLine.TranslateTo(0, -150, 400, Easing.CubicOut);

            var animation17 = _emailLabel.FadeTo(1, 400, Easing.CubicOut);
            var animation18 = _emailLabel.TranslateTo(0, -100, 400, Easing.CubicOut);

            var animation19 = _passwordLabel.FadeTo(1, 400, Easing.CubicOut);
            var animation20 = _passwordLabel.TranslateTo(0, -100, 400, Easing.CubicOut);

            var animation21 = _infoLabelWLW.FadeTo(1, 400, Easing.CubicOut);
            var animation22 = _infoLabelWLW.TranslateTo(0, -150, 400, Easing.CubicOut);

            return Task.WhenAll(animation1, animation111, animation2, animation3, animation4, animation5, animation6, animation7, animation9, animation10, animation11, animation12, animation13, animation14,
                animation15, animation16, animation17, animation18, animation19, animation20, animation21, animation22);
        }

    }
}
