using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykkeColorex.CustomPages
{
    public interface IPopupPage
    {
        Task<T> PopupSelect<T>(List<T> objects, Func<T, string> selector, bool hasDefault, T defaultObject = default(T));
        //Task<List<T>> PopupSelectMultiple<T>(List<T> objects, Func<T, string> selector, List<T> defaultObjs = null);
    }
}
