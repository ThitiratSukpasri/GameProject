
namespace ThanaNita.MonoGameTnt
{
    public abstract class TemporalAction : Action
    {
        private float duration;
        protected float Duration => duration;

        private float time;
        private bool finished;
        private bool began;
        private Interpolation interpolation;
        public bool IsFinished() => finished;
        public TemporalAction(float duration, Interpolation interpolation = null)
        {
            this.duration = duration;
            Restart();
            this.interpolation = interpolation ?? Interpolation.Linear;
        }
        public bool Act(float deltaTime)
        {
            if (finished)
                return true;

            if (!began)
            {
                Begin();
                began = true;
            }

            time += deltaTime;
            Update(interpolation.Apply0to1(time, duration));
            finished = time >= duration;
            return finished;
        }

        // ถูกเรียกเมื่อ Act ครั้งแรก ในช่วงที่ยังไม่ finished
        protected abstract void Begin();

        protected abstract void Update(float percent);

        public void Restart()
        {
            time = 0;
            finished = false;
            began = false;
        }
    }
}
