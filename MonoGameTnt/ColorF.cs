using Microsoft.Xna.Framework;

namespace ThanaNita.MonoGameTnt
{
    public readonly struct ColorF
    {
        public float RedF { get; }
        public float GreenF { get; }
        public float BlueF { get; }
        public float AlphaF { get; }

        public ColorF(float redF, float greenF, float blueF, float alphaF)
        {
            RedF = redF;
            GreenF = greenF;
            BlueF = blueF;
            AlphaF = alphaF;
        }

        public ColorF(Color color)
        {
            RedF = color.R / 255.0f;
            GreenF = color.G / 255.0f;
            BlueF = color.B / 255.0f;
            AlphaF = color.A / 255.0f;
        }

        public static explicit operator ColorF(Color color)
        {
            return new ColorF(color);
        }

        public Color ToColor()
        {
            return new Color(RedF, GreenF, BlueF, AlphaF);
        }

        public ColorF Combine(ColorF other)
        {
            return new ColorF(
                RedF * other.RedF,
                GreenF * other.GreenF,
                BlueF * other.BlueF,
                AlphaF * other.AlphaF
                );
        }

        public static ColorF operator *(ColorF left, ColorF right)
        {
            return left.Combine(right);
        }

        public ColorF ToPremultiplyAlpha()
        {
            if (AlphaF == 0)
                return new ColorF(0, 0, 0, 0);

            return new ColorF(
                RedF * AlphaF,
                GreenF * AlphaF,
                BlueF * AlphaF,
                AlphaF
                );
        }
    }
}
