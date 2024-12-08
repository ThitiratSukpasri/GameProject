using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanaNita.MonoGameTnt
{
    public class HollowRectActor : Actor
    {
        public float LineWidth { get; }
        public HollowRectActor(Color color, float lineWidth, RectF rect)
        {
            Color = color;
            LineWidth = lineWidth;
            Add(RectangleActor.HorizontalLine(Color.White, rect.Y, rect.X, rect.XMax, lineWidth));
            Add(RectangleActor.HorizontalLine(Color.White, rect.YMax, rect.X, rect.XMax, lineWidth));
            Add(RectangleActor.VerticalLine(Color.White, rect.X, rect.Y, rect.YMax, lineWidth));
            Add(RectangleActor.VerticalLine(Color.White, rect.XMax, rect.Y, rect.YMax, lineWidth));
        }

        public static void AddDebugRect(Actor parent)
        {
            parent.Add(new HollowRectActor(Color.Red, 1, parent.RawRect.CreateExpand(-0.5f)));
        }
    }
}
