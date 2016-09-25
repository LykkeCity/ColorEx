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
    public partial class SignInWLWPage : ContentPage
    {
        private AbsoluteLayout _mainLayout;
        private BackArrowCx _backArrow;
        private LabelCx _infoWLWLabel;
        private ButtonOld _signInWLWButton;
        private LabelCx _signInLabel;
        private EntryCx _emailEntry;
        private BoxView _emailEntryLine;
        private EntryCx _passwordEntry;
        private BoxView _passwordEntryLine;
        private LabelCx _forgotLabel;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _backArrow.FadeTo(1, 500);
        }

        public async Task GoBack()
        {
            ((LoginPage)Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]).CameBackFromSignInWLWPage =
                    true;
            
            await Navigation.PopAsync(false);
        }

        protected override bool OnBackButtonPressed()
        {
            ((LoginPage)Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]).CameBackFromSignInWLWPage = true;
            return base.OnBackButtonPressed();
        }

        public SignInWLWPage(Rectangle signInWLWButtonBounds, Rectangle signInLabelBounds, Rectangle forgotPasswordLabelBounds, Rectangle emailEntryLineBounds, Rectangle passwordEntryLineBounds, Rectangle infoWLWLabelBounds)
        {
            //InitializeComponent();

            BackgroundColor = Color.White;

            NavigationPage.SetHasNavigationBar(this, false);
            BackgroundColor = Color.White;

            _mainLayout = new AbsoluteLayout { HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill };

            _backArrow = new BackArrowCx(fake: false)
            {
                Opacity = 0
            };
            _backArrow.Clicked += async delegate
            {
                await GoBack();
            };
            _mainLayout.Children.Add(
                _backArrow,
                new Rectangle(24, Device.OnPlatform(Android: 43 + 5 - 25, iOS: 43, WinPhone: 43), 28, 35));


            _infoWLWLabel = new LabelCx
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

                Opacity = 1
            };
            _infoWLWLabel.SpanClicked += delegate(object sender, EventArgs args)
            {
                var a = 234;
            };
            _mainLayout.Children.Add(_infoWLWLabel, infoWLWLabelBounds);

            _signInWLWButton = new ButtonOld
            {
                TextColor = Color.White,
                Text = "Sign In with Lykke Wallet",
                HorizontalOptions = LayoutOptions.Fill,
                HeightRequest = 60,
                FontSize = 17
            };
            _signInWLWButton.Clicked += (sender, args) =>
            {

            };

            _mainLayout.Children.Add(_signInWLWButton, signInWLWButtonBounds);

            _signInLabel = new LabelCx
            {
                FontSize = 27,
                Text = "Sign In",
                TextColor = Color.FromRgb(51, 51, 51)
            };
            _mainLayout.Children.Add(_signInLabel, signInLabelBounds);

            _emailEntry = new EntryCx
            {
                PlaceholderText = "Email",
                TextColor = Color.FromRgb(63, 77, 96),
                Opacity = 1,
                PlaceholderUpperSize = 14,
                FontSize = 17,
                HeightRequest = 80,
                HorizontalOptions = LayoutOptions.Fill,
                Keyboard = Keyboard.Email
            };
            _mainLayout.Children.Add(_emailEntry, new Rectangle(SignInWLWPageLayout.Padding, SignInWLWPageLayout.EmailEntryFromTop, App.Dimensions.Width - SignInWLWPageLayout.Padding * 2, 80));
            
            _passwordEntry = new EntryCx
            {
                PlaceholderText = "Password",
                TextColor = Color.FromRgb(63, 77, 96),
                Opacity = 1,
                PlaceholderUpperSize = 14,
                FontSize = 17,
                HeightRequest = 80,
                HorizontalOptions = LayoutOptions.Fill,
                IsPassword = true
            };
            _mainLayout.Children.Add(_passwordEntry, new Rectangle(SignInWLWPageLayout.Padding, SignInWLWPageLayout.PasswordEntryFromTop, App.Dimensions.Width - SignInWLWPageLayout.Padding * 2, 80));
            
            _forgotLabel = new LabelCx
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

                Opacity = 1
            };
            _mainLayout.Children.Add(_forgotLabel, forgotPasswordLabelBounds);


            Content = _mainLayout;

        }
    }
}
