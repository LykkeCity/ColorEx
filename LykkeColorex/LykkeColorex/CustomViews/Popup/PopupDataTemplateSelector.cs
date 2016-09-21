using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeColorex.CustomViews.Popup
{
    public class PopupDataTemplateSelector : DataTemplateSelector
    {

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return new DataTemplate(() => new ViewCell());
        }
    }
}
