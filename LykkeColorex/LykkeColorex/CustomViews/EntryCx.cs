using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeColorex.CustomViews
{
    public enum EntryCxState
    {
        Normal, Active, Error
    }

    public class EntryCx : ContentView
    {
        private EntryEx _entry;
        private LabelEx _label;
        private BoxView _underline;
        private BoxView _errorUnderline;
        private BoxView _underlineBlue;
        private int _fontSize;
        private int _labelUpperFontSize;
        private bool _isRaised;
        private Color _labelNormalColor = Color.FromHex("#8C94A0");
        private Color _underlineErrorColor = Color.FromRgb(255, 62, 46);
        private Color _labelFocusColor = Color.FromHex("#3F8EFD");
        private Color _entryTextColor = Color.FromHex("#3F4D60");

        private EntryCxState State { set; get; }

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

        public async void SetNormal()
        {
            await _errorUnderline.FadeTo(0, 100);
        }

        public async void SetError()
        {
            await _errorUnderline.FadeTo(1, 100);
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

        public new void Unfocus()
        {
            _entry.Unfocus();
        }

        public new void Focus()
        {
            _entry.Focus();
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

        public int ItemHeight { set; get; }
        
        public EntryEx Entry { get { return _entry; } }

        private void Redraw()
        {
            try
            {
                _isRaised = false;
                var al = new AbsoluteLayout();

                _entry = new EntryEx
                {
                    HorizontalOptions = LayoutOptions.Fill,
                    FontSize = _fontSize,
                    TextColor = _entryTextColor
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
                _label.AnchorX = 0;

                _errorUnderline = new BoxView { Color = _underlineErrorColor, Opacity = 0};

                _underlineBlue = new BoxView {HeightRequest = 2, Color = Color.FromRgb(63, 142, 253), Opacity = 0};
                _underline = new BoxView {HeightRequest = 0.5, Color = State == EntryCxState.Normal ? Color.FromRgb(222, 225, 228) : ( State == EntryCxState.Active ? Color.FromRgb(63, 142, 253) : Color.FromRgb(255, 62, 46)), HorizontalOptions = LayoutOptions.Fill};
                al.Children.Add(_entry, new Rectangle(_entryIndent, 0 + 16 + _fontSize, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional);
                al.Children.Add(_label, new Rectangle(2, 10 + 16 + _fontSize, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional);
                al.Children.Add(_underline, new Rectangle(0, 80 - 1, 1, 0.5), AbsoluteLayoutFlags.WidthProportional);
                al.Children.Add(_underlineBlue, new Rectangle(0, 80 - 1, 0, 1.5));
                al.Children.Add(_errorUnderline, new Rectangle(0, 80 - 2, 1, 3), AbsoluteLayoutFlags.WidthProportional);

                Content = al;

                Debug.WriteLine("redrawn...");

                 
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
            _entryIndent = -2;
            ItemHeight = 80;
            State = EntryCxState.Normal;
            Redraw();
        }

        private void EntryFocused(object sender, FocusEventArgs e)
        {
            _label.TextColor = _labelFocusColor;
            _underlineBlue.LayoutTo(new Rectangle(_underlineBlue.X, _underlineBlue.Y, _underline.Width, 2), 130, Easing.CubicOut);
            _underlineBlue.FadeTo(1, 130);
            if (!_isRaised)
            {
                _label.ScaleTo((double)_labelUpperFontSize / (double)_fontSize, 100, Easing.SinInOut);
                _label.TranslateTo(0, - (_spacing + 10 + _fontSize), 100, Easing.SinInOut);
                _isRaised = true;
            }
        }
        private void EntryUnfocused(object sender, FocusEventArgs e)
        {
            _label.TextColor = _labelNormalColor;
            _underline.Color = Color.FromRgb(222, 225, 228);
            if (string.IsNullOrEmpty(_entry.Text))
            {
                _underline.HeightRequest = 2;
                _label.ScaleTo(1, 100, Easing.SinInOut);
                _label.TranslateTo(0, 0, 100, Easing.SinInOut);
                _isRaised = false;
            }
            _underlineBlue.LayoutTo(new Rectangle(_underlineBlue.X, _underlineBlue.Y, 0, 2), 130, Easing.CubicOut);
            _underlineBlue.FadeTo(0, 130);
            Debug.WriteLine("unfocused...");
        }
    }
}
