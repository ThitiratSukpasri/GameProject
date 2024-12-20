using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using ThanaNita.MonoGameTnt;

namespace Game09
{
    public class BG : SpriteActor
    {
        private Vector2 initialPosition;
        private OrthographicCamera camera;
        private GraphicsDevice graphicsDevice;
        public float Width { get; set; }
        public float Height { get; set; }
        public float LayerDepth { get; set; }
        private float speedMultiplier;
        public Texture2D texture;
        public Vector2 position;

        public float SpeedMultiplier { get; set; } = 1f;

        public BG(RectF rect, Texture2D texture, OrthographicCamera camera, GraphicsDevice graphicsDevice, float speedMultiplier = 1.0f)
        {
            SetTexture(texture);
            initialPosition = new Vector2(rect.X, rect.Y);
            Position = new Vector2(rect.X, rect.Y);
            Width = graphicsDevice.Viewport.Width;  // ตั้งค่าความกว้างของพื้นหลังให้เท่ากับขนาดของหน้าจอ
            Height = graphicsDevice.Viewport.Height; // ตั้งค่าความสูงของพื้นหลังให้เท่ากับขนาดของหน้าจอ
            Scale = new Vector2(
                rect.Width / texture.Width,
                rect.Height / texture.Height);
            this.camera = camera;
            this.graphicsDevice = graphicsDevice;
            this.speedMultiplier = speedMultiplier;
            this.texture = texture;
            this.position = Vector2.Zero;
        }
        
    }

}