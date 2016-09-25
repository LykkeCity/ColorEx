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

        public BackArrowCx(bool fake)
        {
            _buttonNormal = new Image { Source = ImageSource.FromFile("backScreenIcn.png"), Aspect = Aspect.AspectFit };

            Children.Add(_buttonNormal, new Rectangle(0, 0, 20, AutoSize));
            if (!fake)
            {
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped +=
                    async delegate
                    {
                        _buttonNormal.Opacity = 0.3;
                        Clicked?.Invoke(this, new EventArgs());
                        await Task.Delay(100);
                        _buttonNormal.Opacity = 1;
                    };

                this.GestureRecognizers.Add(tapGestureRecognizer);
            }
        }

        public event EventHandler Clicked;

    }
}
