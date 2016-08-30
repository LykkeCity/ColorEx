using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeColorex.Constants.Layouts;
using LykkeColorex.CustomViews;
using Xamarin.Forms;

namespace LykkeColorex.Pages
{
    public partial class LoginPage : ContentPage
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (CameBackFromSignInPage)
            {
                var animation1 = _singInButton.LayoutTo(_oldSignInButtonBounds, 400, Easing.CubicOut);

                var animation2 = _sinInWLWButton.LayoutTo(_oldSignInWLWButtonBounds, 400, Easing.CubicOut);
                var animation3 = _signUpLabel.LayoutTo(_oldSignUpLabelBounds, 400, Easing.CubicOut);

                var animation4 = _infoLabel.LayoutTo(_oldInfoLabelBounds, 400, Easing.CubicOut);
                var animation5 = _infoLabel.FadeTo(1, 400, Easing.CubicOut);

                var animation6 = _logoColorex.LayoutTo(_oldLogoBounds, 400, Easing.CubicOut);
                var animation7 = _logoColorex.FadeTo(1, 400, Easing.CubicOut);


                var animation9 = _signInLabel.LayoutTo(_oldSignInLabelBounds, 400, Easing.CubicOut);
                var animation10 = _signInLabel.FadeTo(0, 300);

                var animation11 = _forgotPasswordLabel.LayoutTo(_oldForgotPasswordLabelBounds, 300, Easing.SinIn);
                var animation12 = _forgotPasswordLabel.FadeTo(0, 300);
                

                _emailEntryLine.FadeTo(0, 300, Easing.SinIn);
                _emailEntryLine.LayoutTo(_oldEmailEntryLineBounds, 300, Easing.SinIn);

                _passwordEntryLine.FadeTo(0, 300, Easing.SinIn);
                _passwordEntryLine.LayoutTo(_oldPasswordEntryLineBounds, 300, Easing.SinIn);

                _emailLabel.Opacity = 0;
                _emailLabel.Layout(_oldEmailLabelBounds);

                _passwordLabel.Opacity = 0;
                _passwordLabel.Layout(_oldPasswordLabelBounds);
            }
            if (CameBackFromSignInWLWPage)
            {
                //var animation1 = _singInButton.LayoutTo(_oldSignInButtonBounds, 400, Easing.CubicOut);
                var animation55 = _singInButton.TranslateTo(0, 0, 400, Easing.CubicOut);
                var animation111 = _singInButton.FadeTo(1, 400, Easing.CubicOut);

                //var animation2 = _sinInWLWButton.LayoutTo(_oldSignInWLWButtonBounds, 400, Easing.CubicOut);
                var animation2 = _sinInWLWButton.TranslateTo(0, 0, 400, Easing.CubicOut);
                var animation3 = _signUpLabel.LayoutTo(_oldSignUpLabelBounds, 400, Easing.CubicOut);

                var animation4 = _infoLabel.LayoutTo(_oldInfoLabelBounds, 400, Easing.CubicOut);
                var animation5 = _infoLabel.FadeTo(1, 400, Easing.CubicOut);

                var animation6 = _logoColorex.LayoutTo(_oldLogoBounds, 400, Easing.CubicOut);
                var animation7 = _logoColorex.FadeTo(1, 400, Easing.CubicOut);


                var animation9 = _signInLabel.LayoutTo(_oldSignInLabelBounds, 400, Easing.CubicOut);
                var animation10 = _signInLabel.FadeTo(0, 300);

                var animation11 = _forgotPasswordLabel.LayoutTo(_oldForgotPasswordLabelBounds, 300, Easing.SinIn);
                var animation12 = _forgotPasswordLabel.FadeTo(0, 300);


                _emailEntryLine.FadeTo(0, 300, Easing.CubicIn);
                _emailEntryLine.LayoutTo(_oldEmailEntryLineBounds, 300, Easing.SinIn);

                _passwordEntryLine.FadeTo(0, 300, Easing.SinIn);
                _passwordEntryLine.LayoutTo(_oldPasswordEntryLineBounds, 300, Easing.SinIn);

                _infoLabelWLW.FadeTo(0, 200, Easing.CubicIn);
                _infoLabelWLW.LayoutTo(_oldInfoLabelWLWBounds, 400, Easing.CubicOut);

                _emailLabel.Opacity = 0;
                _emailLabel.Layout(_oldEmailLabelBounds);

                _passwordLabel.Opacity = 0;
                _passwordLabel.Layout(_oldPasswordLabelBounds);
            }
        }

        public LoginPage()
        {
            //InitializeComponent();

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
            _sinInWLWButton.Clicked +=  async delegate
            {
                await SignInWLWLowerAnimation();
                await
                    Navigation.PushAsync(new SignInWLWPage(_sinInWLWButton.GetRealPosition(), _signInLabel.Bounds,
                        _forgotPasswordLabel.Bounds, _emailEntryLine.Bounds, _passwordEntryLine.Bounds,
                        _infoLabelWLW.Bounds), false);
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
                await SignInLowerAnimation();
                await Navigation.PushAsync(new SignInPage(_singInButton.GetRealPosition(), _signInLabel.Bounds, _forgotPasswordLabel.Bounds,
                    _emailEntryLine.Bounds, _passwordEntryLine.Bounds), false);
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
            _mainLayout.Children.Add(_signInLabel, new Rectangle(SignInPageLayout.Padding - 1, SignInPageLayout.SignInLabelFromTop + 90, 90, 32));

            _forgotPasswordLabel = new LabelCx
            {
                FormattedText = new FormattedString
                {
                    Spans =
                    {
                        new Span
                        {
                            Text = "Forgot your password?",
                            ForegroundColor = Color.FromRgb(63, 142, 253)
                        }
                    }
                },
                ClickableSpanIndex = 0,
                FontSize = 14,

                Opacity = 0
            };
            _mainLayout.Children.Add(_forgotPasswordLabel, new Rectangle(App.Dimensions.Width - SignInPageLayout.Padding - 146, SignInPageLayout.ForgotPasswordFromTop + 100, AbsoluteLayout.AutoSize, 17));

            _emailLabel = new LabelEx { Text = "Email", FontSize = 17, TextColor = Color.FromHex("#8C94A0"), Opacity = 0 };
            _mainLayout.Children.Add(_emailLabel, new Rectangle(5 + 22, 35.5 + 185 + 100, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            _emailEntryLine = new BoxView
            {
                BackgroundColor = Color.FromRgb(222, 225, 228),
                Opacity = 0
            };
            _mainLayout.Children.Add(_emailEntryLine, new Rectangle(SignInPageLayout.Padding, SignInPageLayout.EmailEntryLineFromTop + 100, App.Dimensions.Width - SignInPageLayout.Padding * 2, 0.5));

            _passwordLabel = new LabelEx { Text = "Password", FontSize = 17, TextColor = Color.FromHex("#8C94A0"), Opacity = 0 };
            _mainLayout.Children.Add(_passwordLabel, new Rectangle(5 + 22, 35.5 + 260 + 100, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            _passwordEntryLine = new BoxView
            {
                BackgroundColor = Color.FromRgb(222, 225, 228),
                Opacity = 0
            };
            _mainLayout.Children.Add(_passwordEntryLine, new Rectangle(SignInPageLayout.Padding, SignInPageLayout.PasswordEntryLineFromTop + 150, App.Dimensions.Width - SignInPageLayout.Padding * 2, 0.5));

            _infoLabelWLW= new LabelCx
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

            _oldSignInLabelBounds = new Rectangle(SignInPageLayout.Padding - 1, SignInPageLayout.SignInLabelFromTop + 90, 90, 32);
            _oldForgotPasswordLabelBounds = new Rectangle(App.Dimensions.Width - SignInPageLayout.Padding - 146, SignInPageLayout.ForgotPasswordFromTop + 100, _forgotPasswordLabel.Width, 17);
            _oldEmailEntryLineBounds = new Rectangle(SignInPageLayout.Padding, SignInPageLayout.EmailEntryLineFromTop + 100, App.Dimensions.Width - SignInPageLayout.Padding * 2, 0.5); ;
            _oldPasswordEntryLineBounds = new Rectangle(SignInPageLayout.Padding, SignInPageLayout.PasswordEntryLineFromTop + 150, App.Dimensions.Width - SignInPageLayout.Padding * 2, 0.5); ;
            _oldEmailLabelBounds = new Rectangle(5 + 22, 35.5 + 185 + 100 , _emailLabel.Width, _emailLabel.Height); ;
            _oldPasswordLabelBounds = new Rectangle(5 + 22, 35.5 + 260 + 100, _passwordLabel.Width, _passwordLabel.Height);
            double lower = (App.Dimensions.Height - 24) - (_oldSignInButtonBounds.Height + _oldSignInButtonBounds.Y);

            Rectangle newSignInButtonBounds = new Rectangle(_oldSignInButtonBounds.X, _oldSignInButtonBounds.Y + lower, _oldSignInButtonBounds.Width, _oldSignInButtonBounds.Height);
            Rectangle newSignInWLWButtonBounds = new Rectangle(_oldSignInWLWButtonBounds.X, _oldSignInWLWButtonBounds.Y + lower + 20, _oldSignInWLWButtonBounds.Width, _oldSignInWLWButtonBounds.Height);
            Rectangle newSignUpLabelBounds = new Rectangle(_oldSignUpLabelBounds.X, _oldSignUpLabelBounds.Y + lower + 40, _oldSignUpLabelBounds.Width, _oldSignUpLabelBounds.Height);
            Rectangle newInfoLabelBounds = new Rectangle(_oldInfoLabelBounds.X, _oldInfoLabelBounds.Y - 30, _oldInfoLabelBounds.Width, _oldInfoLabelBounds.Height);
            Rectangle newLogoBounds = new Rectangle(_oldLogoBounds.X, _oldLogoBounds.Y - 60, _oldLogoBounds.Width, _oldLogoBounds.Height);
            Rectangle newSignInLabelBounds = new Rectangle(_oldSignInLabelBounds.X, SignInPageLayout.SignInLabelFromTop, _oldSignInLabelBounds.Width, _oldSignInLabelBounds.Height);
            Rectangle newForgotPasswordLabelBounds = new Rectangle(_oldForgotPasswordLabelBounds.X, SignInPageLayout.ForgotPasswordFromTop, _oldForgotPasswordLabelBounds.Width, _oldForgotPasswordLabelBounds.Height);
            Rectangle newEmailEntryLineBounds = new Rectangle(_oldEmailEntryLineBounds.X, SignInPageLayout.EmailEntryLineFromTop, _oldEmailEntryLineBounds.Width, _oldEmailEntryLineBounds.Height);
            Rectangle newPasswordEntryLineBounds = new Rectangle(_oldPasswordEntryLineBounds.X, SignInPageLayout.PasswordEntryLineFromTop, _oldPasswordEntryLineBounds.Width, _oldPasswordEntryLineBounds.Height);
            Rectangle newEmailLabelBounds = new Rectangle(_oldEmailLabelBounds.X, _oldEmailLabelBounds.Y - 100, _oldEmailLabelBounds.Width, _oldEmailLabelBounds.Height);
            Rectangle newPasswordLabelBounds = new Rectangle(_oldPasswordLabelBounds.X, _oldPasswordLabelBounds.Y - 100, _oldPasswordLabelBounds.Width, _oldPasswordLabelBounds.Height);

            var animation1 = _singInButton.LayoutTo(newSignInButtonBounds, 400, Easing.CubicOut);

            var animation2 = _sinInWLWButton.LayoutTo(newSignInWLWButtonBounds, 400, Easing.CubicOut);
            var animation3 = _signUpLabel.LayoutTo(newSignUpLabelBounds, 400, Easing.CubicOut);

            var animation4 = _infoLabel.LayoutTo(newInfoLabelBounds, 400, Easing.CubicOut);
            var animation5 = _infoLabel.FadeTo(0, 400, Easing.CubicOut);

            var animation6 = _logoColorex.LayoutTo(newLogoBounds, 400, Easing.CubicOut);
            var animation7 = _logoColorex.FadeTo(0, 400, Easing.CubicOut);


            var animation9 = _signInLabel.LayoutTo(newSignInLabelBounds, 400, Easing.CubicOut);
            var animation10 = _signInLabel.FadeTo(1, 300, Easing.CubicOut);

            var animation11 = _forgotPasswordLabel.LayoutTo(newForgotPasswordLabelBounds, 400, Easing.CubicOut);
            var animation12 = _forgotPasswordLabel.FadeTo(1, 300);

            var animtion13 = _emailEntryLine.FadeTo(1, 400, Easing.CubicOut);
            var animation14 = _emailEntryLine.LayoutTo(newEmailEntryLineBounds, 400, Easing.CubicOut);

            var animation15 = _passwordEntryLine.FadeTo(1, 400, Easing.CubicOut);
            var animation16 = _passwordEntryLine.LayoutTo(newPasswordEntryLineBounds, 400, Easing.CubicOut);

            var animation17 = _emailLabel.FadeTo(1, 400, Easing.CubicOut);
            var animation18 = _emailLabel.LayoutTo(newEmailLabelBounds, 400, Easing.CubicOut);

            var animation19 = _passwordLabel.FadeTo(1, 400, Easing.CubicOut);
            var animation20 = _passwordLabel.LayoutTo(newPasswordLabelBounds, 400, Easing.CubicOut);

            return Task.WhenAll(animation1, animation2, animation3, animation4, animation5, animation6, animation7, animation9, animation10, animation11, animation12);
        }

        private Task SignInWLWLowerAnimation()
        {

            _oldSignInButtonBounds = _singInButton.Bounds;
            _oldSignInWLWButtonBounds = _sinInWLWButton.Bounds;
            _oldSignUpLabelBounds = _signUpLabel.Bounds;
            _oldInfoLabelBounds = _infoLabel.Bounds;
            _oldLogoBounds = _logoColorex.Bounds;

            _oldSignInLabelBounds = new Rectangle(SignInWLWPageLayout.Padding - 1, SignInWLWPageLayout.SignInLabelFromTop + 90, 90, 32); ;
            _oldForgotPasswordLabelBounds = new Rectangle(App.Dimensions.Width - SignInWLWPageLayout.Padding - 146, SignInWLWPageLayout.ForgotPasswordFromTop + 100, _forgotPasswordLabel.Width, 17);
            _oldEmailEntryLineBounds = new Rectangle(SignInWLWPageLayout.Padding, SignInWLWPageLayout.EmailEntryLineFromTop + 100, App.Dimensions.Width - SignInWLWPageLayout.Padding * 2, 0.5);
            _oldPasswordEntryLineBounds = new Rectangle(SignInWLWPageLayout.Padding, SignInWLWPageLayout.PasswordEntryLineFromTop + 150, App.Dimensions.Width - SignInWLWPageLayout.Padding * 2, 0.5);
            _oldEmailLabelBounds = new Rectangle(5 + 22, 35.5 + 185 + 100 + 60, _emailLabel.Width, _emailLabel.Height);
            _oldPasswordLabelBounds = new Rectangle(5 + 22, 35.5 + 260 + 100 + 60, _passwordLabel.Width, _passwordLabel.Height);
            _oldInfoLabelWLWBounds = new Rectangle(SignInWLWPageLayout.Padding, SignInWLWPageLayout.InfoLabelFromTop + 150, App.Dimensions.Width - 2 * SignInWLWPageLayout.Padding, _infoLabelWLW.Height);

            double lower = (App.Dimensions.Height - 24) - (_oldSignInWLWButtonBounds.Height + _oldSignInWLWButtonBounds.Y);

            Rectangle newSignButtonInBounds = new Rectangle(_oldSignInButtonBounds.X, _oldSignInButtonBounds.Y - 20, _oldSignInButtonBounds.Width, _oldSignInButtonBounds.Height);
            Rectangle newSignInWLWButtonBounds = new Rectangle(_oldSignInWLWButtonBounds.X, _oldSignInWLWButtonBounds.Y + lower, _oldSignInWLWButtonBounds.Width, _oldSignInWLWButtonBounds.Height);
            Rectangle newSignUpLabelBounds = new Rectangle(_oldSignUpLabelBounds.X, _oldSignUpLabelBounds.Y + lower + 30, _oldSignUpLabelBounds.Width, _oldSignUpLabelBounds.Height);
            Rectangle newInfoLabelBounds = new Rectangle(_oldInfoLabelBounds.X, _oldInfoLabelBounds.Y - 30, _oldInfoLabelBounds.Width, _oldInfoLabelBounds.Height);
            Rectangle newLogoBounds = new Rectangle(_oldLogoBounds.X, _oldLogoBounds.Y - 60, _oldLogoBounds.Width, _oldLogoBounds.Height);
            Rectangle newSignInLabelBounds = new Rectangle(_oldSignInLabelBounds.X, SignInWLWPageLayout.SignInLabelFromTop, _oldSignInLabelBounds.Width, _oldSignInLabelBounds.Height);
            Rectangle newForgotPasswordLabelBounds = new Rectangle(_oldForgotPasswordLabelBounds.X, SignInWLWPageLayout.ForgotPasswordFromTop, _oldForgotPasswordLabelBounds.Width, _oldForgotPasswordLabelBounds.Height);
            Rectangle newEmailEntryLineBounds = new Rectangle(_oldEmailEntryLineBounds.X, SignInWLWPageLayout.EmailEntryLineFromTop, _oldEmailEntryLineBounds.Width, _oldEmailEntryLineBounds.Height);
            Rectangle newPasswordEntryLineBounds = new Rectangle(_oldPasswordEntryLineBounds.X, SignInWLWPageLayout.PasswordEntryLineFromTop, _oldPasswordEntryLineBounds.Width, _oldPasswordEntryLineBounds.Height);
            Rectangle newEmailLabelBounds = new Rectangle(_oldEmailLabelBounds.X, _oldEmailLabelBounds.Y - 100, _oldEmailLabelBounds.Width, _oldEmailLabelBounds.Height);
            Rectangle newPasswordLabelBounds = new Rectangle(_oldPasswordLabelBounds.X, _oldPasswordLabelBounds.Y - 100, _oldPasswordLabelBounds.Width, _oldPasswordLabelBounds.Height);
            Rectangle newInfoLabelWLWBounds = new Rectangle(_oldInfoLabelWLWBounds.X, SignInWLWPageLayout.InfoLabelFromTop, _oldInfoLabelWLWBounds.Width, _oldInfoLabelWLWBounds.Height);

            _signInLabel.Layout(_oldSignInLabelBounds);
            _forgotPasswordLabel.Layout(_oldForgotPasswordLabelBounds);
            _emailEntryLine.Layout(_oldEmailEntryLineBounds);
            _passwordEntryLine.Layout(_oldPasswordEntryLineBounds);
            _emailLabel.Layout(_oldEmailLabelBounds);
            _passwordLabel.Layout(_oldPasswordLabelBounds);
            _infoLabelWLW.Layout(_oldInfoLabelWLWBounds);
            

            //animation1 = _singInButton.LayoutTo(newSignButtonInBounds, 400, Easing.CubicOut);
            var animation1Translate = _singInButton.TranslateTo(0, newSignButtonInBounds.Y - _oldSignInButtonBounds.Y, 400, Easing.CubicOut);

            var animation111 = _singInButton.FadeTo(0, 400, Easing.CubicOut);

            //var animation2 = _sinInWLWButton.LayoutTo(newSignInWLWButtonBounds, 400, Easing.CubicOut);
            var animation2Translate = _sinInWLWButton.TranslateTo(0, newSignInWLWButtonBounds.Y - _oldSignInWLWButtonBounds.Y, 400, Easing.CubicOut);

            var animation3 = _signUpLabel.LayoutTo(newSignUpLabelBounds, 400, Easing.CubicOut);
            //var animation3Translate = _signUpLabel.TranslateTo(newSignUpLabelBounds.X, newSignUpLabelBounds.Y, 400, Easing.CubicOut);

            var animation4 = _infoLabel.LayoutTo(newInfoLabelBounds, 400, Easing.CubicOut);
            //var animation4Translate = _infoLabel.TranslateTo(newInfoLabelBounds.X, newInfoLabelBounds.Y, 400, Easing.CubicOut);

            var animation5 = _infoLabel.FadeTo(0, 400, Easing.CubicOut);

            var animation6 = _logoColorex.LayoutTo(newLogoBounds, 400, Easing.CubicOut);
           // var animation6Translate = _logoColorex.TranslateTo(newLogoBounds.X, newLogoBounds.Y, 400, Easing.CubicOut);

            var animation7 = _logoColorex.FadeTo(0, 400, Easing.CubicOut);

            var animation9 = _signInLabel.LayoutTo(newSignInLabelBounds, 400, Easing.CubicOut);
            //var animation9Translate = _signInLabel.TranslateTo(newSignInLabelBounds.X, newSignInLabelBounds.Y, 400, Easing.CubicOut);

            var animation10 = _signInLabel.FadeTo(1, 300, Easing.CubicOut);

            var animation11 = _forgotPasswordLabel.LayoutTo(newForgotPasswordLabelBounds, 400, Easing.CubicOut);
            //var animation11Translate = _forgotPasswordLabel.LayoutTo(newForgotPasswordLabelBounds, 400, Easing.CubicOut);

            var animation12 = _forgotPasswordLabel.FadeTo(1, 300);

            var animtion13 = _emailEntryLine.FadeTo(1, 400, Easing.CubicOut);
            var animation14 = _emailEntryLine.LayoutTo(newEmailEntryLineBounds, 400, Easing.CubicOut);

            var animation15 = _passwordEntryLine.FadeTo(1, 400, Easing.CubicOut);
            var animation16 = _passwordEntryLine.LayoutTo(newPasswordEntryLineBounds, 400, Easing.CubicOut);

            var animation17 = _emailLabel.FadeTo(1, 400, Easing.CubicOut);
            var animation18 = _emailLabel.LayoutTo(newEmailLabelBounds, 400, Easing.CubicOut);

            var animation19 = _passwordLabel.FadeTo(1, 400, Easing.CubicOut);
            var animation20 = _passwordLabel.LayoutTo(newPasswordLabelBounds, 400, Easing.CubicOut);

            var animation21 = _infoLabelWLW.FadeTo(1, 400, Easing.CubicOut);
            var animation22 = _infoLabelWLW.LayoutTo(newInfoLabelWLWBounds, 400, Easing.CubicOut);

            return Task.WhenAll( animation11, animation3, animation4, animation5, animation6, animation7);
        }

    }
}
