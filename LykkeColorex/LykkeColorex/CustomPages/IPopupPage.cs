using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykkeColorex.CustomPages
{
    public interface IPopupPage
    {
        Task<List<T>> PopupSelect<T>(bool allowMultiple, string title, List<T> objects, Func<T, string> selector, List<T> defaultObjects);
        Task<List<T>> PopupSelect<T>(bool allowMultiple, string title, List<T> objects, Func<T, Tuple<string, string>> selector, List<T> defaultObjects);

    }
}
