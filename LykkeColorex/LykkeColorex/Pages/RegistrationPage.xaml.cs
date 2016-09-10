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

        public RegistrationPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            BackgroundColor = Color.White;

            al = new AbsoluteLayout() { HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill };

            var rb = new RegistrationProgressBar((int)(App.Dimensions.Width - 2 * LoginPageLayout.Padding), 5);

            al.Children.Add(rb, new Rectangle(LoginPageLayout.Padding, RegistrationPageLayout.RegistrationProgressBarFromTop, AbsoluteLayout.AutoSize, 3));

            _button = new StickyButton
            {
                InputTransparent = false
            };

            al.Children.Add(_button, new Rectangle(0, App.Dimensions.Height - 64, App.Dimensions.Width + 1, 64));
            _button.Clicked += (sender, args) =>
            {
                var b = (StickyButton)sender;
                if(b.State == StickyButtonState.Error) b.SetState(StickyButtonState.Loading, true);
                else if (b.State == StickyButtonState.Loading) b.SetState(StickyButtonState.Next, true);
                else if (b.State == StickyButtonState.Next) b.SetState(StickyButtonState.Success, true);
                else if (b.State == StickyButtonState.Success) b.SetState(StickyButtonState.Error, true);
            };

            var entry = new Entry
            {
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.Red
            };

            al.Children.Add(entry, new Rectangle(100,100, 200, 100));


            Content = al;

            Content.SizeChanged += ContentOnSizeChanged;


            Device.StartTimer(TimeSpan.FromMilliseconds(300), () =>
            {
                Debug.WriteLine(Content.Height);
                return true;
            });
        }

        private void ContentOnSizeChanged(object sender, EventArgs eventArgs)
        {
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
