using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeColorex.CustomViews
{
    public enum StickyButtonState
    {
        Loading, Next, Success, Error
    }


    public class Entry2 : Entry
    {

    }

    public class StickyButton : ContentView
    {
        public StickyButtonState State { private set; get; }
        private AbsoluteLayout _layout;
        private BoxView _boxView;
        private LabelEx _centralText;
        private LabelEx _secondaryText;
        private Image _loadingCircle;
        private Color _blue = Color.FromRgb(63, 142, 253);
        private Color _red = Color.FromRgb(255, 62, 46);
        private Color _green = Color.FromRgb(19, 183, 42);

        public event EventHandler Clicked;

        public StickyButton()
        {
            _layout = new AbsoluteLayout();
            //_layout.BackgroundColor = Color.Aqua;
            _boxView = new BoxView { Color = _blue };
            _centralText = new LabelEx { InputTransparent = true, IsEnabled = false, TextColor = Color.White, FontName = "Karla-Bold", FontSize = 17, WidthRequest = 200, HorizontalTextAlignment = TextAlignment.Center };
            _secondaryText = new LabelEx { InputTransparent = true, IsEnabled = false, TextColor = Color.White, FontName = "Karla-Regular", FontSize = 15, WidthRequest = 200, HorizontalTextAlignment = TextAlignment.Center };
            _loadingCircle = new Image { InputTransparent = true, IsEnabled = false, Source = ImageSource.FromFile("loaderSticky.png"), WidthRequest = 30, Aspect = Aspect.AspectFit };
            RotateElement(_loadingCircle);
            _layout.Children.Add(_boxView, new Rectangle(0, 14, 1, 50), AbsoluteLayoutFlags.WidthProportional);
            _layout.Children.Add(_loadingCircle, new Rectangle(0.5, 14 + 8.5, 30, 30), AbsoluteLayoutFlags.XProportional);
            _layout.Children.Add(_centralText, new Rectangle(0.5, 30, 200, 30), AbsoluteLayoutFlags.XProportional);
            _layout.Children.Add(_secondaryText, new Rectangle(0.5, 11, 200, 30), AbsoluteLayoutFlags.XProportional);
            SetState(StickyButtonState.Loading, false);
            Content = _layout;

            var tapGestureRecognizer = new TapGestureRecognizer();

            tapGestureRecognizer.Tapped += (s, e) => {
                Clicked?.Invoke(this, new EventArgs());
            };

            _boxView.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private async Task RotateElement(VisualElement element)
        {
            //while (!cancellation.IsCancellationRequested)
            while (true)
            {
                await element.RotateTo(360, 800, Easing.Linear);
                await element.RotateTo(0, 0); // reset to initial position
            }
        }

        public void SetState(StickyButtonState newState, bool animate)
        {
            if (newState != State)
            {
                if (!animate)
                {
                    State = newState;
                    if (newState == StickyButtonState.Loading)
                    {
                        _boxView.Layout(new Rectangle(0, 14, _boxView.Bounds.Width, 50));
                        _boxView.Color = _blue;
                        _centralText.Opacity = 0;
                        _secondaryText.Opacity = 0;
                        _loadingCircle.Opacity = 1;
                    }
                    if (newState == StickyButtonState.Next)
                    {
                        _boxView.Layout(new Rectangle(0, 14, _boxView.Bounds.Width, 50));
                        _centralText.Text = "Next";
                        _centralText.Layout(new Rectangle(_centralText.Bounds.X, 15 + 14, _centralText.Bounds.Width, _centralText.Bounds.Height));
                        _boxView.Color = _blue;
                        _centralText.Opacity = 1;
                        _secondaryText.Opacity = 0;
                        _loadingCircle.Opacity = 0;
                    }

                    if (newState == StickyButtonState.Success)
                    {
                        _boxView.Layout(new Rectangle(0, 14, _boxView.Bounds.Width, 50));
                        _centralText.Text = "Great!";
                        _centralText.Layout(new Rectangle(_centralText.Bounds.X, 15 + 14, _centralText.Bounds.Width, _centralText.Bounds.Height));
                        _boxView.Color = _green;
                        _centralText.Opacity = 1;
                        _secondaryText.Opacity = 0;
                        _loadingCircle.Opacity = 0;
                    }

                    if (newState == StickyButtonState.Error)
                    {
                        _boxView.Layout(new Rectangle(0, 0, _boxView.Bounds.Width, 64));
                        _boxView.Color = _red;
                        _secondaryText.Opacity = 1;
                        _secondaryText.Text = "Invalid code";
                        _centralText.Text = "Resend";
                        _centralText.Opacity = 1;
                        _centralText.Layout(new Rectangle(_centralText.Bounds.X, 16 + 14, _centralText.Bounds.Width, _centralText.Bounds.Height));
                        _loadingCircle.Opacity = 0;
                    }
                }
                else
                {
                    var oldState = State;
                    State = newState;

                    if (newState == StickyButtonState.Loading)
                    {
                        _boxView.LayoutTo(new Rectangle(0, 14, _boxView.Bounds.Width, 50), 100);
                        _boxView.Color = _blue;
                        _centralText.FadeTo(0, 200);
                        _secondaryText.FadeTo(0, 200);
                        _loadingCircle.FadeTo(1, 200);
                    }
                    if (newState == StickyButtonState.Next)
                    {
                        _boxView.LayoutTo(new Rectangle(0, 14, _boxView.Bounds.Width, 50), 100);
                        _centralText.Text = "Next";
                        _centralText.LayoutTo(new Rectangle(_centralText.Bounds.X, 15 + 14, _centralText.Bounds.Width, _centralText.Bounds.Height), 100);
                        _boxView.Color = _blue;
                        _centralText.FadeTo(1, 200);
                        _secondaryText.FadeTo(0, 200);
                        _loadingCircle.FadeTo(0, 200);
                    }

                    if (newState == StickyButtonState.Success)
                    {
                        _boxView.LayoutTo(new Rectangle(0, 14, _boxView.Bounds.Width, 50), 100);
                        _centralText.Text = "Great!";
                        _centralText.LayoutTo(new Rectangle(_centralText.Bounds.X, 15 + 14, _centralText.Bounds.Width, _centralText.Bounds.Height), 100);
                        _boxView.Color = _green;
                        _centralText.FadeTo(1, 200);
                        _secondaryText.FadeTo(0, 200);
                        _loadingCircle.FadeTo(0, 200);
                    }

                    if (newState == StickyButtonState.Error)
                    {
                        _boxView.LayoutTo(new Rectangle(0, 0, _boxView.Bounds.Width, 64), 100);
                        _boxView.Color = _red;
                        _secondaryText.FadeTo(1, 200);
                        _secondaryText.Layout(new Rectangle(_centralText.Bounds.X, 16, _centralText.Bounds.Width, _centralText.Bounds.Height));
                        _secondaryText.LayoutTo(new Rectangle(_centralText.Bounds.X, 11, _centralText.Bounds.Width, _centralText.Bounds.Height), 200);
                        _secondaryText.Text = "Invalid code";
                        _centralText.Text = "Resend";
                        _centralText.FadeTo(1, 200);
                        _centralText.LayoutTo(new Rectangle(_centralText.Bounds.X, 16 + 14, _centralText.Bounds.Width, _centralText.Bounds.Height), 100);
                        _loadingCircle.FadeTo(0, 200);
                    }
                }
            }
        }

        public Task Hide()
        {
            return _layout.TranslateTo(0, 100, 200, Easing.CubicOut);
        }

        public Task Show()
        {
            return _layout.TranslateTo(0, 0, 200, Easing.CubicOut);
        }
    }
}
