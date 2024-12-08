using Microsoft.Xna.Framework;

namespace ThanaNita.MonoGameTnt
{
    public class CrossHair : Actor
    {
        public CrossHair(float x, float y)
            : this(new Vector2(x, y))
        {
        }
        public CrossHair(Vector2 position)
        {
            Color = Color.Red;
            Position = position;

            float halfLen = 10;
            float width = 2;
            Add(RectangleActor.HorizontalLine(Color.White, 0, -halfLen, halfLen, width));
            Add(RectangleActor.VerticalLine(Color.White, 0, -halfLen, halfLen, width));
        }
    }
}
