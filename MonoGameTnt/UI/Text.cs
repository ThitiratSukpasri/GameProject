using FontStashSharp;
using FontStashSharp.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;


namespace ThanaNita.MonoGameTnt
{
    // Wrap the FontStashShape
    public class Text : Actor
    {
        private GraphicsDevice device;
        private FontSystem fontSystem;
        private FontStashRenderer renderer;
        private bool needUpdate = true;

        private SpriteFontBase font;
        private float fontSize;
                                // set this.Color will clear colorArray to null
        private Color[] colorArray = null; // if null, will use the (single) color
        private string str;
        private FontSystemEffect effect = FontSystemEffect.None;
        private int effectAmount = 1;
        private float characterSpacing = 0;

        private float baseLineHeight;
        private Vector2 sizeInPixel;
        public float FontSize
        {
            get => fontSize;
            set { fontSize = value; NeedUpdate(); }
        }
        public override Color Color
        {
            get => base.Color;
            set
            {
                base.Color = value;
                colorArray = null;
                NeedUpdate();
            }
        }
        public Color[] ColorArray
        {
            set
            {
                Debug.Assert(value != null && value.Length > 0);
                colorArray = (Color[])value.Clone();
            }
        }
        public string Str
        {
            get => str;
            set { str = value; NeedUpdate(); }
        }
        public FontSystemEffect Effect 
        { 
            get => effect;
            set { effect = value; NeedUpdate(); } 
        }
        public int EffectAmount 
        { 
            get => effectAmount; 
            set { effectAmount = value; NeedUpdate(); } 
        }
        public float CharacterSpacing
        {
            get => characterSpacing;
            set { characterSpacing = value; NeedUpdate(); }
        }

        public float LineHeight
        {
            get { Update(); return font.LineHeight; }
        }
        public float BaseLineHeight 
        { 
            get { Update(); return baseLineHeight; } 
        }
        public float Width
        {
            get { Update(); return sizeInPixel.X; }
        }

// ไม่ค่อยจำเป็น        public float BaseLineHeightBelow { get { return LineHeight - BaseLineHeight; } }
/* ใช้ line height เป็นหลักดีกว่า เพราะตัวนี้ ค่า Y ด้านบนวัดจากขอบบนสุดซึ่งที่ว่างเยอะ แต่ด้านล่างวัดถึงสระอู ซึ่งดูไม่สมดุลระหว่างที่ว่างบนล่าง
        public Vector2 MeasuredSize
        {
            get { Update(); return sizeInPixel; }
        }*/
        public override RectF RawRect
        {
            get { Update(); return new RectF(Vector2.Zero, new Vector2(Width, LineHeight)); }
        }

        public Text(string fontName, float fontSize, Color color, string str)
            : this(GlobalGraphicsDevice.Value, 
                  FontCache.Get(fontName),
                  fontSize, color, str)
        {
        }

