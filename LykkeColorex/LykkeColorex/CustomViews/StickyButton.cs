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

    public interface IStickyButton
    {
        StickyButtonState State { get; }
        Task Hide();
        Task Show();
        Task SetState(StickyButtonState newState, bool animate, string centralText = null, string secondaryText = null);

        event EventHandler Clicked;
    }

    public class StickyButton : ContentView, IStickyButton
    {
        public StickyButtonState State { private set; get; }

        private AbsoluteLayout _layout;
        private BoxView _boxView;
        private LabelEx _centralText;
        private LabelEx _secondaryText;
        private Image _loadingCircle;
        private Color _blue = Color.FromRgb(63, 142, 253);
        private Color _red = Color.FromRgb(255, 62, 46);
        private Color _green = Color.FromRgb(30, 215, 97);

        public event EventHandler Clicked;

        public StickyButton()
        {
            _layout = new AbsoluteLayout();
            //_layout.BackgroundColor = Color.Aqua;
            _boxView = new BoxView { Color = _blue };
            _centralText = new LabelEx { InputTransparent = true, IsEnabled = false, TextColor = Color.White, FontName = "Lato-Bold", FontSize = 17, WidthRequest = 200, HorizontalTextAlignment = TextAlignment.Center };
            _secondaryText = new LabelEx { InputTransparent = true, IsEnabled = false, TextColor = Color.White, FontName = "Lato-Regular", FontSize = 15, WidthRequest = 200, HorizontalTextAlignment = TextAlignment.Center };
            _loadingCircle = new Image { InputTransparent = true, IsEnabled = false, Source = ImageSource.FromFile("loaderSticky.png"), WidthRequest = 30, Aspect = Aspect.AspectFit };
            RotateElement(_loadingCircle);
            _layout.Children.Add(_boxView, new Rectangle(0, 14, 1, 50), AbsoluteLayoutFlags.WidthProportional);
            _layout.Children.Add(_loadingCircle, new Rectangle(0.5, 14 + 8.5, 30, 30), AbsoluteLayoutFlags.XProportional);
            _layout.Children.Add(_centralText, new Rectangle(0.5, 25, 200, 30), AbsoluteLayoutFlags.XProportional);
            _layout.Children.Add(_secondaryText, new Rectangle(0.5, 11, 200, 30), AbsoluteLayoutFlags.XProportional);
            SetState(StickyButtonState.Loading, false);
            Content = _layout;

            var tapGestureRecognizer = new TapGestureRecognizer();

            tapGestureRecognizer.Tapped += (s, e) =>
            {
                Clicked?.Invoke(this, new EventArgs());
            };

            _boxView.GestureRecognizers.Add(tapGestureRecognizer);

            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "Y")
                {
                    Debug.WriteLine("Y poperty changed!!");
                    Layout(new Rectangle(Bounds.X, ((AbsoluteLayout)Parent).Height - 64, Bounds.Width, Bounds.Height));
                }
            };

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

        public Task SetState(StickyButtonState newState, bool animate, string centralText = null, string secondaryText = null)
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
                        _centralText.Text = string.IsNullOrEmpty(centralText) ? "Next" : centralText;
                        _centralText.Layout(new Rectangle(_centralText.Bounds.X, 15 + 14, _centralText.Bounds.Width, _centralText.Bounds.Height));
                        _boxView.Color = _blue;
                        _centralText.Opacity = 1;
                        _secondaryText.Opacity = 0;
                        _loadingCircle.Opacity = 0;
                    }

                    if (newState == StickyButtonState.Success)
                    {
                        _boxView.Layout(new Rectangle(0, 14, _boxView.Bounds.Width, 50));
                        _centralText.Text = string.IsNullOrEmpty(centralText) ? "Great!" : centralText;
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
                        _secondaryText.Text = string.IsNullOrEmpty(secondaryText) ? "Error" : secondaryText;
                        _centralText.Text = string.IsNullOrEmpty(centralText) ? "Error" : centralText;
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
                        var a1 = _boxView.LayoutTo(new Rectangle(0, 14, _boxView.Bounds.Width, 50), 100);
                        _boxView.Color = _blue;
                        var a2 = _centralText.FadeTo(0, 200);
                        var a3 = _secondaryText.FadeTo(0, 200);
                        var a4 = _loadingCircle.FadeTo(1, 200);

                        return Task.WhenAll(a1, a2, a3, a4);
                    }
                    if (newState == StickyButtonState.Next)
                    {
                        var a1 = _boxView.LayoutTo(new Rectangle(0, 14, _boxView.Bounds.Width, 50), 100);
                        _centralText.Text = string.IsNullOrEmpty(centralText) ? "Next" : centralText;
                        var a3 = _centralText.LayoutTo(new Rectangle(_centralText.Bounds.X, 15 + 14, _centralText.Bounds.Width, _centralText.Bounds.Height), 100);
                        _boxView.Color = _blue;
                        var a4 = _centralText.FadeTo(1, 200);
                        var a5 = _secondaryText.FadeTo(0, 200);
                        var a6 = _loadingCircle.FadeTo(0, 200);

                        return Task.WhenAll(a1, a3, a4, a5, a6);
                    }

                    if (newState == StickyButtonState.Success)
                    {
                        var a1 = _boxView.LayoutTo(new Rectangle(0, 14, _boxView.Bounds.Width, 50), 100);
                        _centralText.Text = string.IsNullOrEmpty(centralText) ? "Great!" : centralText;
                        var a2 = _centralText.LayoutTo(new Rectangle(_centralText.Bounds.X, 15 + 14, _centralText.Bounds.Width, _centralText.Bounds.Height), 100);
                        _boxView.Color = _green;
                        var a3 = _centralText.FadeTo(1, 200);
                        var a4 = _secondaryText.FadeTo(0, 200);
                        var a5 = _loadingCircle.FadeTo(0, 200);

                        return Task.WhenAll(a1, a2, a3, a4, a5);
                    }

                    if (newState == StickyButtonState.Error)
                    {
                        var a1 = _boxView.LayoutTo(new Rectangle(0, 0, _boxView.Bounds.Width, 64), 100);
                        _boxView.Color = _red;
                        var a2 = _secondaryText.FadeTo(1, 200);
                        _secondaryText.Layout(new Rectangle(_centralText.Bounds.X, 16, _centralText.Bounds.Width, _centralText.Bounds.Height));
                        var a3 = _secondaryText.LayoutTo(new Rectangle(_centralText.Bounds.X, 11, _centralText.Bounds.Width, _centralText.Bounds.Height), 200);
                        _secondaryText.Text = string.IsNullOrEmpty(secondaryText) ? "Error" : secondaryText;
                        _centralText.Text = string.IsNullOrEmpty(centralText) ? "Error" : centralText;
                        var a4 = _centralText.FadeTo(1, 200);
                        var a5 = _centralText.LayoutTo(new Rectangle(_centralText.Bounds.X, 16 + 14, _centralText.Bounds.Width, _centralText.Bounds.Height), 100);
                        var a6 = _loadingCircle.FadeTo(0, 200);

                        return Task.WhenAll(a1, a2, a3, a4, a5, a6);
                    }
                }
            }
            return new Task(delegate { });
        }

        public Task Hide()
        {
            return _layout.TranslateTo(0, 100, 300, Easing.CubicOut);
        }

        public Task Show()
        {
            return _layout.TranslateTo(0, 0, 300, Easing.CubicOut);
        }
    }
}
