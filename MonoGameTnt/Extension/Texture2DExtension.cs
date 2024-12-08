using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ThanaNita.MonoGameTnt
{
    public static class Texture2DExtension
    {
        public static Vector2 Size(this Texture2D texture)
        {
            return texture.Bounds.Size.ToVector2();
        }
    }
}
