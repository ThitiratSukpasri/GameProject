
using Microsoft.Xna.Framework;
using System;

namespace ThanaNita.MonoGameTnt
{
    // change R, G, B but not Alpha
    public class MorphColorAction : TemporalAction
    {
        private MorphActor actor;
        private Color start;
        private Color end;
        private int index;
        public MorphColorAction(float duration, Color endColor, MorphActor actor, int index, Interpolation interpolation = null)
            : base(duration, interpolation)
        {
            this.actor = actor;
            this.end = endColor;
            this.index = index;
        }

        protected override void Begin()
        {
            start = actor.GetColor(index);
        }

        protected override void Update(float apply0to1)
        {
            var color = InterpolationUtil.Apply(start, end, apply0to1);
            actor.SetColor(index, new Color(color, actor.Color.A));
        }
    }
}
