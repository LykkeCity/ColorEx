using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LykkeColorex.Annotations;
using Xamarin.Forms;

namespace LykkeColorex.CustomViews.Popup
{
    public class SingleSelectItemModel : INotifyPropertyChanged
    {
        private string _title;
        public string Title
        {
            set
            {
                if (value != _title)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
            get { return _title; }
        }
        
        private bool _isSelected;
        public bool IsSelected
        {
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    OnPropertyChanged();
                }
            }
            get { return _isSelected; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class SingleSelectDoubleDataItemModel : INotifyPropertyChanged
    {
        private string _title;
        public string Title
        {
            set
            {
                if (value != _title)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
            get { return _title; }
        }

        private string _secondaryTitle;
        public string SecondaryTitle
        {
            set
            {
                if (value != _secondaryTitle)
                {
                    _secondaryTitle = value;
                    OnPropertyChanged();
                }
            }
            get { return _secondaryTitle; }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    OnPropertyChanged();
                }
            }
            get { return _isSelected; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class SelectPopupCx<T> : ContentView
    {
        private ListView _listView;
        private List<T> _objects;

        public SelectPopupCx(string title, List<T> objects, Func<T, string> selector, bool hasDefault, T defaultObject = default(T))
        {
            _listView = new ListView(ListViewCachingStrategy.RecycleElement);

            _objects = objects;

            var l =
                objects.Select(item => new { item, s = selector(item) })
                    .Select(
                        @t =>
                            new SingleSelectItemModel
                            {
                                Title = @t.s,
                                IsSelected = hasDefault && @t.item.Equals(defaultObject)
                            });

            _listView.ItemsSource = l;

            _listView.ItemTemplate = new DataTemplate(typeof(SingleItemSelectViewCell));

            _listView.SeparatorColor = Color.FromRgb(222, 225, 228);

            _listView.RowHeight = 52;

            _listView.ItemSelected += delegate
            {
                if (_listView.SelectedItem != null)
                    Device.BeginInvokeOnMainThread(() => _listView.SelectedItem = null);
            };
            _listView.ItemTapped += (sender, args) =>
            {
                OnItemSelected(_objects[_objects.Select(selector).ToList().IndexOf((args.Item as SingleSelectItemModel).Title)]);
            };

            var al = new AbsoluteLayout();


            var label = new LabelEx
            {
                Text = title,
                FontSize = 19,
                TextColor = Color.FromRgb(51, 51, 51),
                HorizontalTextAlignment = TextAlignment.Center,
                FontName = "Lato-Bold"
            };

            var bg = new RoundedBoxView() { Color = Color.White, CornerRadius = 8 };

            al.Children.Add(bg, new Rectangle(1, 1, 1, 0), AbsoluteLayoutFlags.PositionProportional | AbsoluteLayoutFlags.WidthProportional);
            al.Children.Add(_listView, new Rectangle(16, 56, App.Dimensions.Width - 16 * 2, 0));
            al.Children.Add(label, new Rectangle(0, 16, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional);

            Content = al;

            Content.SizeChanged += delegate
            {
                _listView.Layout(new Rectangle(16, 56, App.Dimensions.Width - 16 * 2, Content.Height - 56));
                bg.Layout(new Rectangle(0, 0, bg.Bounds.Width, Content.Height + 50));
            };
        }

        public SelectPopupCx(string title, List<T> objects, Func<T, Tuple<string, string>> selector, bool hasDefault, T defaultObject = default(T))
        {
            _listView = new ListView(ListViewCachingStrategy.RecycleElement);

            _objects = objects;

            var l = objects.Select(item => new SingleSelectDoubleDataItemModel
            {
                IsSelected = hasDefault && item.Equals(defaultObject),
                SecondaryTitle = selector(item).Item2,
                Title = selector(item).Item1,
            }).ToList();

            _listView.ItemsSource = l;

            _listView.ItemTemplate = new DataTemplate(typeof(SingleItemSelectDoubleDataViewCell));

            _listView.SeparatorColor = Color.FromRgb(222, 225, 228);

            _listView.RowHeight = 52;

            _listView.ItemSelected += delegate
            {
                if (_listView.SelectedItem != null)
                    Device.BeginInvokeOnMainThread(() => _listView.SelectedItem = null);
            };

            bool tapProcessing = false;
            _listView.ItemTapped += async (sender, args) =>
            {
                if (!tapProcessing)
                {
                    tapProcessing = true;

                    var castedItem = args.Item as SingleSelectDoubleDataItemModel;
                    foreach (var item in _listView.ItemsSource)
                    {
                        var casted = item as SingleSelectDoubleDataItemModel;
                        casted.IsSelected = false;
                    }
                    castedItem.IsSelected = true;
                    await Task.Delay(150);
                    var obj =
                        _objects.FirstOrDefault(
                            x => selector(x).Item1 == castedItem.Title && selector(x).Item2 == castedItem.SecondaryTitle);
                    OnItemSelected(obj);
                }
            };

            var al = new AbsoluteLayout();


            var label = new LabelEx
            {
                Text = title,
                FontSize = 19,
                TextColor = Color.FromRgb(51, 51, 51),
                HorizontalTextAlignment = TextAlignment.Center,
                FontName = "Lato-Bold"
            };

            var bg = new RoundedBoxView() {Color = Color.White, CornerRadius = 8};

            al.Children.Add(bg, new Rectangle(1, 1, 1, 0), AbsoluteLayoutFlags.PositionProportional|AbsoluteLayoutFlags.WidthProportional);
            al.Children.Add(_listView, new Rectangle(16, 56, App.Dimensions.Width - 16 * 2, 0));
            al.Children.Add(label, new Rectangle(0, 16, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional);

            Content = al;

            Content.SizeChanged += delegate
            {
                _listView.Layout(new Rectangle(16, 56, App.Dimensions.Width - 16 * 2, Content.Height - 56));
                bg.Layout(new Rectangle(0, 0, bg.Bounds.Width, Content.Height + 50));
            };

        }

        public event EventHandler<T> ItemSelected;

        protected virtual void OnItemSelected(T e)
        {
            ItemSelected?.Invoke(this, e);
        }
    }
}
