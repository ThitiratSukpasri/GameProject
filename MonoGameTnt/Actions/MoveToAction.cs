using Microsoft.Xna.Framework;

namespace ThanaNita.MonoGameTnt
{
    public class MoveToAction : TemporalAction
    {
        Actor actor;
        Vector2 startPosition;
        Vector2 endPosition;
        public MoveToAction(float duration, Vector2 endPosition, Actor actor, Interpolation interpolation = null)
            : base(duration, interpolation)
        {
            this.actor = actor;
            this.endPosition = endPosition;
        }

        protected override void Begin()
        {
            startPosition = actor.Position;
        }

        protected override void Update(float percent)
        {
            actor.Position = InterpolationUtil.Apply(startPosition, endPosition, percent);
        }
    }
}
