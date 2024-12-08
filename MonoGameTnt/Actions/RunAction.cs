
namespace ThanaNita.MonoGameTnt
{
    public class RunAction : Action
    {
        public delegate void VoidFunc();
        private VoidFunc func;
        private bool finished = false;
        public bool IsFinished() => finished;
        public RunAction(VoidFunc func)
        {
            this.func = func;
        }

        public bool Act(float deltaTime)
        {
            if (finished)
                return true;

            func();
            finished = true;
            return finished;
        }

        public void Restart()
        {
            finished = false;
        }
    }
}
