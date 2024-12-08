using System.Collections.Generic;

namespace ThanaNita.MonoGameTnt
{
    public abstract class CoroutineAction : Action
    {
        IEnumerator<Action> enumerator = null;
        Action action = null;
        bool finished = false;
        public bool Act(float deltaTime)
        {
            if (enumerator == null) // call only one time
                enumerator = Coroutine();

            if (action == null)
            {
                if (!enumerator.MoveNext())
                {
                    finished = true;
                    return finished; // finished
                }

                action = enumerator.Current;
            }

            if (action != null) // in case of action == null, just do nothing & skip one frame.
            {
                var result = action.Act(deltaTime);
                if (result == true) // finish
                    action = null;
            }
            return false;
        }

        public abstract IEnumerator<Action> Coroutine();

        public virtual void Restart()
        {
            enumerator = null;
            action = null;
            finished = false;
        }
        public bool IsFinished() => finished;
    }
}
