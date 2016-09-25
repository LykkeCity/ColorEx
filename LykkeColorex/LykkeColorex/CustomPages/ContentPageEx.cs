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

        public async Task HidePopup<T>(SelectPopupCx<T> selectPopup, BoxView shader)
        {
            try
            {
                Debug.WriteLine("HidePopup running");
                var a1 = shader.FadeTo(0, 300);
                var a2 = selectPopup.LayoutTo(new Rectangle(0, Content.Height, Content.Width, 0), 300, Easing.CubicOut);
                //var a2 = selectPopup.TranslateTo(0, 0 , 300, Easing.CubicOut);

                await Task.WhenAll(a1, a2);

                var al = Content as AbsoluteLayout;
                Debug.WriteLine("ASDFASDF!!");
                if (al == null)
                    return;

                al.Children.Remove(selectPopup);

                al.Children.Remove(shader);

            }
            catch (Exception e)
            {
                var a = 234;
            }
        }

        public async Task<T> PopupSelect<T>(string title, List<T> objects, Func<T, string> selector, bool hasDefault, T defaultObject = default(T))
        {
            try
            {
                var al = Content as AbsoluteLayout;

                if (al == null)
                    return await Task.Run(() => default(T));

                var popup = new SelectPopupCx<T>(title, objects, selector, hasDefault, defaultObject);
                var shader = new BoxView { Color = Color.FromRgb(36, 50, 67), Opacity = 0 };

                al.Children.Add(shader, new Rectangle(-1, -1, Content.Width + 1, Content.Height + 1));
                al.Children.Add(popup, new Rectangle(0, Content.Height, Content.Width, Content.Height - Content.Height / 3));

                var tcs = new TaskCompletionSource<T>();


                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += async (sender, e) =>
                {
                    await HidePopup(popup, shader);
                    var a = default(T);
                    tcs.SetResult(a);
                };
                shader.GestureRecognizers.Add(tapGestureRecognizer);

                popup.ItemSelected += async (sender, e) =>
                {
                    await HidePopup(popup, shader);
                    tcs.SetResult(e);
                };


                var a1 = shader.FadeTo(0.2, 300);
                var a2 =
                    popup.LayoutTo(
                        new Rectangle(0, Content.Height / 3, Content.Width, Content.Height - Content.Height / 3), 300,
                        Easing.SpringOut);


                await Task.WhenAll(a1, a2);


                return await tcs.Task;
            }
            catch (Exception ex)
            {
                var a = 234;
            }
            return await Task.Run(() => default(T));
        }

        public async Task<T> PopupSelect<T>(string title, List<T> objects, Func<T, Tuple<string, string>> selector, bool hasDefault, T defaultObject = default(T))
        {
            try
            {
                var al = Content as AbsoluteLayout;

                if (al == null)
                    return await Task.Run(() => default(T));

                var popup = new SelectPopupCx<T>(title, objects, selector, hasDefault, defaultObject);
                var shader = new BoxView { Color = Color.FromRgb(36, 50, 67), Opacity = 0 };

                al.Children.Add(shader, new Rectangle(-1, -1, Content.Width + 1, Content.Height + 1));
                al.Children.Add(popup, new Rectangle(0, Content.Height, Content.Width, Content.Height - Content.Height / 3));

                var tcs = new TaskCompletionSource<T>();

                bool dismissionBegan = false;
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += async (sender, e) =>
                {
                    if (!dismissionBegan)
                    {
                        dismissionBegan = true;

                        await HidePopup(popup, shader);
                        tcs.SetResult(default(T));
                    }
                };
                shader.GestureRecognizers.Add(tapGestureRecognizer);
                
                popup.ItemSelected += async (sender, e) =>
                {
                    if (!dismissionBegan)
                    {
                        dismissionBegan = true;

                        await HidePopup(popup, shader);
                        tcs.SetResult(e);
                    }
                };


                var a1 = shader.FadeTo(0.2, 400);
                var a2 = popup.LayoutTo( new Rectangle(0, Content.Height / 3, popup.Bounds.Width, popup.Bounds.Height), 400, Easing.SpringOut);
                //var a2 = popup.TranslateTo(0, - Content.Height + Content.Height/3, 300, Easing.CubicOut);

                await Task.WhenAll(a1, a2);


                return await tcs.Task;
            }
            catch (Exception ex)
            {
                var a = 234;
            }
            return await Task.Run(() => default(T));
        }
    }
}
