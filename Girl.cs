using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using ThanaNita.MonoGameTnt;
using System.Threading.Tasks;

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
        //sound
        private SoundEffect jumpEffect, gameoverEffect, getEffect, santaEffect, checkpointEffect;
        private SoundEffectInstance jumpEffectInstance, gameoverEffectInstance, getEffectInstance, santaEffectInstance, checkpointEffectInstance;
        private float soundEffectVolume = 0.5f;

        private Song backgroundMusic;

        // Constructor now takes a Game09 instance and position
        public Girl(Game09 game, Vector2 position)
        {
            mainGame = game; // Assign the passed Game09 instance to the field

            //var size = new Vector2(60, 60);
            var size = new Vector2(60, 60); //60 41
            Position = position;
            Origin = size / 2;
            Scale = new Vector2(2, 2); //2,2

            //var texture = TextureCache.Get("Girl.png");
            var texture = TextureCache.Get("santa_new.png");
            var regions2d = RegionCutter.Cut(texture, size);
            var selector = new RegionSelector(regions2d);
            /*var stay = new Animation(this, 1.0f, selector.Select1by1(0, 4));
            var left = new Animation(this, 1.0f, selector.Select(start: 8, count: 8));
            var right = new Animation(this, 1.0f, selector.Select(start: 16, count: 8));*/
            var stay = new Animation(this, 1.0f, selector.Select1by1(0, 5));
            var left = new Animation(this, 1.0f, selector.Select(start: 29, count: 6));
            var right = new Animation(this, 1.0f, selector.Select(start: 6, count: 6));
            states = new AnimationStates(new[] { stay, left, right });
            AddAction(states);

            var collisionObj = CollisionObj.CreateWithRect(this, RawRect.CreateAdjusted(0.4f, 1), 1);
            collisionObj.OnCollide = OnCollide;
            collisionObj.DebugDraw = false;
            Add(collisionObj);

            // โหลด Background Music
            backgroundMusic = mainGame.Content.Load<Song>(@"sound\background");
            MediaPlayer.IsRepeating = true; // เล่นซ้ำ
            MediaPlayer.Volume = 0.3f; // ลดระดับเสียง


            santaEffect = mainGame.Content.Load<SoundEffect>(@"sound\santa");//sound santa
            santaEffectInstance = santaEffect.CreateInstance();

            jumpEffect = mainGame.Content.Load<SoundEffect>(@"sound\jump1");//sound jump
            jumpEffectInstance = jumpEffect.CreateInstance();

            gameoverEffect = mainGame.Content.Load<SoundEffect>(@"sound\gameover");//gameoverEffect
            gameoverEffectInstance = gameoverEffect.CreateInstance();

            checkpointEffect = mainGame.Content.Load<SoundEffect>(@"sound\checkpoint");//checkpointEffect
            checkpointEffectInstance = checkpointEffect.CreateInstance();


            getEffect = mainGame.Content.Load<SoundEffect>(@"sound\get");//getEffect
            getEffectInstance = getEffect.CreateInstance();

            // เล่นเสียง Santa Effect
            if (santaEffectInstance.State != SoundState.Playing)
            {
                santaEffectInstance.Volume = soundEffectVolume;
                santaEffectInstance.Play();
            }
            // เริ่ม Background Music หลังเสียง Santa Effect
            Task.Delay(2000).ContinueWith(_ =>
            {
                MediaPlayer.Play(backgroundMusic);
            });
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
            // Check if the girl's position exceeds 2000
            if (Position.Y > 1400)
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
            {
                V.Y = -750;
                if (jumpEffectInstance.State != SoundState.Playing)
                {
                    jumpEffectInstance.Volume = soundEffectVolume;
                    jumpEffectInstance.Play(); //sound
                }
            }
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
            mainGame.SetGameOver(Position,false); // Call SetGameOver method to handle game over state
            MediaPlayer.Stop();
            //sound
            if (gameoverEffectInstance.State != SoundState.Playing)
            {
                gameoverEffectInstance.Volume = soundEffectVolume;
                gameoverEffectInstance.Play(); //sound
            }
        }
        public void Collect()
        {
            Random random = new Random();

            // Generate a random integer to decide the effect
            int effect = random.Next(2); 

            if (effect == 0)
            {
                speedMultiplier = 1.5f; // Increase speed 
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
            if (getEffectInstance.State != SoundState.Playing)
            {
                getEffectInstance.Volume = soundEffectVolume;
                getEffectInstance.Play(); //sound
            }

        }
        
        public void Checkpoint()
        {
            if (checkpointEffectInstance.State != SoundState.Playing)
            {
                checkpointEffectInstance.Volume = soundEffectVolume;
                checkpointEffectInstance.Play();
            }
            if (mainGame.currentState == GameState.Stage1)
            {
                mainGame.LoadStage2();
            }
            else if(mainGame.currentState == GameState.Stage2) 
            { 
                mainGame.LoadStage3(); 
            }
            else if(mainGame.currentState == GameState.Stage3)
            {
                mainGame.SetGameOver(Position,true);
                MediaPlayer.Stop();
            }
        }
    }
}