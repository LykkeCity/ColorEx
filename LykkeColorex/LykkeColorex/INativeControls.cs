using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeColorex
{
    public interface INativeControls
    {
        void SetAreaEntrySafe(Rectangle? rect);
        void SetRects(List<Rectangle> list);
        void SetAdjustResize(bool v);
    }
}
