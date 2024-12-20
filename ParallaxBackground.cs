using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using ThanaNita.MonoGameTnt;

namespace Game09
{
    public class ParallaxBackground : SpriteActor
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

        public ParallaxBackground(RectF rect, Texture2D texture, OrthographicCamera camera, GraphicsDevice graphicsDevice, float speedMultiplier = 1.0f)
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
        
        public override void Act(float deltaTime)
        {
            base.Act(deltaTime);

            // คำนวณการเลื่อนของพื้นหลัง
            var cameraOffset = camera.Position * (1 - LayerDepth) * speedMultiplier;
            Position = initialPosition + cameraOffset;

            // ดึงขนาดความกว้างของ viewport
            float viewportWidth = graphicsDevice.Viewport.Width;

            // การวนลูปพื้นหลังเมื่อมันเคลื่อนที่ออกจากจอ
            if (Position.X < -Width)
            {
                Position = new Vector2(camera.Position.X + viewportWidth, Position.Y);
            }
            else if (Position.X > camera.Position.X + viewportWidth)
            {
                Position = new Vector2(camera.Position.X - Width, Position.Y);
            }
        }

        public void Update(GameTime gameTime)
        {
            position.X -= SpeedMultiplier * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // รีเซ็ตตำแหน่งถ้าพื้นหลังเลื่อนออกจากจอ
            if (position.X <= -texture.Width)  // ใช้ 'texture' แทน 'Texture'
            {
                position.X = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            float screenWidth = spriteBatch.GraphicsDevice.Viewport.Width;
            float screenHeight = spriteBatch.GraphicsDevice.Viewport.Height;
            spriteBatch.Draw(texture, new Rectangle(0, 0, (int)graphicsDevice.Viewport.Width, (int)graphicsDevice.Viewport.Height), Color.White);
        }
    }

}