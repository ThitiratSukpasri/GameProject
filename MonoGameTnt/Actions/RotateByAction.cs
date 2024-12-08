
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace ThanaNita.MonoGameTnt
{
    public class RotateByAction : RelativeTemporalAction
    {
        private Actor actor;
        private float relativeRotation;

        public RotateByAction(float duration, float relativeRotation, Actor actor)
            : base(duration, null)
        {
            this.actor = actor;
            this.relativeRotation = relativeRotation;
        }

        protected override void UpdateRelative(float percentDelta)
        {
            actor.Rotation += relativeRotation * percentDelta;
        }
    }
}
