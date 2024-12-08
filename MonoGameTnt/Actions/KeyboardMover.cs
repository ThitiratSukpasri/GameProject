using Microsoft.Xna.Framework;

namespace ThanaNita.MonoGameTnt
{
    public class KeyboardMover : Mover
    {
        private float speed;
        public KeyboardMover(Actor actor, float speed)
            : base(actor, new Vector2())
        {
            this.speed = speed;
        }

        public override bool Act(float deltaTime)
        {
            this.Velocity = DirectionKey.Direction * speed;

            base.Act(deltaTime);
            return false;
        }
    }
}
