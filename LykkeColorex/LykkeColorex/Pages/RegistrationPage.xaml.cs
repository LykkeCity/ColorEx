using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LykkeColorex.Constants.Layouts;
using LykkeColorex.CustomPages;
using LykkeColorex.CustomViews;
using LykkeColorex.CustomViews.RegistrationSteps;
using Xamarin.Forms;

namespace LykkeColorex.Pages
{
    public partial class RegistrationPage : ContentPageEx, IRegistrationHostPage
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
        private List<RegistrationStep> _steps;
        private RegistrationStep _currentRegStep;
        private LabelCx _notNowLabel;
        private BackArrowCx _backArrow;
        private RegistrationManager _registrationManager;

        private Image _loginLogoColorex;
        protected override bool OnBackButtonPressed()
        {
            Task.Run(async () => await AnimateBack())
                .ContinueWith(task => Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync(false)));
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

                _registrationBar = new RegistrationProgressBar((int)(App.Dimensions.Width - 2 * LoginPageLayout.Padding), 4, 0);

                _registrationBar.Setup(5, 0);

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

                _registrationManager = new RegistrationManager(typeof(EmailConfirmStep), this, _button, _registrationBar, _registrationContext);

                foreach (var step in _steps)
                {
                    step.TranslationY = App.Dimensions.Height;
                }

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



                _layout.Children.Add(_button, new Rectangle(0, 0, App.Dimensions.Width + 1, 64));

                Content = _layout;

                Content.SizeChanged += ContentOnSizeChanged;


                Device.StartTimer(TimeSpan.FromMilliseconds(10), () =>
                {
                    //Debug.WriteLine(App.Dimensions.Height);
                    //Debug.WriteLine(_minified);
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

        private bool _minified = false;
        private void ContentOnSizeChanged(object sender, EventArgs eventArgs)
        {
            try
            {

                HeightChanged?.Invoke(this, Content.Height);
                /*
                if (Content.Height < 380) // 380
                {
                    
                    if (!_minified)
                    {
                        Debug.WriteLine("minifying");
                        _minified = true;
                        foreach (var child in _layout.Children)
                        {
                            if (child != _button)
                                child.LayoutTo(new Rectangle(child.Bounds.X, child.Bounds.Y - 40, child.Bounds.Width, child.Bounds.Height), 100, Easing.CubicOut);//child.TranslateTo(0, -40, 100, Easing.CubicOut);
                        }
                    }
                    //_currentRegStep?.Minimize();
                }
                else
                {
                    
                    if (_minified)
                    {
                        Debug.WriteLine("maximizing");
                        _minified = false;
                        foreach (var child in _layout.Children)
                        {
                            if (child != _button)
                                child.LayoutTo(new Rectangle(child.Bounds.X, child.Bounds.Y + 40, child.Bounds.Width, child.Bounds.Height), 100, Easing.CubicOut);//child.TranslateTo(0, 0, 100, Easing.CubicOut);
                        }
                    }
                    
                    //_currentRegStep?.Maximize();

                    _currentRegStep?.Maximize();
                }
                */
                _button.Layout(new Rectangle(_button.Bounds.X, Content.Height - 64, _button.Bounds.Width,
                    _button.Bounds.Height));
                if (_currentRegStep != null && _currentRegStep.IsDismissible)
                    _nativeControls.SetAreaEntrySafe(_button.Bounds);
                else
                    _nativeControls.SetAreaEntrySafe(new Rectangle(0, 0, Content.Width, Content.Height));
            }
            catch (Exception ex)
            {
                var a = 234;
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _nativeControls.SetAdjustResize(true);
            try
            {
                var animations = new List<Task<bool>>();

                animations.Add(_loginSignUpLabel.TranslateTo(0, 0, 500, Easing.CubicOut));
                animations.Add(_loginSignInButton.TranslateTo(0, 0, 500, Easing.CubicOut));
                animations.Add(_loginSignInWLWButton.TranslateTo(0, 0, 500, Easing.CubicOut));
                animations.Add(_loginLogoColorex.TranslateTo(0, 0, 500, Easing.CubicOut));
                animations.Add(_infoLabel.TranslateTo(0, 0, 500, Easing.CubicOut));
                animations.AddRange(_steps.Select(step => step.TranslateTo(0, 0, 500, Easing.CubicOut)));
                animations.Add(_backArrow.TranslateTo(0, 0, 500, Easing.CubicOut));
                animations.Add(_notNowLabel.TranslateTo(0, 0, 500, Easing.CubicOut));
                animations.Add(_button.TranslateTo(0, 0, 500, Easing.CubicOut));
                animations.Add(_registrationBar.TranslateTo(0, 0, 500, Easing.CubicOut));

                await Task.WhenAll(animations);

                _currentRegStep?.FirstFocusEntry.Focus();

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

        public void SetSteps(List<RegistrationStep> steps, int currentStepIndex)
        {
            _steps = steps;
            for (int i = 0; i < steps.Count; i++)
            {
                _currentRegStep = steps[currentStepIndex];
                _layout.Children.Add(steps[i], new Rectangle(23 + (i - currentStepIndex) * App.Dimensions.Width, 127 - 25, App.Dimensions.Width - 2 * 23, AbsoluteLayout.AutoSize));
                _currentRegStep.TranslationY = App.Dimensions.Height;
            }
        }

        public event EventHandler<double> HeightChanged;

        public Task MoveCurrentStepBy(int steps)
        {
            var animations = new List<Task<bool>>();
            foreach (var step in _steps)
            {
                var oldTranslationX = step.TranslationX;
                var oldTranslationY = step.TranslationY;
                animations.Add(step.TranslateTo(oldTranslationX - App.Dimensions.Width * steps, oldTranslationY, 200, Easing.CubicOut));
            }
            _currentRegStep = _steps[_steps.IndexOf(_currentRegStep) + 1];

            if (_currentRegStep != null && _currentRegStep.IsDismissible)
                _nativeControls.SetAreaEntrySafe(_button.Bounds);
            else
                _nativeControls.SetAreaEntrySafe(new Rectangle(0, 0, Content.Width, Content.Height));

            return Task.WhenAll(animations);

        }
    }
}
