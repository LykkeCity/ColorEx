using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeColorex.CustomViews.Popup;
using LykkeColorex.CustomViews.RegistrationSteps;
using Xamarin.Forms;

namespace LykkeColorex
{
    public class ContentPageEx : ContentPage
    {
        private ListView _popupListView;
        private BoxView Shader = new BoxView { Color = Color.FromRgb(36, 50, 67), Opacity = 0 };
        private TapGestureRecognizer ShaderTapGestureRecognizer;
        private PopupCx Popup = new PopupCx();

        public ContentPageEx()
        {
            ShaderTapGestureRecognizer = new TapGestureRecognizer();
            Shader.GestureRecognizers.Add(ShaderTapGestureRecognizer);
        }


        protected Task ShowPopup()
        {
            try
            {
                var al = Content as AbsoluteLayout;

                if (al == null)
                    return Task.Run(() => { });

                if (al.Children.Contains(Popup))
                    al.Children.Remove(Popup);

                if (al.Children.Contains(Shader))
                    al.Children.Remove(Shader);

                Shader.Opacity = 0;
                
                ShaderTapGestureRecognizer.Tapped += TapGestureRecognizerOnTapped;


                al.Children.Add(Shader, new Rectangle(-1, -1, Content.Width+1, Content.Height+1));
                al.Children.Add(Popup, new Rectangle(0, Content.Height, Content.Width, 0));

                var a1 = Shader.FadeTo(0.2, 300);
                var a2 =
                    Popup.LayoutTo(
                        new Rectangle(0, Content.Height / 3, Content.Width, Content.Height - Content.Height / 3), 300,
                        Easing.SpringOut);

                Popup.ItemSelected += PopupOnItemSelected;

                return Task.WhenAll(a1, a2);
            }
            catch (Exception e)
            {
                var a = 234;
            }

            return null;
        }

        private async void PopupOnItemSelected(object sender, EventArgs eventArgs)
        {
            await HidePopup();
            ShaderTapGestureRecognizer.Tapped -= TapGestureRecognizerOnTapped;
            Popup.ItemSelected -= PopupOnItemSelected;
        }

        private async void TapGestureRecognizerOnTapped(object sender, EventArgs eventArgs)
        {
            await HidePopup();
            ShaderTapGestureRecognizer.Tapped -= TapGestureRecognizerOnTapped;
            Popup.ItemSelected -= PopupOnItemSelected;
        }

        protected async Task HidePopup()
        {
            try
            {
                var a1 = Shader.FadeTo(0, 200);
                await Popup.LayoutTo(new Rectangle(0, Content.Height, Content.Width, 0), 200, Easing.CubicOut);
                //Task.WaitAll(a1, a2);

                var al = Content as AbsoluteLayout;
                Debug.WriteLine("ASDFASDF!!");
                if (al == null)
                    return;

                if (al.Children.Contains(Popup))
                    al.Children.Remove(Popup);

                if (al.Children.Contains(Shader))
                    al.Children.Remove(Shader);
                
            }
            catch (Exception e)
            {
                var a = 234;
            }
        }
    }
}
