using Game09;
using Microsoft.Xna.Framework;
using System;
using ThanaNita.MonoGameTnt;


namespace Game09
{
    public class Trap : SpriteActor
    {
        public Trap(Vector2 position, int textureIndex)
        {
            Position = position;
            Origin = new Vector2(30, 30); // Center of the trap
            Scale = new Vector2(1.0f, 1.0f); // Adjust size as needed

            // Load trap texture based on the provided index
            var texture = TextureCache.Get($"trap_{textureIndex}.png"); // Use texture based on index
            SetTextureRegion(new TextureRegion(texture, new RectF(Vector2.Zero, new Vector2(60, 60)))); // Adjust size based on texture

            // Collision detection setup
            var collisionObj = CollisionObj.CreateWithRect(this, RawRect.CreateAdjusted(0.5f, 1), 2);
            collisionObj.OnCollide = OnCollide; // Define what happens on collision
            Add(collisionObj);
        }

        private void OnCollide(CollisionObj objB, CollideData data)
        {
            Console.WriteLine("Collision detected with: " + objB.Parent.GetType().Name);

            if (objB.Parent is Girl girl)
            {
                Console.WriteLine("Girl has collided with the trap.");
                //girl.Die(); // Call the method to handle the collision
                girl.Collect();
                this.Detach();
                Game09.ScoreCounter++;
            }
        }
    }

}
