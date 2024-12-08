using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System.Diagnostics;

namespace ThanaNita.MonoGameTnt
{
    public struct SpriteDrawable
    {
        private TextureRegion region;
        private VertexPositionColorTexture[] vertices = new VertexPositionColorTexture[4];
        private short[] indices = new short[6];
        public Vector2 RawSize { get => region.Size; }
        public RectF RawRect { get => new RectF(new Vector2(), region.Size); }


        // ยังไม่สมบูรณ์ ยังไม่มี Texture ต้องรอ set เข้ามา
        // ใช้เผื่อกรณีจะทำ animation ต้องสร้าง sprite ว่างๆ เตรียมไว้ก่อน แล้วจึงให้ animation set ค่า texture เข้ามา
        public SpriteDrawable()
        {
        }
        public SpriteDrawable(Texture2D texture)
            : this(new TextureRegion(texture))
        {
        }
        public SpriteDrawable(TextureRegion region)
        {
            this.region = region;
            UpdatePositionColorTexture();
        }

        private void UpdatePositionColorTexture()
        {
            UpdatePositionTexture();
            UpdateColor();
        }
        private void UpdatePositionTexture()
        {
            if (region.Texture == null)
                return;
            RectF rect = // normalized texture rect
                DrawableUtil.NormalizeRect(region.Region, region.Texture);
            
            float w = region.Region.Width;
            float h = region.Region.Height;
            vertices[0].Position = new Vector3(0, 0, 0);
            vertices[1].Position = new Vector3(w, 0, 0);
            vertices[2].Position = new Vector3(0, h, 0);
            vertices[3].Position = new Vector3(w, h, 0);

            float l = rect.X;
            float r = rect.XMax;
            float t = !GlobalConfig.GeometricalYAxis ? rect.Y : rect.YMax;
            float b = !GlobalConfig.GeometricalYAxis ? rect.YMax : rect.Y;
            vertices[0].TextureCoordinate = new Vector2(l, t);
            vertices[1].TextureCoordinate = new Vector2(r, t);
            vertices[2].TextureCoordinate = new Vector2(l, b);
            vertices[3].TextureCoordinate = new Vector2(r, b);

            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;
            indices[3] = 1;
            indices[4] = 3;
            indices[5] = 2;
        }


        public void SetTextureRegion(TextureRegion region)
        {
            this.region = region;
            UpdatePositionColorTexture();
        }

        private void UpdateColor()
        {
            for (int i = 0; i < vertices.Length; i++)
                vertices[i].Color = Color.White;
        }

        public void Draw(DrawTarget target, DrawState state)
        {
            if (region.Texture == null)
                return;

            target.Draw(vertices, indices, region.Texture, state);
        }
    }
}
