using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeColorex.Constants.Layouts;
using LykkeColorex.CustomViews;
using Xamarin.Forms;

namespace LykkeColorex.Pages
{
    public partial class RegistrationPage : ContentPage
    {
        private StickyButton _button;
        private AbsoluteLayout al;
        private ClassInterface instance = DependencyService.Get<ClassInterface>();
        private LabelCx _label;
        private EntryCx _entry;
        

        public RegistrationPage()
        {
            try
            {
                NavigationPage.SetHasNavigationBar(this, false);

                BackgroundColor = Color.White;

                al = new AbsoluteLayout() { HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill };

                var rb = new RegistrationProgressBar((int)(App.Dimensions.Width - 2 * LoginPageLayout.Padding), 5);

                al.Children.Add(rb,
                    new Rectangle(LoginPageLayout.Padding, RegistrationPageLayout.RegistrationProgressBarFromTop,
                        AbsoluteLayout.AutoSize, 3));

                _button = new StickyButton
                {
                    InputTransparent = false
                };
                _button.SetState(StickyButtonState.Next, false);
                al.Children.Add(_button, new Rectangle(0, App.Dimensions.Height - 64, App.Dimensions.Width + 1, 64));
                _button.Clicked += (sender, args) =>
                {
                    var b = (StickyButton)sender;
                    if (b.State == StickyButtonState.Error) b.SetState(StickyButtonState.Loading, true);
                    else if (b.State == StickyButtonState.Loading) b.SetState(StickyButtonState.Next, true);
                    else if (b.State == StickyButtonState.Next) b.SetState(StickyButtonState.Success, true);
                    else if (b.State == StickyButtonState.Success) b.SetState(StickyButtonState.Error, true);
                };

                var entry = new Entry
                {
                    HorizontalOptions = LayoutOptions.Fill,
                    BackgroundColor = Color.Red
                };

                //al.Children.Add(entry, new Rectangle(100,100, 200, 100));

                _label = new LabelCx
                {
                    TextColor = Color.FromRgb(51, 51, 51),
                    Text = "Okay, enter your email",
                    FontSize = 27
                };
                al.Children.Add(_label,
                    new Rectangle(RegistrationPageLayout.Padding, RegistrationPageLayout.InfoLabelFromTop, 290, 32));

                _entry = new EntryCx
                {
                    PlaceholderUpperSize = 14,
                    FontSize = 17,
                    PlaceholderText = "Email",
                    TextColor = Color.FromRgb(63, 77, 96),
                    HeightRequest = 80,
                    HorizontalOptions = LayoutOptions.Fill,
                    Keyboard = Keyboard.Email
                };

                al.Children.Add(_entry,
                    new Rectangle(RegistrationPageLayout.Padding, RegistrationPageLayout.EmailEntryFromTop,
                        App.Dimensions.Width - RegistrationPageLayout.Padding * 2, 80));

                var entry2 = new EntryCxPin(4)
                {
                    FontSize = 17,
                    TextColor = Color.FromRgb(63, 77, 96),
                    HeightRequest = 80,
                    HorizontalOptions = LayoutOptions.Fill
                };
                al.Children.Add(entry2,
                    new Rectangle(RegistrationPageLayout.Padding, RegistrationPageLayout.EmailEntryFromTop + 80,
                        App.Dimensions.Width - RegistrationPageLayout.Padding * 2, 80));

                Content = al;

                Content.SizeChanged += ContentOnSizeChanged;


                Device.StartTimer(TimeSpan.FromMilliseconds(300), () =>
                {
                    //Debug.WriteLine(Content.Height);
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
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            DependencyService.Get<ClassInterface>().SetRect(null);
            DependencyService.Get<ClassInterface>().SetAdjustResize(false);
        }
    }
}
