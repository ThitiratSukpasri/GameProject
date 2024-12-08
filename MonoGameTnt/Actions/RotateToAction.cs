using Microsoft.Xna.Framework;

namespace ThanaNita.MonoGameTnt
{
    public class RotateToAction : TemporalAction
    {
        Actor actor;
        float start;
        float end;
        public RotateToAction(float duration, float endRotation, Actor actor, Interpolation interpolation = null)
            : base(duration, interpolation)
        {
            this.actor = actor;
            this.end = endRotation;
        }

        protected override void Begin()
        {
            start = actor.Rotation;
        }

        protected override void Update(float apply0_1)
        {
            actor.Rotation = InterpolationUtil.Apply(start, end, apply0_1);
        }
    }
}
