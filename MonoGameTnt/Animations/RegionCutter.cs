using Microsoft.Xna.Framework.Graphics;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace ThanaNita.MonoGameTnt
{
    public class RegionCutter
    {
        public static TextureRegion[][] Cut(Texture2D texture, Vector2 size)
        {
            return Cut(texture, (int)size.X, (int)size.Y);
        }
        public static TextureRegion[][] Cut(Texture2D texture, int width, int height)
        {
            int countX = texture.Width / width;
            int countY = texture.Height / height;
            return Cut(texture, width, height, countX, countY);
        }
        public static TextureRegion[][] Cut(Texture2D texture, Vector2 size, int countX, int countY,
                                            Vector2 offset = default, Vector2 spacing = default)
        {
            return Cut(texture, (int)size.X, (int)size.Y, countX, countY, offset, spacing);
        }
        public static TextureRegion[][] Cut(Texture2D texture, int width, int height, int countX, int countY, 
                                            Vector2 offset = default, Vector2 spacing = default)
        { 
            TextureRegion[][] arr = new TextureRegion[countY][];
            for (int y = 0; y < countY; y++)
            {
                arr[y] = new TextureRegion[countX];
                for (int x = 0; x < countX; x++)
                {
                    arr[y][x] = new TextureRegion(texture,
                        new RectF(  offset.X + x * width + (x-1)*spacing.X, 
                                    offset.Y + y * height + (y-1)*spacing.Y, 
                                    width, height));
                }
            }

            return arr;
        }

        public static void Print(TextureRegion[][] arr2d)
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < arr2d.Length; y++)
            {
                for (int x = 0; x < arr2d[y].Length; x++)
                    sb.Append($"[{y},{x}:{arr2d[y][x]}], ");
            }

            Debug.WriteLine(sb.ToString());
        }
    }
}
