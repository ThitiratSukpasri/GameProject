//using Game11;
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
        Stage1,
        Stage2,
        Stage3,
        GameOver
    }

    public class Game09 : Game2D
    {
        public GameState currentState = GameState.Stage1;
        //public GameState currentState = GameState.Playing;
        private Girl girl;
        private Text gameText;
        private SpriteBatch spriteBatch;
        private Vector2 screenSize = new Vector2(1920, 1080); // Adjust as necessary
        public static int ScoreCounter { get; set; } = 0;
        private TimeSpan elapsedTime; // Variable to store elapsed time
        private bool isGameOver = false; // Flag to check if the game is over


        protected override void LoadContent()
        {
            ClearColor = Color.CadetBlue;
            BackgroundColor = Color.CadetBlue;
            CollisionDetectionUnit.AddDetector(1, 2);
            //Background

            spriteBatch = new SpriteBatch(GraphicsDevice);
            Vector2 screenSize = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            ClearColor = Color.CadetBlue;
            BackgroundColor = Color.CadetBlue;
            CollisionDetectionUnit.AddDetector(1, 2);

            //Background
            var skyTexture = TextureCache.Get("Sky_Background.png");
            var cityTexture = TextureCache.Get("City_Midground.png");
            var housesTexture = TextureCache.Get("Houses_Foreground.png");

            All.Add(new BG(new RectF(-2000, -2000, 10000, 10000), skyTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.1f });
            All.Add(new ParallaxBackground(new RectF(0, -50, 2300, 1080), skyTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.3f });


            All.Add(new BG(new RectF(0, -200, 1920, 1080), cityTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.4f });
            All.Add(new BG(new RectF(-2500, -200, 2500, 1080), cityTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.4f });
            All.Add(new BG(new RectF(1900, -200, 1920, 1080), cityTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.4f });
            All.Add(new BG(new RectF(3800, -200, 1920, 1080), cityTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.4f });

            All.Add(new BG(new RectF(-2000, -1100, 10000, 3000), housesTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.2f });
            All.Add(new BG(new RectF(-2500, -260, 1920, 1200), housesTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.5f });
            All.Add(new BG(new RectF(-580, -200, 1920, 1080), housesTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.5f });
            All.Add(new BG(new RectF(1340, -200, 1920, 1080), housesTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.5f });
            All.Add(new BG(new RectF(3260, -200, 1920, 1080), housesTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.5f });


            // Initialize the girl before setting the checkpoint
            girl = new Girl(this, new Vector2(ScreenSize.X / 2, 0));
            All.Add(girl);
            girl.Add(new CameraMan(Camera, ScreenSize));


            // Add other game elements
            CreateRandomBricks();
            CreateTraps();
            CreateInvisibleTrap();
        }



        private void CreateInvisibleTrap()
        {
            var Vtraps = new (Vector2 position, int textureIndex)[]
            {
                (new Vector2(770, 550), 1), // trap stage1,2
                (new Vector2(2300, 50), 4), //trap stage1
                (new Vector2(1210, 360), 1), // trap stage1,2
                (new Vector2(2300, 500), 1), //trap step down stage2 
                (new Vector2(2200, 850), 3), //trap platfrom
                (new Vector2(2700, 730), 5), //trap platfrom above head
                (new Vector2(2980,800), 5), //trap step down stage2 above head trap
                (new Vector2(7010,320), 5), //above head trap stage3
                (new Vector2(8300, 400), 3) //trap stage3

            };

            foreach (var trapData in Vtraps)
            {
                var Vtrap = new InvisibleTrap(trapData.position, trapData.textureIndex);
                All.Add(Vtrap);
            }
        }

        private void CreateRandomBricks()
        {
            var brickData = new RectF[]  // Array of RectF to define position and size
            {
                new RectF(new Vector2(0, 600), new Vector2(1500, 96)),  // Position (x, y) and Size (width, height)
                new RectF(new Vector2(1500, 505), new Vector2(150, 192)),
                new RectF(new Vector2(600, 420), new Vector2(100, 48)),
                new RectF(new Vector2(1000, 320), new Vector2(100, 48)),
                new RectF(new Vector2(1200, 400), new Vector2(100, 48)),
                new RectF(new Vector2(1700, 350), new Vector2(200, 30)),
                new RectF(new Vector2(2200, 30), new Vector2(1000, 48)) //last long
            };

            foreach (var rect in brickData)
            {
                var brick = new Brick(rect);  // Directly passing RectF to Brick constructor
                All.Add(brick);  // Add the brick to the game world
            }
            All.Add(new Block(new RectF(1900, 140, 50, 50)));

        }

        protected override void Update(GameTime gameTime)
        {
            //Background
            float screenWidth = GraphicsDevice.Viewport.Width;
            float screenHeight = GraphicsDevice.Viewport.Height;


            if (currentState == GameState.GameOver)
            {
                var keyInfo = Keyboard.GetState();
                if (keyInfo.IsKeyDown(Keys.Enter))
                {
                    RestartGame();
                }
                return; // Skip base update when the game is over
            }
            if (!isGameOver)
            {
                elapsedTime += gameTime.ElapsedGameTime; // Add elapsed time in each frame
            }
            //scoreText.Str = $"Score: {ScoreCounter}";
            base.Update(gameTime); // Call the base update when the game is playing
        }

        private void RestartGame()
        {
            girl.Reset();
            currentState = GameState.Stage1;
            All.Clear();
            LoadContent();
        }

        private void CreateTraps()
        {
            var traps = new (Vector2 position, int textureIndex)[]
            {
                (new Vector2(1100, 540), 7), //sock stage 1
                (new Vector2(3624,1050), 7), //sock stage 2
                (new Vector2(2300, -30), 4), //trap stage1 tree
                (new Vector2(5500, 400), 4), //trap stage 3
                (new Vector2(5600,400), 7), //sock stage 3
                (new Vector2(6000, 400), 4),//trap stage 3
                (new Vector2(6800, 400), 4),//trap stage 3
                (new Vector2(7100, 400), 7),//sock stage 3
                (new Vector2(7300, 230), 4),//trap step up stage 3
                (new Vector2(8000, 390), 4), //trap stage 3
                (new Vector2(3000,-40), 2), //Checkpoint 1
                (new Vector2(4400, 930), 2), //Checkpoint 2
                (new Vector2(8500, 390), 2) //Checkpoint 3
            };

            foreach (var trapData in traps)
            {
                var trap = new Trap(trapData.position, trapData.textureIndex);
                All.Add(trap);
            }
        }


        public void SetGameOver(Vector2 position, bool isFinished)
        {
            currentState = GameState.GameOver;

            // Game Over or Game Finished Text
            string message = isFinished ? "Game Finished" : "Game Over";
            gameText = new Text("resource/upheavtt.ttf", 80, Color.IndianRed, message);

            // Measure text size
            float textWidth = gameText.Width;     // Get width from your Text class
            float textHeight = gameText.LineHeight; // Get line height from your Text class

            // Center the text
            gameText.Position = new Vector2(position.X - textWidth / 2, position.Y - textHeight / 2 - 100);
            All.Add(gameText);

            // Display Elapsed Time
            string timeText = $"Time: {elapsedTime.TotalSeconds:F2} s";
            var timeDisplay = new Text("resource/upheavtt.ttf", 40, Color.White, timeText);

            // Measure time text size
            float timeTextWidth = timeDisplay.Width;
            float timeTextHeight = timeDisplay.LineHeight;

            // Center the time text below the main text
            timeDisplay.Position = new Vector2(position.X - timeTextWidth / 2, position.Y + textHeight / 2 + 50);
            All.Add(timeDisplay);
        }



        public void LoadStage2()
        {
            currentState = GameState.Stage2;
            Console.WriteLine("Transitioning to the new stage...");

            // Clear current stage objects
            All.Clear();

            //Background stage 2
            var skyTexture = TextureCache.Get("Sky_Background.png");
            var cityTexture = TextureCache.Get("City_Midground.png");
            var housesTexture = TextureCache.Get("Houses_Foreground.png");

            All.Add(new BG(new RectF(-2300, -2000, 10000, 10000), skyTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.1f });
            All.Add(new ParallaxBackground(new RectF(200, 0, 2300, 1080), skyTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.3f });


            All.Add(new BG(new RectF(0, 0, 1920, 1080), cityTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.4f });
            All.Add(new BG(new RectF(-2500, 0, 2500, 1080), cityTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.4f });
            All.Add(new BG(new RectF(1920, 0, 1920, 1080), cityTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.4f });
            All.Add(new BG(new RectF(3840, 0, 1920, 1080), cityTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.4f });

            All.Add(new BG(new RectF(-2300, -700, 10000, 3000), housesTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.2f });
            All.Add(new BG(new RectF(-1900, 140, 1920, 1200), housesTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.5f });
            All.Add(new BG(new RectF(20, 200, 1920, 1080), housesTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.5f });
            All.Add(new BG(new RectF(1940, 200, 1920, 1080), housesTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.5f });
            All.Add(new BG(new RectF(3860, 200, 1920, 1080), housesTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.5f });

            var brickData = new RectF[]  // Array of RectF to define position and size
            {
                new RectF(new Vector2(-50, 600), new Vector2(1200, 96)),  // Position (x, y) and Size (width, height) //-50
                new RectF(new Vector2(750, 400), new Vector2(100, 48)), //step up 800
                new RectF(new Vector2(1150, 400), new Vector2(100, 48)),//step up2 1200
                new RectF(new Vector2(1300, 960), new Vector2(600, 48)),//step down 
                new RectF(new Vector2(2100, 900), new Vector2(600,96)), //platform //chirstmast
                new RectF(new Vector2(2900, 1000), new Vector2(100,48)), //step down 3 // over head trap
                new RectF(new Vector2(3100, 1200), new Vector2(552,96)),//step down near small wall
                new RectF(new Vector2(3600, 1104), new Vector2(48,96)), //small wall
                new RectF(new Vector2(3780, 1000), new Vector2(720,48)) //step3 up long
            };
            foreach (var rect in brickData)
            {
                var brick = new Brick(rect);  // Directly passing RectF to Brick constructor
                All.Add(brick);  // Add the brick to the game world
            }

            All.Add(new Block(new RectF(3660, 835, 48, 48))); //up block
            All.Add(new Block(new RectF(3650, 1200, 130, 50))); // down block
            girl.Position = new Vector2(100, 500); // Reset the girl's position
            All.Add(girl); // Re-add the girl to the new stage
            // Optionally, load assets or configurations specific to the new stage
            CreateTraps();
            CreateInvisibleTrap();
        }
        public void LoadStage3()
        {
            currentState = GameState.Stage3;
            Console.WriteLine("Transitioning to the new stage...");

            // Clear current stage objects
            All.Clear();
            //Background stage 3
            var skyTexture = TextureCache.Get("Sky_Background.png");
            var cityTexture = TextureCache.Get("City_Midground.png");
            var housesTexture = TextureCache.Get("Houses_Foreground.png");

            All.Add(new BG(new RectF(-500, -2000, 10000, 10000), skyTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.1f });
            All.Add(new ParallaxBackground(new RectF(1500, 0, 2300, 1080), skyTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.3f });


            All.Add(new BG(new RectF(4500, -400, 1920, 1080), cityTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.4f });
            All.Add(new BG(new RectF(2000, -400, 2500, 1080), cityTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.4f });
            All.Add(new BG(new RectF(6420, -400, 1920, 1080), cityTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.4f });
            All.Add(new BG(new RectF(8340, -400, 1920, 1080), cityTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.4f });

            All.Add(new BG(new RectF(1000, -1200, 10000, 3000), housesTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.2f });
            All.Add(new BG(new RectF(3000, -360, 1920, 1200), housesTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.5f });
            All.Add(new BG(new RectF(4920, -300, 1920, 1080), housesTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.5f });
            All.Add(new BG(new RectF(6840, -300, 1920, 1080), housesTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.5f });
            All.Add(new BG(new RectF(8760, -300, 1920, 1080), housesTexture, Camera, this.GraphicsDevice) { LayerDepth = 0.5f });

            var brickData = new RectF[]  // Array of RectF to define position and size
            {
                new RectF(new Vector2(5000, 450), new Vector2(1500, 96)),  // Position (x, y) and Size (width, height) //-50
                new RectF(new Vector2(6600, 450), new Vector2(2000, 96)),
                new RectF(new Vector2(7000, 280), new Vector2(400, 48)),
            };
            foreach (var rect in brickData)
            {
                var brick = new Brick(rect);  // Directly passing RectF to Brick constructor
                All.Add(brick);  // Add the brick to the game world
            }

            girl.Position = new Vector2(5100, 300); // Reset the girl's position
            All.Add(girl); // Re-add the girl to the new stage
            // Optionally, load assets or configurations specific to the new stage
            CreateTraps();
            CreateInvisibleTrap();
        }

    }
}


