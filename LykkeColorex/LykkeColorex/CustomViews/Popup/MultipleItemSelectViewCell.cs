using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeColorex.CustomViews.Popup
{
    public class MultipleItemSelectViewCell : ViewCell
    {
        private readonly Label _title;
        private readonly Image _voidImage, _selectedImage;


        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(MultipleItemSelectViewCell), "Title");
        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value);}
        }

        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(MultipleItemSelectViewCell), false);
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public MultipleItemSelectViewCell()
        {
            var layout = new AbsoluteLayout();

            _title = new LabelEx
            {
                FontSize = 15,
                TextColor = Color.FromRgb(63, 77, 96),
                VerticalTextAlignment = TextAlignment.Center

            };
            layout.Children.Add(_title, new Rectangle(36, 0.5, 150, 24), AbsoluteLayoutFlags.YProportional);

            _selectedImage = new Image { Source = ImageSource.FromFile("popupMultipleSelected"), Aspect = Aspect.AspectFit, IsVisible = IsSelected};
            _voidImage = new Image { Source = ImageSource.FromFile("popupSingleVoid"), Aspect = Aspect.AspectFit };

            layout.Children.Add(_voidImage, new Rectangle(0, 14, 24, 24));
            layout.Children.Add(_selectedImage, new Rectangle(0, 14, 24, 24));

            _selectedImage.SetBinding(Image.IsVisibleProperty, new Binding("IsSelected"));
            _title.SetBinding(Label.TextProperty, new Binding("Title"));
            
            View = layout;
        }
    }
}
