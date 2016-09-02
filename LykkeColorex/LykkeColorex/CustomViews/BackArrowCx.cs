using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeColorex.CustomViews
{
    public class BackArrowCx : AbsoluteLayout
    {
        private Image _buttonNormal;
        private Image _buttonClicked;

        public BackArrowCx(bool fake)
        {
            _buttonNormal = new Image { Source = ImageSource.FromFile("backScreenIcn.png"), Aspect = Aspect.AspectFit };
            //_buttonClicked = new Image { Source = ImageSource.FromFile("backScreenHoverIcn.png"), Aspect = Aspect.AspectFill };
            
            //Children.Add(_buttonClicked, new Rectangle(0, 0, 20, AutoSize));
            Children.Add(_buttonNormal, new Rectangle(0, 0, 20, AutoSize));
            if (!fake)
            {
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped +=
                    async delegate
                    {
                        //_buttonNormal.IsVisible = false;
                        _buttonNormal.Opacity = 0.3;
                        Clicked?.Invoke(this, new EventArgs());
                        await Task.Delay(100);
                        _buttonNormal.Opacity = 1;
                        //_buttonNormal.IsVisible = true;
                    };

                this.GestureRecognizers.Add(tapGestureRecognizer);
            }
        }

        public event EventHandler Clicked;


    }
}
