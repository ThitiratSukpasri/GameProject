using Microsoft.Xna.Framework;

namespace ThanaNita.MonoGameTnt
{
    public class OriginAxis : Actor
    {
        public OriginAxis(float length)
        {
            Color = Color.Blue;

            float width = 2;
            Add(RectangleActor.HorizontalLine(Color.White, 0, 0, length, width));
            Add(RectangleActor.VerticalLine(Color.White, 0, 0, length, width));
            Add(new RectangleActor(Color.White, new RectF(-5, -5, 10, 10)));
        }
    }
}
