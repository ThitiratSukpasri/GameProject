using Game11;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using ThanaNita.MonoGameTnt;


namespace Game09
{
    public enum GameState
    {
        Playing,
        GameOver
    }

    public class Game09 : Game2D
    {
        private GameState currentState = GameState.Playing;
        private Girl girl;
        private Text gameText;
        private Text scoreText; // Text object to display the score
        private Placeholder gamePlaceholder = new Placeholder();
        private Vector2 screenSize = new Vector2(1920, 1080); // Adjust as necessary
        public static int ScoreCounter { get; set; } = 0;


        protected override void LoadContent()
        {
            ClearColor = Color.CadetBlue;
            BackgroundColor = Color.CadetBlue;
            CollisionDetectionUnit.AddDetector(1, 2);

            // Initialize the girl before setting the checkpoint
            girl = new Girl(this, new Vector2(ScreenSize.X / 2, 0));
            All.Add(girl);
            girl.Add(new CameraMan(Camera, ScreenSize));

            scoreText = new Text("resource/FiraCodeNerdFontMono.ttf", 40, Color.White, "Score: 0");
            scoreText.Position = new Vector2(20, 20); // Top-left corner
            All.Add(scoreText);

            // Add other game elements
            CreateRandomBricks();
            CreateTraps();
            CreateInvisibleTrap();
        }

        private void CreateInvisibleTrap()
        {
            var Vtraps = new (Vector2 position, int textureIndex)[]
            {
                (new Vector2(770, 550), 1),
                (new Vector2(2300, 50), 1),
            };

            foreach (var trapData in Vtraps)
            {
                var Vtrap = new InvisibleTrap(trapData.position, trapData.textureIndex);
                All.Add(Vtrap);
            }
        }

        private void CreateRandomBricks()
        {
            All.Add(new Brick(new RectF(0, 600, 1500, 96)));
            All.Add(new Brick(new RectF(1500, 505, 150, 192)));
            All.Add(new Brick(new RectF(600, 420, 100, 48)));
            All.Add(new Brick(new RectF(1000, 320, 100, 48)));
            All.Add(new Brick(new RectF(1200, 400, 100, 48)));
            All.Add(new Brick(new RectF(1700, 350, 200, 30)));
            All.Add(new Block(new RectF(1900, 140, 50, 50)));
            All.Add(new Brick(new RectF(2200, 100, 1000, 48)));
        }

        protected override void Update(GameTime gameTime)
        {
            if (currentState == GameState.GameOver)
            {
                var keyInfo = Keyboard.GetState();
                if (keyInfo.IsKeyDown(Keys.Enter))
                {
                    RestartGame();
                }
                return; // Skip base update when the game is over
            }
            scoreText.Str = $"Score: {ScoreCounter}";

            base.Update(gameTime); // Call the base update when the game is playing
        }

        private void RestartGame()
        {
            girl.Reset();
            currentState = GameState.Playing;

            All.Clear();
            LoadContent();
        }

        private void CreateTraps()
        {
            var traps = new (Vector2 position, int textureIndex)[]
            {
                (new Vector2(1100, 550), 3),
            };

            foreach (var trapData in traps)
            {
                var trap = new Trap(trapData.position, trapData.textureIndex);
                All.Add(trap);
            }
        }

        public void SetGameOver(Vector2 Position)
        {
            currentState = GameState.GameOver;

            gameText = new Text("resource/FiraCodeNerdFontMono.ttf", 80, Color.Blue, "Game Over");

            //gameText.Position = new Vector2(ScreenSize.X-215, ScreenSize.Y-110);

            gameText.Position = new Vector2(Position.X - 155, Position.Y - 150);
            All.Add(gameText);
        }
        public void SetScore(Vector2 Position)
        {
            //currentState = GameState.GameOver;

            //scoreText = new Text("resource/FiraCodeNerdFontMono.ttf", 40, Color.White, "Score: 0");
            scoreText.Position = new Vector2(Position.X - 700, Position.Y - 550);
            scoreText.Str = $"Score: {ScoreCounter}";
            All.Add(scoreText);
        }
    }
}