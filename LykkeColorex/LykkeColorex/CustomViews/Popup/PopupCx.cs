using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeColorex.CustomViews.Popup
{
    public class PopupCx<T> : ContentView
    {
        private ListView _listView;
        private List<T> _objects;
        private Func<T, string> _selector; 
        public PopupCx(List<T> objects, Func<T, string> selector, bool hasDefault, T defaultObject = default(T))
        {
            _listView = new ListView();

            _objects = objects;

            _listView.ItemsSource = _objects.Select(selector).Select(x => new { Title = x });

            _listView.ItemTemplate = new DataTemplate(typeof(SingleItemSelectViewCell));

            _listView.SeparatorColor = Color.FromRgb(222, 225, 228);

            _listView.ItemSelected += delegate
            {
                if (_listView.SelectedItem != null)
                    Device.BeginInvokeOnMainThread(() => _listView.SelectedItem = null);
            };
            _listView.ItemTapped += (sender, args) => {
                OnItemSelected(_objects[_objects.Select(selector).ToList().IndexOf(args.Item as string)]);
            };

            var al = new AbsoluteLayout();

            al.Children.Add(new BoxView { Color = Color.White }, new Rectangle(1, 1, 1, 1), AbsoluteLayoutFlags.All);
            al.Children.Add(_listView, new Rectangle(16, 1, App.Dimensions.Width - 16 * 2, 1), AbsoluteLayoutFlags.YProportional | AbsoluteLayoutFlags.HeightProportional);

            Content = al;
        }

        public event EventHandler<T> ItemSelected;

        protected virtual void OnItemSelected(T e)
        {
            ItemSelected?.Invoke(this, e);
        }
    }
}
