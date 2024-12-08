
namespace ThanaNita.MonoGameTnt
{
    public class AnimationStates : Action
    {

        private int last = -1;
        private Animation[] animations;

        public AnimationStates(params Animation[] animations)
        {
            this.animations = animations;
        }

        public Animation GetAnimation(int index)
        {
            return animations[index];
        }
        public void Restart()
        {
            if (last == -1)
                return;
            GetAnimation(last).Restart();
        }
        public void Pause()
        {
            if (last == -1)
                return;
            GetAnimation(last).Pause();
        }

        public void Animate(int index)
        {
            GetAnimation(index).Play();
            if (index == last)
                return;

            for (int i = 0; i < animations.Length; ++i)
                GetAnimation(i).Running = false;

            var animation = GetAnimation(index);
            animation.Play();
            animation.Restart();
            last = index;
        }

        public bool Act(float delta)
        {
            if (last == -1)
                return false;

            var ani = GetAnimation(last);
            ani.Act(delta);
            return false;
        }
        public bool IsFinished() => false;

    }
}
