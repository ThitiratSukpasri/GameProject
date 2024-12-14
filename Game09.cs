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
        GameOver
    }

    public class Game09 : Game2D
    {
        //private GameState currentState = GameState.Stage1;
        private GameState currentState = GameState.Playing;
        private Girl girl;
        private Text gameText;
        private Vector2 screenSize = new Vector2(1920, 1080); // Adjust as necessary
        public static int ScoreCounter { get; set; } = 0;
        private TimeSpan elapsedTime; // Variable to store elapsed time
        private bool isGameOver = false; // Flag to check if the game is over



        protected override void LoadContent()
        {
            ClearColor = Color.CadetBlue;
            BackgroundColor = Color.CadetBlue;
            CollisionDetectionUnit.AddDetector(1, 2);

            // Initialize the girl before setting the checkpoint
            girl = new Girl(this, new Vector2(ScreenSize.X / 2, 0));
            All.Add(girl);
            girl.Add(new CameraMan(Camera, ScreenSize));


            // Add other game elements
            CreateRandomBricks();
            CreateTraps();
            CreateInvisibleTrap();
            Checkpoint();
        }

        private void CreateInvisibleTrap()
        {
            var Vtraps = new (Vector2 position, int textureIndex)[]
            {
                (new Vector2(770, 550), 1), // trap stage1,2
                (new Vector2(2300, 50), 4), //trap stage1
                (new Vector2(1210, 360), 1), // trap stage1,2
                (new Vector2(2300, 500), 1), //trap step down stage2 
                (new Vector2(2300, 850), 4), //trap platfrom
                (new Vector2(2980,800), 4) //trap step down stage2 above head trap

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
            currentState = GameState.Playing;
            /*if (currentState == GameState.Stage1)
            {
                All.Clear();
                LoadContent();
            }
            else if (currentState == GameState.Stage2)
            {
                All.Clear();
                LoadNewStage();
            }*/

            All.Clear();
            LoadContent();
        }

        private void CreateTraps()
        {
            var traps = new (Vector2 position, int textureIndex)[]
            {
                (new Vector2(1100, 550), 7), //apple stage 1
                (new Vector2(3624,1050), 7),
                (new Vector2(2300, -30), 4), //trap stage1 tree


                //(new Vector2(3700,1100), 3) 
            };

            foreach (var trapData in traps)
            {
                var trap = new Trap(trapData.position, trapData.textureIndex);
                All.Add(trap);
            }
        }
        private void Checkpoint()
        {
            var traps = new (Vector2 position, int textureIndex)[]
           {
                (new Vector2(3000,-40), 2),
                (new Vector2(4400,910), 2)

           };

            foreach (var trapData in traps)
            {
                var Checkpoint = new Checkpoint(trapData.position, trapData.textureIndex);
                All.Add(Checkpoint);
            }
        }
       
    
        public void SetGameOver(Vector2 Position)
        {
            currentState = GameState.GameOver;

            gameText = new Text("resource/FiraCodeNerdFontMono.ttf", 80, Color.Blue, "Game Over");

            gameText.Position = new Vector2(Position.X - 155, Position.Y - 150);
            All.Add(gameText);
            string timeText = $"Time: {elapsedTime.TotalSeconds:F2}s";
            var timeDisplay = new Text("resource/FiraCodeNerdFontMono.ttf", 40, Color.White, timeText);
            timeDisplay.Position = new Vector2(Position.X - 100, Position.Y + 80);
            All.Add(timeDisplay);
        }


        public void LoadNewStage()
        {
            Console.WriteLine("Transitioning to the new stage...");

            // Clear current stage objects
            All.Clear();

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
                new RectF(new Vector2(3780, 980), new Vector2(720,48)) //step3 up long
            };
            foreach (var rect in brickData)
            {
                var brick = new Brick(rect);  // Directly passing RectF to Brick constructor
                All.Add(brick);  // Add the brick to the game world
            }
            All.Add(new Block(new RectF(3660, 835, 48, 48))); //up block
            All.Add(new Block(new RectF(3650,1200, 130, 50))); // down block
            girl.Position = new Vector2(100, 500); // Reset the girl's position
            All.Add(girl); // Re-add the girl to the new stage
            // Optionally, load assets or configurations specific to the new stage
            CreateTraps();
            CreateInvisibleTrap();
            Checkpoint();
        }
    }
    

}