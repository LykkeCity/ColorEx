using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeColorex.CustomViews
{
    public class RegistrationProgressBar : ContentView
    {
        private AbsoluteLayout _al;
        private int _steps;
        private int _height;
        private double _widthOfItem;
        private int _width;
        private int _atStep;
        private int _spacing;
        private RoundedBoxView[] _boxes;
        private object lockable = new object();

        public RegistrationProgressBar(int width, int atStep = 0)
        {
            _steps = 8;
            _spacing = 8;
            _width = width;
            _atStep = atStep;
            _height = 3;
            _widthOfItem = (_width - (_spacing * (_steps - 1))) / (double)_steps;
            _boxes = new RoundedBoxView[_steps];
            _al = new AbsoluteLayout();
            Draw();
        }

        public async void Next()
        {

            if (_atStep + 1 < _steps)
            {
                var animatable = new RoundedBoxView
                {
                    Color = Color.FromRgb(63, 142, 253),
                    CornerRadius = 2,

                };

                _al.Children.Add(animatable, new Rectangle(_boxes[_atStep + 1].X, _boxes[_atStep + 1].Y, 0, _boxes[_atStep + 1].Height));
                await animatable.LayoutTo(_boxes[_atStep + 1].Bounds, 200, Easing.CubicOut);

                _boxes[_atStep + 1].Color = Color.FromRgb(63, 142, 253);
                _al.Children.Remove(animatable);

                _atStep++;
            }
        }

        public async void Previous()
        {
            if (_atStep >= 1)
            {
                var animatable = new RoundedBoxView
                {
                    Color = Color.FromRgb(235, 237, 239),
                    CornerRadius = 2,

                };

                _al.Children.Add(animatable, new Rectangle(_boxes[_atStep].X + _boxes[_atStep].Width, _boxes[_atStep].Y, 0, _boxes[_atStep].Height));
                await animatable.LayoutTo(_boxes[_atStep].Bounds, 200, Easing.CubicOut);

                _boxes[_atStep].Color = Color.FromRgb(235, 237, 239);
                _al.Children.Remove(animatable);

                _atStep--;
            }
        }

        public void Draw()
        {
            _al = new AbsoluteLayout();
            //_al.BackgroundColor = Color.Red;
            _al.HeightRequest = _height;
            _al.WidthRequest = _width;

            for (int i = 0; i < _steps; i++)
            {
                var b = new RoundedBoxView
                {
                    Color = i <= _atStep ? Color.FromRgb(63, 142, 253) : Color.FromRgb(235, 237, 239),
                    CornerRadius = 2,

                };
                _boxes[i] = b;
                _al.Children.Add(b, new Rectangle((_spacing + _widthOfItem) * i, 0, _widthOfItem, _height));
            }

            Content = _al;
        }

    }
}