        public Text(GraphicsDevice device, FontSystem fontSystem, 
            float fontSize, Color color, string str)
        {
            this.device = device;
            this.fontSystem = fontSystem;
            this.renderer = new FontStashRenderer(device);

            this.fontSize = fontSize;
            base.Color = color;
            this.str = str;
            NeedUpdate();
        }
        private void NeedUpdate() { needUpdate = true; }
        private void Update()
        {
            if (!needUpdate)
                return;

            font = fontSystem.GetFont(fontSize); // มี cache ให้อยู่แล้ว
            renderer.ClearAndInit(font);
            DrawText();
            CalculateBaseLineAndTotalSize();
            // color[] 
            // textStyle ล่ะ  มีแค่ขีดเส้นใต้ กับ strike through
            // charspace กับ linespace ล่ะ ควรรับเข้ามาไหม ? // 24/7/24 พบว่า linespacing ไม่ได้ใช้ใน code ของ font stash sharp
            needUpdate = false;
        }
        private void DrawText()
        {
            if(colorArray == null)
                font.DrawText(renderer, str, new Vector2(), Color, effect: Effect, effectAmount: EffectAmount
                    , characterSpacing: characterSpacing);
            else
                font.DrawText(renderer, str, new Vector2(), colorArray, effect: Effect, effectAmount: EffectAmount
                    , characterSpacing: characterSpacing);
        }
        private void CalculateBaseLineAndTotalSize()
        {
            string baseLineStr = "X"; // ถือว่า ขนาด glyph ของ "X" ความสูง glyph แทน baseline
            Vector2 baseLineSize = MeasureString(baseLineStr);
            baseLineHeight = baseLineSize.Y;
            //System.Diagnostics.Debug.WriteLine( $"{font.LineHeight}, {font.FontSize}, {baseLineSize}"); 
            //System.Diagnostics.Debug.WriteLine( $": {text.Length}, {renderer.glyphes.Count}");
            //พบว่า text length ตรงกับ renderer glyphes count ยกเว้นกรณีมี \n ซึ่ง glyphes จะน้อยกว่าอยู่ 1 ตัว
            // เดาว่า glyphes จะเว้น \n ไว้
            // ส่วน character ที่ไม่มีเช่น ภาษาไทย เดาว่าใช้ default glyph

            sizeInPixel = MeasureString(str);
        }
        private Vector2 MeasureString(string s)
        {
            return font.MeasureString(s, effect: Effect, effectAmount: EffectAmount
                , characterSpacing: characterSpacing);
        }
        protected override void DrawSelf(DrawTarget target, DrawState state)
        {
            if (needUpdate)
                Update();

            renderer.Draw(target, CombineState(state));
        }

        private class FontStashRenderer : IFontStashRenderer2
        {
            private GraphicsDevice device;
            private SpriteFontBase font;
            private List<VertexArray> glyphes; // Caching vertex

            public FontStashRenderer(GraphicsDevice device)
            {
                this.device = device;
                glyphes = new List<VertexArray>();
            }
            public void ClearAndInit(SpriteFontBase font)
            {
                glyphes.Clear();
                this.font = font;
            }
            public GraphicsDevice GraphicsDevice => device;

            // วาดขึ้นจอจริงๆ : จังหวะนี้จึงค่อย apply state.transform ที่ส่งมาจาก caller (Text)
            public void Draw(DrawTarget target, DrawState state)
            {
                var combine = state.Combine(GetSwapMatrix());
                for (int i=0;i<glyphes.Count;++i)
                    glyphes[i].Draw(target, combine);
            }
            private Matrix3 GetSwapMatrix()
            {
                if(!GlobalConfig.GeometricalYAxis)
                    return Matrix3.Identity;

                var res = Matrix3.CreateTranslation(0, font.LineHeight).Combine(
                        Matrix3.CreateScale(1, -1));
                return res;
            }

            // draw เก็บไว้ใน List of VertexArray
            public void DrawQuad(Texture2D texture, 
                ref VertexPositionColorTexture topLeft, ref VertexPositionColorTexture topRight, 
                ref VertexPositionColorTexture bottomLeft, ref VertexPositionColorTexture bottomRight)
            {
                VertexArray glyph = new VertexArray();
                glyph.texture = texture;

                glyph.vertices = new VertexPositionColorTexture[4];
                glyph.vertices[0] = topLeft;
                glyph.vertices[1] = topRight;
                glyph.vertices[2] = bottomLeft;
                glyph.vertices[3] = bottomRight;

                //CheckSwapTexture(glyph.vertices);

                glyph.indices = new short[6];
                glyph.indices[0] = 0;
                glyph.indices[1] = 1;
                glyph.indices[2] = 2;
                glyph.indices[3] = 1;
                glyph.indices[4] = 3;
                glyph.indices[5] = 2;

                this.glyphes.Add(glyph);
            }

            private void CheckSwapTexture(VertexPositionColorTexture[] vertices)
            {
                float top = vertices[0].TextureCoordinate.Y;
                float bottom = vertices[2].TextureCoordinate.Y;
                if (GlobalConfig.GeometricalYAxis) // need swap
                {
                    vertices[0].TextureCoordinate.Y = bottom;
                    vertices[1].TextureCoordinate.Y = bottom;
                    vertices[2].TextureCoordinate.Y = top;
                    vertices[3].TextureCoordinate.Y = top;
                }

            }
        }
    }
}
