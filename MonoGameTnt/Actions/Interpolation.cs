
using Microsoft.Xna.Framework;
using System;

namespace ThanaNita.MonoGameTnt
{
    // adapted from LibGDX
    public class Interpolation
    {
        public delegate float ApplyDelegate(float completion);
        private ApplyDelegate apply;

        public Interpolation(ApplyDelegate apply)
        {
            this.apply = apply;
        }

        /* completion ratio between 0 and 1. */
        public float Apply(float start, float end, float time, float duration)
        {
            float apply0_1 = Apply0to1(time, duration);
            return InterpolationUtil.Apply(start, end, apply0_1);
        }

        public float Apply0to1(float time, float duration)
        {
            return apply(Completion(time, duration));
        }

        private float Completion(float time, float duration)
        {
            float completion;
            if (duration == 0)
                completion = 1;
            else
                completion = Cutoff(time / duration);
            return completion;
        }

        // cut off value from 0 to 1
        private static float Cutoff(float value)
        {
            return MathF.Max(0, MathF.Min(1, value));
        }

        public static readonly Interpolation Linear = new Interpolation(a => a);
        public static readonly Interpolation Smooth = new Interpolation(a => a * a * (3 - 2 * a));
        public static readonly Interpolation Exp5In = new Interpolation(new ExpIn(2, 5).Apply);
        public static readonly Interpolation Exp5Out = new Interpolation(new ExpOut(2, 5).Apply);

        public class Exp
        {
            protected float value, power, min, scale;

            public Exp(float value, float power)
            {
                this.value = value;
                this.power = power;
                min = MathF.Pow(value, -power);
                scale = 1 / (1 - min);
            }

            public virtual float Apply(float a)
            {
                if (a <= 0.5f) return (MathF.Pow(value, power * (a * 2 - 1)) - min) * scale / 2;
                return (2 - (MathF.Pow(value, -power * (a * 2 - 1)) - min) * scale) / 2;
            }
        };
        public class ExpIn : Exp
        {

            public ExpIn(float value, float power)
                : base(value, power)
            {
            }

            public override float Apply(float a)
            {
                return (MathF.Pow(value, power * (a - 1)) - min) * scale;
            }
        }
        public class ExpOut : Exp
        {

            public ExpOut(float value, float power)
                : base(value, power)
            {
            }

            public override float Apply(float a)
            {
                return 1 - (MathF.Pow(value, -power * a) - min) * scale;
            }
        }
    }
}
