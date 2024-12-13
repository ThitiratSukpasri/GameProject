//using Game09; // Ensure this is the namespace of SpriteActor
using Microsoft.Xna.Framework;
using System;
using ThanaNita.MonoGameTnt;

namespace Game09
{
    public class Checkpoint : SpriteActor
    {
        public Checkpoint(Vector2 position, int textureIndex) : base()
        {
            Position = position;
            Origin = new Vector2(30, 30);
            Scale = new Vector2(1.0f, 1.0f);

            // Load trap texture based on the provided index
            var texture = TextureCache.Get($"trap_{textureIndex}.png");
            SetTextureRegion(new TextureRegion(texture, new RectF(Vector2.Zero, new Vector2(98, 104))));

            var collisionObj = CollisionObj.CreateWithRect(this, RawRect.CreateAdjusted(1, 1), 2);
            collisionObj.OnCollide = OnCollide;
            Add(collisionObj);
        }

        private void OnCollide(CollisionObj objB, CollideData data)
        {
            Console.WriteLine("Collision detected with: " + objB.Parent.GetType().Name);

            if (objB.Parent is Girl girl)
            {
                Console.WriteLine("Girl has collided with the trap.");
                girl.Checkpoint();
                this.Detach();
                Game09.ScoreCounter++;
            }
        }
    }
}
