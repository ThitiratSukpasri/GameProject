namespace ThanaNita.MonoGameTnt
{
    public class DrawableAdapter : Actor
    {
        protected Drawable drawable;

        public DrawableAdapter(Drawable drawable)
        {
            this.drawable = drawable;
        }

        public void Set(Drawable drawable)
        {
            this.drawable = drawable; 
        }

        protected override void DrawSelf(DrawTarget target, DrawState state)
        {
            base.DrawSelf(target, state);

            if (drawable == null)
                return;
            drawable.Draw(target, CombineState(state));
        }
    }
}
