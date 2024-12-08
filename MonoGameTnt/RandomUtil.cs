using Microsoft.Xna.Framework;
using System;

namespace ThanaNita.MonoGameTnt
{
    public static class RandomUtil
    {
        private static Random random = new Random();
        public static Random RandomObj { get => random; }
        public static void ChangeRandomObj(Random random)
        {
            RandomUtil.random = random;
        }
        public static int Next(int maxValue)
        {
            return random.Next(maxValue);
        }

        public static int Next(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }
        public static float NextSingle()
        {
            return random.NextSingle();
        }
        public static float NextSingle(float exclusiveMax)
        {
            return random.NextSingle() * exclusiveMax;
        }
        public static float NextSingle(float min, float exclusiveMax)
        {
            return min + random.NextSingle() * (exclusiveMax-min);
        }

        public static Vector2 Direction()
        {
            float theta = NextSingle() * 2 * MathF.PI;
            return new Vector2(MathF.Cos(theta), MathF.Sin(theta));
        }

        public static Vector2 Position(Vector2 size)
        {
            return new Vector2 (NextSingle(size.X), NextSingle(size.Y));
        }

        public static Color Color()
        {
            return new Color(Next(256), Next(256), Next(256));
        }

        public static Color LightColor()
        {
            return new Color(128 + Next(128), 128 + Next(128), 128 + Next(128));
        }
    }
}
