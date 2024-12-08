using Microsoft.Xna.Framework;

namespace ThanaNita.MonoGameTnt
{
    public static class Alignment
    {
        public static void SetOrigin(Actor actor, Align align)
        {
            Vector2 origin = actor.Origin;
            if (align == Align.Left)
                origin.X = actor.RawRect.X;
            else if (align == Align.Right)
                origin.X = actor.RawRect.XMax;
            else // center
                origin.X = actor.RawRect.CenterPoint.X;

            actor.Origin = origin;
        }

        public static void SetPosition(Actor refActor, Actor set, AlignDirection direction, float delta = 0)
        {
            if (direction == AlignDirection.Down)
                set.Position = new Vector2(refActor.Position.X, refActor.Position.Y + refActor.RawRect.YMax + delta);
// Todo:
//            else if (direction == AlignDirection.Right)
//                set.Position = new Vector2(refActor.Position.X + refActor.RawRect.XMax + delta, refActor.Position.Y);
        }
    }

    public enum Align { Left = -1, Center = 0, Right = 1 };
    public enum AlignDirection { Down }; // todo: , Right };
}
