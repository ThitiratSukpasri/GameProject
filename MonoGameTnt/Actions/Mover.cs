using Microsoft.Xna.Framework;

namespace ThanaNita.MonoGameTnt
{
    public class Mover : Action
    {
        Actor actor;
        public Vector2 Velocity { get; set; }
        public float Vx { get => Velocity.X; set => Velocity = new Vector2(value, Velocity.Y); }
        public float Vy { get => Velocity.Y; set => Velocity = new Vector2(Velocity.X, value); }
        public float AngularVelocity { get; set; }
        public Mover(Actor actor, Vector2 velocity)
        {
            this.actor = actor;
            this.Velocity = velocity;
        }
        public Mover(Actor actor, float angularVelocity)
        {
            this.actor = actor;
            this.AngularVelocity = angularVelocity;
        }

        public Mover(Actor actor)
        {
            this.actor = actor;
        }

        public virtual bool Act(float deltaTime)
        {
            Vector2 ds = Velocity * deltaTime; // ds = v * dt
            actor.Position += ds;
            actor.Rotation += AngularVelocity * deltaTime;
            return false;
        }

        public void Restart()
        {
            
        }
        public bool IsFinished() => false;

    }
}
