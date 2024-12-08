using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanaNita.MonoGameTnt
{
    public static class ColorExtension
    {
        public static Color WithNewAlpha(this Color color, int alpha)
        {
            return new Color(color, alpha);
        }
    }
}
