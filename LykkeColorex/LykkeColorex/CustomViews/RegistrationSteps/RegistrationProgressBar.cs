﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeColorex.CustomViews;
using Xamarin.Forms;

namespace LykkeColorex.CustomViews.RegistrationSteps
{
    public class RegistrationProgressBar : ContentView, IRegistrationProgressBar
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

        public RegistrationProgressBar(int width, int steps, int atStep = 0)
        {
            _steps = steps;
            _spacing = 8;
            _width = width;
            _atStep = atStep;
            _height = 3;
            _widthOfItem = (_width - (_spacing * (_steps - 1))) / (double)_steps;
            _boxes = new RoundedBoxView[_steps];
            _al = new AbsoluteLayout();
            Draw();
        }

        public async Task Next()
        {

            if (_atStep + 1 < _steps)
            {
                var animatable = new RoundedBoxView
                {
                    Color = Color.FromRgb(63, 142, 253),
                    CornerRadius = 2,
                    Opacity = 0
                };

                _al.Children.Add(animatable, new Rectangle(_boxes[_atStep + 1].X, _boxes[_atStep + 1].Y, _boxes[_atStep + 1].Width, _boxes[_atStep + 1].Height));
                //await animatable.LayoutTo(_boxes[_atStep + 1].Bounds, 200, Easing.CubicOut);
                await animatable.FadeTo(1, 200);
                _boxes[_atStep + 1].Color = Color.FromRgb(63, 142, 253);
                _al.Children.Remove(animatable);

                _atStep++;
            }
        }

        public async Task Previous()
        {
            if (_atStep >= 1)
            {
                var animatable = new RoundedBoxView
                {
                    Color = Color.FromRgb(63, 142, 253),
                    CornerRadius = 2,
                    Opacity = 1
                };



                _al.Children.Add(animatable, new Rectangle(_boxes[_atStep].X, _boxes[_atStep].Y, _boxes[_atStep].Width, _boxes[_atStep].Height));
                _boxes[_atStep].Color = Color.FromRgb(235, 237, 239);
                await animatable.FadeTo(0, 200);
                //await animatable.LayoutTo(_boxes[_atStep].Bounds, 200, Easing.CubicOut);

                _al.Children.Remove(animatable);

                _atStep--;
            }
        }

        private void Draw()
        {

            Device.BeginInvokeOnMainThread(() => _al.Children.Clear());

            _al.HeightRequest = _height;
            _al.WidthRequest = _width;

            _boxes = new RoundedBoxView[_steps];

            for (int i = 0; i < _steps; i++)
            {
                try
                {
                    var b = new RoundedBoxView
                    {
                        Color = i <= _atStep ? Color.FromRgb(63, 142, 253) : Color.FromRgb(235, 237, 239),
                        CornerRadius = 2,

                    };
                    _boxes[i] = b;
                    Device.BeginInvokeOnMainThread(() => _al.Children.Add(b, new Rectangle((_spacing + _widthOfItem) * i, 0, _widthOfItem, _height)));
                }
                catch (Exception e)
                {
                    var a = 234;
                }
            }

            Content = _al;
        }

        public Task MoveCurrentStepBy(int steps)
        {
            if (steps > 0)
                return Next();
            else
                return Previous();
        }

        public void Setup(int count, int currentStepIndex)
        {
            Debug.WriteLine($"Redrawing Stepper with # of steps {count} starting at step {currentStepIndex}");
            _steps = count;
            _atStep = currentStepIndex;
            _widthOfItem = (_width - (_spacing * (_steps - 1))) / (double)_steps;
            Draw();
        }
    }
}
