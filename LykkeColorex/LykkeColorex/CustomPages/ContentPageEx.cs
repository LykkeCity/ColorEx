using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using LykkeColorex.CustomPages;
using LykkeColorex.CustomViews.Popup;
using LykkeColorex.CustomViews.RegistrationSteps;
using Xamarin.Forms;

namespace LykkeColorex.CustomPages
{
    public class ContentPageEx : ContentPage, IPopupPage
    {

        public ContentPageEx()
        {
            
        }

        public async Task HidePopup<T>(PopupCx<T> popup, BoxView shader)
        {
            try
            {
                var a1 = shader.FadeTo(0, 300);
                var a2 = popup.LayoutTo(new Rectangle(0, Content.Height, Content.Width, 0), 300, Easing.CubicOut);
                await Task.WhenAll(a1, a2);
                
                var al = Content as AbsoluteLayout;
                Debug.WriteLine("ASDFASDF!!");
                if (al == null)
                    return;

                if (al.Children.Contains(popup))
                    al.Children.Remove(popup);

                if (al.Children.Contains(shader))
                    al.Children.Remove(shader);
                
            }
            catch (Exception e)
            {
                var a = 234;
            }
        }

        public async Task<T> PopupSelect<T>(List<T> objects, Func<T, string> selector, bool hasDefault, T defaultObject = default(T))
        {
            var al = Content as AbsoluteLayout;

            if (al == null)
                return await Task.Run(() => default(T));
            
            var popup = new PopupCx<T>(objects, selector, hasDefault, defaultObject);
            var shader = new BoxView {Color = Color.FromRgb(36, 50, 67), Opacity = 0};

            al.Children.Add(shader, new Rectangle(-1, -1, Content.Width + 1, Content.Height + 1));
            al.Children.Add(popup, new Rectangle(0, Content.Height, Content.Width, 0));

            var tcs = new TaskCompletionSource<T>();


            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (sender, e) =>
            {
                await HidePopup(popup, shader);
                tcs.SetResult(defaultObject);
            };
            shader.GestureRecognizers.Add(tapGestureRecognizer);
            
            popup.ItemSelected += async (sender, e) =>
            {
                //await HidePopup(popup, shader);
                //tcs.SetResult(e);
            };


            var a1 = shader.FadeTo(0.2, 200);
            var a2 =
                popup.LayoutTo(
                    new Rectangle(0, Content.Height / 3, Content.Width, Content.Height - Content.Height / 3), 200,
                    Easing.SpringOut);


            await Task.WhenAll(a2);


            return await tcs.Task;
        }
    }
}
