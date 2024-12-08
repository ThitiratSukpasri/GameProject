using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace ThanaNita.MonoGameTnt
{
    public class DirectionAnimator : Action
    {

        private AnimationStates states;
        int[] animates;

        // animates : index numbers of (stay, left, right, up, down) animations
        public DirectionAnimator(AnimationStates states, int[] animates)
        {
            Debug.Assert(animates != null && animates.Length == 5);
            this.states = states;
            this.animates = animates;
        }

        public DirectionAnimator(AnimationStates states)
            : this(states, new int[] { 0, 1, 2, 3, 4 })
        {
            
        }

        public void Restart()
        {
        }
        public bool IsFinished() => false;

        public bool Act(float delta_t)
        {

            Vector2 direction = DirectionKey.Direction;
            if (direction.X < 0)		// left
                states.Animate(animates[1]);
            else if (direction.X > 0)	// right
                states.Animate(animates[2]);
            else if (direction.Y > 0) 	// up
                states.Animate(animates[3]);
            else if (direction.Y < 0) 	// down
                states.Animate(animates[4]);
            else
                states.Animate(animates[0]);

            states.Act(delta_t);

            return false;
        }
    }
}
