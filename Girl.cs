using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using ThanaNita.MonoGameTnt;

namespace Game09
{
    public class Girl : SpriteActor
    {
        public Game09 mainGame; // Field to hold the reference to the main game
        AnimationStates states;
        Vector2 V;
        bool onFloor;
        bool isStopped;
        private float speedMultiplier = 1f; // Default to normal speed
        private float EffectDuration = 0f; // Remaining time for the slow effect

        // Constructor now takes a Game09 instance and position
        public Girl(Game09 game, Vector2 position)
        {
            mainGame = game; // Assign the passed Game09 instance to the field

            var size = new Vector2(60, 60);
            Position = position;
            Origin = size / 2;
            Scale = new Vector2(2, 2);

            var texture = TextureCache.Get("Girl.png");
            var regions2d = RegionCutter.Cut(texture, size);
            var selector = new RegionSelector(regions2d);
            var stay = new Animation(this, 1.0f, selector.Select1by1(0, 4));
            var left = new Animation(this, 1.0f, selector.Select(start: 8, count: 8));
            var right = new Animation(this, 1.0f, selector.Select(start: 16, count: 8));
            states = new AnimationStates(new[] { stay, left, right });
            AddAction(states);

            var collisionObj = CollisionObj.CreateWithRect(this, RawRect.CreateAdjusted(0.3f, 1), 1);
            collisionObj.OnCollide = OnCollide;
            collisionObj.DebugDraw = true;
            Add(collisionObj);

        }

        // Reset the girl's position to the last checkpoint or initial spawn point
        public void Reset()
        {
            V = Vector2.Zero; // Reset velocity
            isStopped = false; // Allow movement
            onFloor = false; // Reset floor state
        }


        public void OnCollide(CollisionObj objB, CollideData data)
        {
            var direction = data.objA.RelativeDirection(data.OverlapRect);

            if (direction.Y == 1)
                onFloor = true;


            if ((direction.Y > 0 && V.Y > 0) ||
                (direction.Y < 0 && V.Y < 0))
            {
                V.Y = 0;
                Position -= new Vector2(0, data.OverlapRect.Height * direction.Y);
            }
            if ((direction.X > 0 && V.X > 0) ||
                (direction.X < 0 && V.X < 0))
            {
                V.X = 0;
                Position -= new Vector2(data.OverlapRect.Width * direction.X, 0);
            }
        }

        public override void Act(float deltaTime)
        {
            if (isStopped)
                return; // Stop movement if the girl has collided with a trap

            if (EffectDuration > 0)
            {
                   EffectDuration -= deltaTime;
                if (EffectDuration <= 0)
                {
                    speedMultiplier = 1f; // Reset speed multiplier
                    Console.WriteLine("The slow effect has ended.");
                }
            }
            ChangeVy(deltaTime);

            var direction = DirectionKey.Direction;
            //V.X = direction.X * 500; // Change only V.X
            V.X = direction.X * 500 * speedMultiplier; // Apply the speed multiplier

            if (direction.X > 0)
                states.Animate(2);
            else if (direction.X < 0)
                states.Animate(1);
            else
                states.Animate(0);


            base.Act(deltaTime);
            Position += V * deltaTime; // Update position based on velocity
            onFloor = false;
            CheckFall();

        }
        private void CheckFall()
        {
            // Check if the girl's position exceeds 1000
            if (Position.Y > 1200)
            {
                Die(); // Call the Die method if she falls off the platform
            }
        }
        private void ChangeVy(float deltaTime)
        {
            // Gravity
            Vector2 a = new Vector2(0, 1500);
            V.Y += a.Y * deltaTime;

            // Jump
            var keyInfo = GlobalKeyboardInfo.Value;
            if (keyInfo.IsKeyPressed(Keys.Space) && onFloor)
                V.Y = -750;
        }

        public void StopAtTrap()
        {
            // Logic to stop the girl's movement
            V = Vector2.Zero;
            isStopped = true;
            Console.WriteLine("The girl has stopped at the trap.");
        }

        public void Die()
        {
            Console.WriteLine("The girl has died!");
            StopAtTrap(); // Stop the girl's movement

            // Notify the main game class about the game over
            mainGame.SetGameOver(Position); // Call SetGameOver method to handle game over state
        }
        public void Collect()
        {
            Random random = new Random();

            // Generate a random integer to decide the effect
            int effect = random.Next(3); // 0 = bigger, 1 = smaller, 2 = slower

            if (effect == 0)
            {
                speedMultiplier = 2f; // Reduce speed to half
                EffectDuration = 3f; // Slow effect lasts for 3 seconds
                Console.WriteLine("The girl has become faster!");

            }
            else if (effect == 1)
            {
                // Make the girl slower
                speedMultiplier = 0.5f; // Reduce speed to half
                EffectDuration = 3f; // Slow effect lasts for 3 seconds

                Console.WriteLine("The girl has become slower!");
            }
           
        }
        
        public void Checkpoint()
        {
            mainGame.LoadNewStage();

        }
    }
}