using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game09
{
    public class MenuState : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _backgroundTexture;

        private DynamicSpriteFont _font;
        private SpriteFontBase _fontBase;
        
        private Vector2 _startTextPosition;
        private Vector2 _exitTextPosition;

        private string _startText = "Start Game";
        private string _exitText = "Exit Game";

        private MouseState _currentMouseState;
        private MouseState _previousMouseState;

        //sound
        private SoundEffect clickEffect;
        private SoundEffectInstance clickEffectInstance;
        private float soundEffectVolume = 0.8f;

        public MenuState()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // ตั้งค่าขนาดหน้าจอ
            _graphics.PreferredBackBufferWidth = 1920; // กำหนดความกว้าง
            _graphics.PreferredBackBufferHeight = 1080; // กำหนดความสูง
            _graphics.ApplyChanges(); // อัปเดตการตั้งค่า
           
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // โหลดฟอนต์จากไฟล์ .ttf
            string fontPath = Path.Combine("resource", "Pixeland.ttf");
            if (!File.Exists(fontPath))
                throw new FileNotFoundException($"ฟอนต์ไม่พบในตำแหน่ง {fontPath}");

            var fontSystem = new FontSystem();
            fontSystem.AddFont(File.ReadAllBytes(fontPath));
            _fontBase = fontSystem.GetFont(48); // ขนาดฟอนต์ 48px
                                                // โหลดภาพ PNG โดยตรง
            string backgroundPath = @"Background.png"; // ระบุ path ของไฟล์ PNG
            if (!File.Exists(backgroundPath))
                throw new FileNotFoundException($"ไม่พบไฟล์ Background ที่ตำแหน่ง {backgroundPath}");

            using (var fileStream = new FileStream(backgroundPath, FileMode.Open))
            {
                _backgroundTexture = Texture2D.FromStream(GraphicsDevice, fileStream);
            }
            // คำนวณตำแหน่งของข้อความ (กลางจอ)
            int screenWidth = _graphics.PreferredBackBufferWidth;
            int screenHeight = _graphics.PreferredBackBufferHeight;

            var startTextSize = _fontBase.MeasureString(_startText);
            var exitTextSize = _fontBase.MeasureString(_exitText);

            _startTextPosition = new Vector2((screenWidth - startTextSize.X) / 2, (screenHeight - startTextSize.Y) / 2 - 50);
            _exitTextPosition = new Vector2((screenWidth - exitTextSize.X) / 2, (screenHeight - exitTextSize.Y) / 2 + 50);

            //sound
            clickEffect = Content.Load<SoundEffect>(@"sound\click");//sound click
            clickEffectInstance = clickEffect.CreateInstance();
        }


        protected override void Update(GameTime gameTime)
        {
            _currentMouseState = Mouse.GetState();

            if (IsMouseClickedOnText(_startText, _startTextPosition))
            {
                // เริ่มเกม
                //clickEffectInstance.Play();
                /*using var game = new Game09();
                game.Run();
                Exit();*/ // ปิด MenuState ก่อน
                using (var game = new Game09())
                {
                    game.Run();
                }
            }

            if (IsMouseClickedOnText(_exitText, _exitTextPosition))
            {
                // ออกจากเกม
                Exit();
            }

            _previousMouseState = _currentMouseState;

            base.Update(gameTime);
        }

        private bool IsMouseClickedOnText(string text, Vector2 position)
        {
            // คำนวณขอบเขตของข้อความ
            var size = _fontBase.MeasureString(text);
            var textBounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

            return _currentMouseState.LeftButton == ButtonState.Pressed &&
                   _previousMouseState.LeftButton == ButtonState.Released &&
                   textBounds.Contains(_currentMouseState.Position);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // วาดภาพ Background ให้เต็มหน้าจอ
            _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);

            // วาดข้อความ Start และ Exit
            _spriteBatch.DrawString(_fontBase, _startText, _startTextPosition, Color.White);
            _spriteBatch.DrawString(_fontBase, _exitText, _exitTextPosition, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
