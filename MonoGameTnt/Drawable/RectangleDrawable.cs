using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ThanaNita.MonoGameTnt
{
    // ปรับตำแหน่งไม่ได้ แต่ปรับสีได้
    // มีค่า x, y, width, height
    public class RectangleDrawable : Drawable
    {
        private VertexArray array; // struct
        private RectF rect;
        private Color color = Color.WhiteSmoke;
        public RectF BoundingBox { get => rect; }
        public Color Color
        {
            get => color;
            set
            {
                color = value;
                UpdateColor();
            }
        }

        public RectangleDrawable(Color color, float x, float y, float width, float height)
            : this(color, new RectF(x, y, width, height))
        {
        }
        public RectangleDrawable(Color color, Vector2 size)
            : this(color, Vector2.Zero, size)
        {
        }
        public RectangleDrawable(Color color, Vector2 position, Vector2 size)
            : this(color, new RectF(position.X, position.Y, size.X, size.Y))
        {
        }
        public RectangleDrawable(Color color, RectF rect)
        {
            this.color = color;
            this.rect = rect;
            CreateArray();
            Update();
        }

        private void CreateArray()
        {
            array.texture = null;
            array.vertices = new VertexPositionColorTexture[4];
            InitIndices();
        }

        private void InitIndices()
        {
            array.indices = new short[6];
            array.indices[0] = 0;
            array.indices[1] = 1;
            array.indices[2] = 2;
            array.indices[3] = 1;
            array.indices[4] = 3;
            array.indices[5] = 2;
        }

        private void Update()
        {
            UpdatePositionTexture();
            UpdateColor();
        }

        private void UpdatePositionTexture()
        {
            array.vertices[0].Position = new Vector3(rect.X, rect.Y, 0);
            array.vertices[1].Position = new Vector3(rect.XMax, rect.Y, 0);
            array.vertices[2].Position = new Vector3(rect.X, rect.YMax, 0);
            array.vertices[3].Position = new Vector3(rect.XMax, rect.YMax, 0);

            // don't need to init TextureCoordinate because the value already default at (0,0)
/*            float l = 0;
            float r = 1;
            float t = 0;
            float b = 1;
            array.vertices[0].TextureCoordinate = new Vector2(l, t);
            array.vertices[1].TextureCoordinate = new Vector2(r, t);
            array.vertices[2].TextureCoordinate = new Vector2(l, b);
            array.vertices[3].TextureCoordinate = new Vector2(r, b);*/
        }
        private void UpdateColor()
        {
            array.vertices[0].Color = color;
            array.vertices[1].Color = color;
            array.vertices[2].Color = color;
            array.vertices[3].Color = color;
        }

        public void Draw(DrawTarget target, DrawState state)
        {
            array.Draw(target, state);
        }
    }
}
