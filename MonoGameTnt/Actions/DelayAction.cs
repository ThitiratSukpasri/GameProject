
namespace ThanaNita.MonoGameTnt
{
    public class DelayAction : Action
    {
        private float duration;
        private bool finished;
        private float time;
        public float Duration { get => duration; set => duration = value; }
        public bool IsFinished() => finished;

        public DelayAction(float duration)
        {
            this.duration = duration;
        }

        public bool Act(float deltaTime)
        {
            if (finished)
                return true;

            time += deltaTime;
            if(time >= duration)
                finished = true;
            return finished;
        }

        public void Restart()
        {
            time = 0.0f;
            finished = false;
        }
    }
}
