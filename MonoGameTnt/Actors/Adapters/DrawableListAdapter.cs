using System.Collections.Generic;

namespace ThanaNita.MonoGameTnt
{
    public class DrawableListAdapter : Actor
    {
        List<Drawable> drawables;

        public DrawableListAdapter()
        {
            drawables = new List<Drawable>();
        }

        public void Add(Drawable drawable)
        {
            drawables.Add(drawable);
        }

        protected override void DrawSelf(DrawTarget target, DrawState state)
        {
            base.DrawSelf(target, state);
            for (int i = 0; i < drawables.Count; i++)
                drawables[i].Draw(target, state);
        }
    }
}
