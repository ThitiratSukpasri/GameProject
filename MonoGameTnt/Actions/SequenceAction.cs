using System;

namespace ThanaNita.MonoGameTnt
{
    public class SequenceAction : Action
    {
        private Action[] actions;
        private int current = 0;

        public SequenceAction(params Action[] actions)
        {
            this.actions = actions;
        }

        public void Add(Action task)
        {
            Action[] newActions = new Action[actions.Length + 1];
            Array.Copy(actions, newActions, actions.Length);
            newActions[actions.Length] = task;
            actions = newActions;
        }

        public void Restart()
        {
            current = 0;
            for(int i=0; i<actions.Length; i++)
                actions[i].Restart();
        }

        public bool Act(float deltaTime)
        {
            if (IsFinished())
                return true;

            bool finished = actions[current].Act(deltaTime);
            if (finished)
                current++;

            return IsFinished();
        }

        public bool IsFinished()
        {
            return current >= actions.Length;
        }
    }
}
