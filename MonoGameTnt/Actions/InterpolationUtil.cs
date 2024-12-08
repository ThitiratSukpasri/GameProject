using Microsoft.Xna.Framework;

namespace ThanaNita.MonoGameTnt
{
    public static class InterpolationUtil
    {

        public static float Apply(float start, float end, float apply0to1)
        {
            return start + (end - start) * apply0to1;
        }

        public static Vector2 Apply(Vector2 start, Vector2 end, float apply0to1)
        {
            return start + (end - start) * apply0to1;
        }

        public static Color Apply(Color start, Color end, float apply0to1)
        {
            var r = (int)Apply(start.R, end.R, apply0to1);
            var g = (int)Apply(start.G, end.G, apply0to1);
            var b = (int)Apply(start.B, end.B, apply0to1);
            return new Color(r, g, b);
        }
    }
}