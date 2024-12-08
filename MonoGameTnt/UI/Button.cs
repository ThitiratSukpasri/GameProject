
using Microsoft.Xna.Framework;

namespace ThanaNita.MonoGameTnt
{
    public class Button : GenericButton
    {
        public Color NormalColor { get; set; } = new Color(225, 225, 225);
        public Color HighlightColor { get; set; } = new Color(229, 241, 251);
        public Color PressedColor { get; set; } = new Color(204, 228, 247);

        public Color NormalColorLine { get; set; } = new Color(173, 173, 173);
        public Color HighlightColorLine { get; set; } = new Color(0, 120, 215);
        public Color PressedColorLine { get; set; } = new Color(0, 84, 153);

        public float OutlineWidth { get => outlineWidth; set { outlineWidth = value; UpdateCurrentVisual(); } }
        private float outlineWidth = 2;

        public string Str { get => text.Str; set { text.Str = value; } }
        public Color TextColor { get => text.Color; set { text.Color = value; } }

        Text text;
        RectangleActor background;
        HollowRectActor frame;
        public Button(string fontName, float fontSize, Color color, 
                      string str, Vector2 buttonSize) 
            : base(buttonSize)
        {
            text = new Text(fontName, fontSize, color, str);
            text.Origin = text.RawSize / 2;
            text.Position = this.RawSize / 2;

            UpdateCurrentVisual();
        }
        private void CreateBackgroundRect(Color fillColor, Color outlineColor)
        {
            background = new RectangleActor(fillColor, RawSize);
            frame = new HollowRectActor(outlineColor, OutlineWidth, RawRect.CreateExpand(-OutlineWidth/2));
        }
        protected override void UpdateVisual(ButtonState state)
        {
            if (state == ButtonState.Pressed)
                CreateBackgroundRect(PressedColor, PressedColorLine);
            else if (state == ButtonState.Highlight)
                CreateBackgroundRect(HighlightColor, HighlightColorLine);
            else
                CreateBackgroundRect(NormalColor, NormalColorLine);
        }
        protected override void DrawSelf(DrawTarget target, DrawState state)
        {
            base.DrawSelf(target, state);
            var combine = CombineState(state);
            background.Draw(target, combine);
            frame.Draw(target, combine);
            text.Draw(target, combine);
        }
    }
}
