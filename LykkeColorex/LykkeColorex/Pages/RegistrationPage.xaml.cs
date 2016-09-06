using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeColorex.Constants.Layouts;
using LykkeColorex.CustomViews;
using Xamarin.Forms;

namespace LykkeColorex.Pages
{
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            BackgroundColor = Color.White;

            var al = new AbsoluteLayout() {HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill};

            var rb = new RegistrationProgressBar((int) (App.Dimensions.Width - 2*LoginPageLayout.Padding), 5);

            al.Children.Add(rb, new Rectangle(LoginPageLayout.Padding, 85, AbsoluteLayout.AutoSize, 3));

            var button = new Button { Text = "asdf" };
            button.Clicked += (sender, args) =>
            {
                rb.Next();
            };

            var button2 = new Button { Text = "asdf" };
            button2.Clicked += (sender, args) =>
            {
                rb.Previous();
            };

            al.Children.Add(button, new Rectangle(200, 200, 50, 50));
            al.Children.Add(button2, new Rectangle(100, 200, 50, 50));

            Content = al;
        }
    }
}
