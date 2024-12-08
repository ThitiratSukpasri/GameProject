using Microsoft.Xna.Framework;

namespace ThanaNita.MonoGameTnt
{
    public static class Actions
    {
        public static AlphaAction FadeOut(float duration, Actor actor, Interpolation interpolation = null)
        {
            return new AlphaAction(duration, 0, actor, interpolation);
        }
        public static AlphaAction FadeIn(float duration, Actor actor, Interpolation interpolation = null)
        {
            return new AlphaAction(duration, 255, actor, interpolation);
        }
        public static ForeverAction Forever(params Action[] actions)
        {
            return new ForeverAction(new SequenceAction(actions));
        }
        public static RepeatAction Repeat(int count, Action action)
        {
            return new RepeatAction(count, action);
        }
        public static RepeatAction Repeat(int count, params Action[] actions)
        {
            return new RepeatAction(count, new SequenceAction(actions));
        }

        // Convenient Methods
        public static DelayAction Delay(float duration) 
        { 
            return new DelayAction(duration); 
        }
        public static MoveToAction MoveTo(float duration, Vector2 endPosition, Actor actor, Interpolation interpolation = null)
        {
            return new MoveToAction(duration, endPosition, actor, interpolation);
        }
        public static MoveByAction MoveBy(float duration, Vector2 displacement, Actor actor)
        {
            return new MoveByAction(duration, displacement, actor);
        }
    }
}
