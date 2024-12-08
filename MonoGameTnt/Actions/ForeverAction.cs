
namespace ThanaNita.MonoGameTnt
{
    public class ForeverAction : Action
    {
        private Action action;

        public ForeverAction(Action action)
        {
            this.action = action;
        }
        public void Restart()
        {
            action.Restart();
        }

        public bool Act(float deltaTime)
        {
            if (IsFinished())
                return true;

            bool finished = action.Act(deltaTime);
            if (finished)
            {
                if(!IsFinished())
                    action.Restart();
            }

            return IsFinished();
        }

        public bool IsFinished()
        {
            return false;
        }
    }
}
