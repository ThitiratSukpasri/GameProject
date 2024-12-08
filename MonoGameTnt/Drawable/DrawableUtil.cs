using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanaNita.MonoGameTnt
{
    public static class DrawableUtil
    {
        public static RectF NormalizeRect(RectF rect, Texture2D texture)
        {
            return NormalizeRect(rect, GetTextureSize(texture));
        }
        public static RectF NormalizeRect(RectF rect, Vector2 overallSize)
        {
            Debug.Assert(overallSize.X > 0 && overallSize.Y > 0);

            return new RectF(
                rect.X / overallSize.X,
                rect.Y / overallSize.Y,
                rect.Width / overallSize.X,
                rect.Height / overallSize.Y);
        }

        public static Vector2 GetTextureSize(Texture2D texture)
        {
            return new Vector2(texture.Width, texture.Height);
        }

    }
}
