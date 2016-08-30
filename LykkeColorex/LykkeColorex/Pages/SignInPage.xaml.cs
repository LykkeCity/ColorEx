﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeColorex.Constants.Layouts;
using LykkeColorex.CustomViews;
using Xamarin.Forms;

namespace LykkeColorex.Pages
{
    public partial class SignInPage : ContentPage
    {
        private AbsoluteLayout _mainLayout;
        private BackArrowCx _backArrow;
        private ButtonCx _signInButton;
        private LabelCx _signInLabel;
        private EntryCx _emailEntry;
        private BoxView _emailEntryLine;
        private EntryCx _passwordEntry;
        private BoxView _passwordEntryLine;
        private LabelCx _forgotLabel;



        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            _backArrow.FadeTo(1, 500);

        }

        public SignInPage(Rectangle signInButtonBounds, Rectangle signInLabelBounds, Rectangle forgotPasswordLabelBounds, Rectangle emailEntryLineBounds, Rectangle passwordEntryLineBounds)
        {
            //InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            BackgroundColor = Color.White;

            _mainLayout = new AbsoluteLayout { HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill };
            
            _backArrow = new BackArrowCx(fake: false)
            {
                Opacity = 0
            };
            _backArrow.Clicked += async delegate
            {
                ((LoginPage) Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]).CameBackFromSignInPage =
                    true;

                await Navigation.PopAsync(false);
            };
            _mainLayout.Children.Add(
                _backArrow, 
                new Rectangle(24, Device.OnPlatform(Android: 43 + 5, iOS:43, WinPhone:43), 20, 15));
            

            _signInButton = new ButtonCx
            {
                TextColor = Color.White,
                Text = "Sign In",
                HorizontalOptions = LayoutOptions.Fill,
                HeightRequest = 60,
                FontSize = 17
            };
            _signInButton.Clicked += (sender, args) =>
            {

            };

            _mainLayout.Children.Add(_signInButton, signInButtonBounds);

            _signInLabel = new LabelCx {
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
            _mainLayout.Children.Add(_emailEntry, new Rectangle(22, 185, App.Dimensions.Width - 24 * 2, 80));

            _emailEntryLine = new BoxView { BackgroundColor = Color.FromRgb(222, 225, 228), Opacity = 1 };
            _mainLayout.Children.Add(_emailEntryLine, emailEntryLineBounds);

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
            _mainLayout.Children.Add(_passwordEntry, new Rectangle(22, 180 + 80, App.Dimensions.Width - 24 * 2, 80));

            _passwordEntryLine = new BoxView
            {
                BackgroundColor = Color.FromRgb(222, 225, 228),
                Opacity = 1
            };
            _mainLayout.Children.Add(_passwordEntryLine, passwordEntryLineBounds);

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
