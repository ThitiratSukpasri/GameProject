
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ThanaNita.MonoGameTnt
{
    public class ImageButton : GenericButton
    {
        public string Str { get { return text.Str; } }
        public Color TextColor { get => text.Color; set { text.Color = value; } }
        public float OutlineWidth { 
            get => frame.LineWidth;
            set => CreateFrame(frame.Color, value); 
        }

        public Color NormalColor { get; set; } = Color.White;
        public Color HighlightColor { get; set; } = new Color(229, 241, 251);
        public Color PressedColor { get; set; } = new Color(204, 228, 247);

        public Color NormalColorLine { get; set; } = Color.Gray;
        public Color HighlightColorLine { get; set; } = Color.Blue;
        public Color PressedColorLine { get; set; } = Color.Black;

        Text text;
        Actor bgImage;
        HollowRectActor frame;

        public ImageButton(string textureName)
            : this(new SpriteActor(TextureCache.Get(textureName)))
        {
        }
        public ImageButton(Texture2D texture)
            : this(new SpriteActor(texture))
        {
        }
        public ImageButton(TextureRegion region) 
            : this(new SpriteActor(region))
        {
        }
        public ImageButton(Actor bgImage) 
            : base(bgImage.RawSize)
        {
            this.bgImage = bgImage;
            CreateFrame(NormalColorLine, lineWidth: 2);
            UpdateCurrentVisual();
        }
        public void SetButtonText(string fontName, float fontSize, Color color, 
                                  string str, Vector2 offset = new Vector2())
        {
            text = new Text(fontName, fontSize, color, str);
            text.Origin = text.RawSize / 2;
            text.Position = this.RawSize / 2 + offset;
            UpdateCurrentVisual();
        }
        public void SetOutlines(float outlineWidth, Color normalColorLine, Color highlightColorLine, Color pressedColorLine)
        {
            NormalColorLine = normalColorLine;
            HighlightColorLine = highlightColorLine;
            PressedColorLine = pressedColorLine;
            OutlineWidth = outlineWidth;
            UpdateCurrentVisual();
        }

        private void CreateFrame(Color color, float lineWidth)
        {
            frame = new HollowRectActor(color, lineWidth, this.RawRect.CreateExpand(-lineWidth / 2));
        }
        protected override void UpdateVisual(ButtonState state)
        {
            if (state == ButtonState.Pressed)
            {
                bgImage.Color = PressedColor;
                frame.Color = PressedColorLine;
            }
            else if (state == ButtonState.Highlight)
            {
                bgImage.Color = HighlightColor;
                frame.Color = HighlightColorLine;
            }
            else
            {
                bgImage.Color = NormalColor;
                frame.Color = NormalColorLine;
            }
        }
        protected override void DrawSelf(DrawTarget target, DrawState state)
        {
            base.DrawSelf(target, state);
            var combine = CombineState(state);
            bgImage.Draw(target, combine);
            frame.Draw(target, combine);
            text?.Draw(target, combine);
        }
    }
}
