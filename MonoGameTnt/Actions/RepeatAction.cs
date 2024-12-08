namespace ThanaNita.MonoGameTnt
{
    public class RepeatAction : Action
    {
        private Action action;
        private int count;
        private int current;

        public RepeatAction(int count, Action action)
        {
            this.count = count;
            this.action = action;
            current = 0;
        }
        public void Restart()
        {
            current = 0;
            action.Restart();
        }

        public bool Act(float deltaTime)
        {
            if (IsFinished())
                return true;

            bool finished = action.Act(deltaTime);
            if (finished)
            {
                current++;
                if(!IsFinished())
                    action.Restart();
            }

            return IsFinished();
        }

        public bool IsFinished()
        {
            return current >= count;
        }
    }
}
