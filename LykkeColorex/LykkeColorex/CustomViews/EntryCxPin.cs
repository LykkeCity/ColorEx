using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeColorex.CustomViews
{
    public class EntryCxPin : ContentView
    {
        private AbsoluteLayout _al;
        private NonDismissibleEntry _entry;
        private BoxView _underline;
        private int _fontSize;
        private bool _isRaised;
        private Color _labelNormalColor = Color.FromHex("#8C94A0");
        private Color _labelFocusColor = Color.FromHex("#3F8EFD");
        private Color _entryTextColor = Color.FromHex("#3F4D60");

        private EntryCxState State { set; get; }

        public NonDismissibleEntry Entry { get { return _entry; } }

        private int _entryIndent;
        private double _spacing;

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

        public Color TextColor
        {
            get { return _entry.TextColor; }
            set { _entry.TextColor = value; }
        }

        public int ItemHeight { set; get; }

        private void Redraw()
        {
            try
            {
                _isRaised = false;
                _al = new AbsoluteLayout();

                _entry = new NonDismissibleEntry
                {
                    //BackgroundColor = Color.Red,
                    HorizontalOptions = LayoutOptions.Fill,
                    FontSize = _fontSize,
                    TextColor = _entryTextColor,
                    Opacity = 0,
                    IsPin = true
                };

                _entry.TextChanged += EntryOnTextChanged;

                _dots = new List<RoundedBoxView>();
                _labels = new List<LabelEx>();
                for (int i = 0; i < _size; i++)
                {
                    var dot = new RoundedBoxView { CornerRadius = 4, Color = Color.FromRgb(207, 210, 215) };
                    _dots.Add(dot);
                    _al.Children.Add(dot, new Rectangle(16 + i * 32, 40, 8, 8));


                }

                _underline = new BoxView { HeightRequest = 0.5, Color = State == EntryCxState.Normal ? Color.FromRgb(222, 225, 228) : (State == EntryCxState.Active ? Color.FromRgb(63, 142, 253) : Color.FromRgb(255, 62, 46)), HorizontalOptions = LayoutOptions.Fill };
                _al.Children.Add(_entry, new Rectangle(_entryIndent, 0 + 16 + _fontSize, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional);
                _al.Children.Add(_underline, new Rectangle(0, 80 - 1, 1, 0.5), AbsoluteLayoutFlags.WidthProportional);
                Content = _al;


            }
            catch (Exception ex)
            {
                var a = 234;
            }
        }

        public string Text
        {
            get
            {
                var r = new StringBuilder();
                foreach (var l in _labels)
                {
                    r.Append(l.Text);
                }
                return r.ToString();
            }
        }

        private void EntryOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            if (string.IsNullOrEmpty(textChangedEventArgs.OldTextValue) || textChangedEventArgs.NewTextValue.Length > textChangedEventArgs.OldTextValue.Length)
            {
                if (_numSymbols != _size)
                {
                    _dots[_numSymbols].Opacity = 0;
                    var label = new LabelEx
                    {
                        Text =
                            textChangedEventArgs.NewTextValue[textChangedEventArgs.NewTextValue.Length - 1].ToString(),
                        //BackgroundColor = Color.Red,
                        FontSize = 25,
                        HorizontalOptions = LayoutOptions.Fill,
                        HorizontalTextAlignment = TextAlignment.Center,
                        TextColor = Color.FromRgb(63, 77, 96)
                    };
                    _labels.Add(label);
                    _al.Children.Add(label, new Rectangle(5 + _numSymbols * 32, 30, 32, 25));
                    _numSymbols++;
                    if (_numSymbols == _size)
                    {
                        OnCompleted();
                    }
                }
            }
            else
            {
                if (_numSymbols > 0)
                {
                    _dots[_numSymbols - 1].Opacity = 1;
                    _al.Children.Remove(_labels[_labels.Count - 1]);
                    _labels.RemoveAt(_labels.Count - 1);
                    _numSymbols--;
                }
            }
        }

        public event EventHandler Completed;

        private List<RoundedBoxView> _dots;
        private List<LabelEx> _labels;
        private int _numSymbols;

        private int _size;

        public EntryCxPin(int size)
        {
            _fontSize = 17;
            _numSymbols = 0;
            _size = size;
            _spacing = 4;
            _entryIndent = -2;
            ItemHeight = 80;
            State = EntryCxState.Normal;
            Redraw();
        }

        protected virtual void OnCompleted()
        {
            Completed?.Invoke(this, EventArgs.Empty);
        }
    }
}
