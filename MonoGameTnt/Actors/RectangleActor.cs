using Microsoft.Xna.Framework;
using System;

namespace ThanaNita.MonoGameTnt
{
    public class RectangleActor : Actor
    {
        private RectangleDrawable rect;
/*        public RectF BoundingBox 
        { 
            get => GetMatrix().TransformRectAABB(RawRect); 
        }*/
        public override RectF RawRect => rect.BoundingBox;
        public RectangleActor(Color color, Vector2 size)
                : this(color, Vector2.Zero, size)
        {
        }
        public RectangleActor(Color color, Vector2 position, Vector2 size)
                : base()
        {
            this.Color = color;
            this.Position = position;
            rect = new RectangleDrawable(Color.White, size);
        }
        public RectangleActor(Color color, RectF rect)
                : this(color, rect.Position, rect.Size)
        {
        }
        protected override void DrawSelf(DrawTarget target, DrawState state)
        {
            base.DrawSelf(target, state);
            rect.Draw(target, CombineState(state));
        }

        public static RectangleActor HorizontalLine(Color color, float y, float x0, float x1, float width)
        {
            float half = width / 2;
            return new RectangleActor(color, 
                new RectF(x0 - half, y - half, x1 - x0 + width, width));
        }
        public static RectangleActor VerticalLine(Color color, float x, float y0, float y1, float width)
        {
            float half = width / 2;
            return new RectangleActor(color,
                new RectF(x - half, y0 - half, width, y1 - y0 + width));
        }
    }
}
