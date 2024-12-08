
using Microsoft.Xna.Framework;
using System;

namespace ThanaNita.MonoGameTnt
{
    public class AlphaAction : TemporalAction
    {
        private Actor actor;
        private int startAlpha;
        private int endAlpha;
        public AlphaAction(float duration, int endAlpha, Actor actor, Interpolation interpolation = null)
            : base(duration, interpolation)
        {
            this.actor = actor;
            this.endAlpha = endAlpha;
        }

        protected override void Begin()
        {
            startAlpha = actor.Color.A;
        }

        protected override void Update(float apply0_1)
        {
            var alpha = (int)InterpolationUtil.Apply(startAlpha, endAlpha, apply0_1);
            actor.Color = new Color(actor.Color, alpha);
        }
    }
}
