
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace ThanaNita.MonoGameTnt
{
    public class MoveByAction : RelativeTemporalAction
    {
        private Actor actor;
        private Vector2 displacement;

        public MoveByAction(float duration, Vector2 displacement, Actor actor)
            : base(duration, null)
        {
            this.actor = actor;
            this.displacement = displacement;
        }

        public static MoveByAction Empty()
        {
            return new MoveByAction(0, Vector2.Zero, null);
        }

        public Vector2 GetNormalizedDirection()
        {
            if(displacement ==  Vector2.Zero)
                return Vector2.Zero;
            
            return Vector2.Normalize(displacement);
        }

        protected override void UpdateRelative(float percentDelta)
        {
            if (actor == null)
                return;

            actor.Position += displacement * percentDelta;
        }
    }
}
