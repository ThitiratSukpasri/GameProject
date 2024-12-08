
namespace ThanaNita.MonoGameTnt
{
    public class ParallelAction : Action
    {
        private Action[] actions;
        private bool[] finished;

        public ParallelAction(params Action[] actions)
        {
            this.actions = actions;
            finished = new bool[actions.Length];
        }

        public bool Act(float deltaTime)
        {
            if (IsFinished())
                return true;

            for (int i = 0; i < actions.Length; i++)
            {
                var result = actions[i].Act(deltaTime);
                if(result == true)
                    finished[i] = true;
            }

            return IsFinished();
        }

        public bool IsFinished()
        {
            for (int i = 0; i < finished.Length; i++)
                if(!finished[i])
                    return false;
            return true;
        }

        public void Restart()
        {
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].Restart();
                finished[i] = false;
            }
        }
    }
}
