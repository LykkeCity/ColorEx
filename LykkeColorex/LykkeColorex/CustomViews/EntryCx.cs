using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeColorex.CustomViews
{
    public class EntryCx : ContentView
    {
        private EntryEx _entry;
        private LabelEx _label;
        private int _fontSize;
        private int _labelUpperFontSize;
        private bool _isRaised;
        private Color _labelNormalColor = Color.FromHex("#8C94A0");
        private Color _labelFocusColor = Color.FromHex("#3F8EFD");
        private Color _entryTextColor = Color.FromHex("#3F4D60");

        private int _entryIndent;
        private double _spacing;

        public LabelEx Label { get { return _label; } }

        public int FontSize
        {
            get { return _fontSize; }
            set
            {
                if (_fontSize != value)
                {
                    _fontSize = value;
                    Redraw();
                }
            }
        }

        public Keyboard Keyboard
        {
            get { return _entry.Keyboard; }
            set { _entry.Keyboard = value; }
        }

        public bool IsPassword
        {
            get { return _entry.IsPassword; }
            set { _entry.IsPassword = value; }
        }

        public TextAlignment HorizontalTextAlignment
        {
            get { return _entry.HorizontalTextAlignment; }
            set { _entry.HorizontalTextAlignment = value; }
        }

        /*
        public int PlaceholderNormalSize
        {
            get { return _labelNormalFontSize; }
            set { _labelNormalFontSize = value; Redraw(); }
        }
        */
        public int PlaceholderUpperSize
        {
            get { return _labelUpperFontSize; }
            set { _labelUpperFontSize = value; }
        }
        public string PlaceholderText
        {
            get { return _label.Text; }
            set { _label.Text = value; }
        }

        public string Text
        {
            get { return _entry.Text; }
            set { _entry.Text = value; }
        }

        public Color TextColor
        {
            get { return _entry.TextColor; }
            set { _entry.TextColor = value; }
        }

        public Color PlaceholderColorActive
        {
            get { return _labelFocusColor; }
            set
            {
                _labelFocusColor = value;
                if (_entry.IsFocused)
                {
                    _label.TextColor = value;
                }
            }
        }

        public Color PlaceholderColorNormal
        {
            get { return _labelNormalColor; }
            set
            {
                _labelNormalColor = value;
                if (!_entry.IsFocused)
                {
                    _entry.TextColor = value;
                }
            }
        }

        private void Redraw()
        {
            try
            {
                _isRaised = false;
                var al = new AbsoluteLayout();
                //  al.BackgroundColor = Color.Red;
                _entry = new EntryEx
                {
                    HorizontalOptions = LayoutOptions.Fill,
                    FontSize = _fontSize,
                    TextColor = _entryTextColor,
                    //   BackgroundColor = Color.Blue
                };
                _entry.Focused += EntryFocused;
                _entry.Unfocused += EntryUnfocused;
                _label = new LabelEx
                {
                    TextColor = _labelNormalColor,
                    Text = "some",
                    FontSize = _fontSize,
                    HorizontalOptions = LayoutOptions.Fill,
                    //        BackgroundColor = Color.Lime
                };
                al.Children.Add(_entry, new Rectangle(_entryIndent, 0 + 8.5 + _fontSize, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional);
                al.Children.Add(_label, new Rectangle(5, 10 + 8.5 + _fontSize, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional);
                Content = al;
            }
            catch (Exception ex)
            {
                var a = 234;
            }
        }

        public EntryCx()
        {
            _fontSize = 17;
            _spacing = 4;
            _entryIndent = 0;
            Redraw();
        }

        private void EntryFocused(object sender, FocusEventArgs e)
        {
            _label.TextColor = _labelFocusColor;
            if (!_isRaised)
            {
                var m = (_label.Width - _label.Width * ((double)_labelUpperFontSize / (double)_fontSize)) / 2;
                _label.ScaleTo((double)_labelUpperFontSize / (double)_fontSize, 100, Easing.SinInOut);
                Rectangle oldBounds = _label.Bounds;
                Rectangle newBounds = new Rectangle(new Point(new Size(5 - m, oldBounds.Y - _spacing - 10 - _fontSize)),
                    oldBounds.Size);
                _label.LayoutTo(newBounds, 100, Easing.SinInOut);
                _isRaised = true;
            }
        }
        private void EntryUnfocused(object sender, FocusEventArgs e)
        {
            _label.TextColor = _labelNormalColor;
            if (string.IsNullOrEmpty(_entry.Text))
            {
                _label.ScaleTo(1, 100, Easing.SinInOut);
                Rectangle oldBounds = _label.Bounds;
                Rectangle newBounds = new Rectangle(new Point(new Size(5, oldBounds.Y + _spacing + 10 + _fontSize)), oldBounds.Size);
                _label.LayoutTo(newBounds, 100, Easing.SinInOut);
                _isRaised = false;
            }
        }
    }
}
