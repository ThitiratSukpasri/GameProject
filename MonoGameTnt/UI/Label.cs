using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanaNita.MonoGameTnt
{
    public class Label : Actor
    {
        Text text;

        public string Text { get => text.Str; set => text.Str = value; }
        public float FontSize {get => text.FontSize; set => text.FontSize = value; }
        public override RectF RawRect => text.RawRect;

        public Label(string fontName, float fontSize, Color color, string text)
        {
            this.text = new Text(fontName, fontSize, Color.White, text);
            this.Color = color;
        }
        public override void Draw(DrawTarget target, DrawState state)
        {
            base.Draw(target, state);
            text.Draw(target, CombineState(state));
        }
    }
}
