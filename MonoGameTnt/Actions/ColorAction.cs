
using Microsoft.Xna.Framework;
using System;

namespace ThanaNita.MonoGameTnt
{
    // change R, G, B but not Alpha
    public class ColorAction : TemporalAction
    {
        private Actor actor;
        private Color start;
        private Color end;
        public ColorAction(float duration, Color endColor, Actor actor, Interpolation interpolation = null)
            : base(duration, interpolation)
        {
            this.actor = actor;
            this.end = endColor;
        }

        protected override void Begin()
        {
            start = actor.Color;
        }

        protected override void Update(float apply0_1)
        {
            var color = InterpolationUtil.Apply(start, end, apply0_1);
            actor.Color = new Color(color, actor.Color.A);
        }
    }
}
