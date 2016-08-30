using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeColorex
{
    public static class ViewExtensions
    {

        public static Rectangle GetRealPosition(this View v)
        {
            return new Rectangle(v.X + v.TranslationX, v.Y + v.TranslationY, v.Width, v.Height);
        }
    }
}
