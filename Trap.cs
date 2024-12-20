using Game09;
using Microsoft.Xna.Framework;
using System;
using ThanaNita.MonoGameTnt;


namespace Game09
{
    public class Trap : SpriteActor
    {
        private int textureindex;
        public Trap(Vector2 position, int textureIndex)
        {
            Position = position;
            Origin = new Vector2(30, 30); // Center of the trap
            Scale = new Vector2(1.5f, 1.5f); // Adjust size as needed
            textureindex = textureIndex;
            // Load trap texture based on the provided index
            var texture = TextureCache.Get($"trap_{textureIndex}.png"); // Use texture based on index
            //SetTextureRegion(new TextureRegion(texture, new RectF(Vector2.Zero, new Vector2(80, 80))));
            SetTextureRegion(new TextureRegion(texture, new RectF(new Vector2(0, 0), new Vector2(80, 80)))); // Adjust size based on texture

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
                if (textureindex == 7)
                {
                    girl.Collect();
                    this.Detach();
                    Game09.ScoreCounter++;
                }
                else if (textureindex == 4)
                {
                    girl.Die();
                }
                else if(textureindex == 2)
                {
                    girl.Checkpoint();
                    this.Detach();
                }
                
            }
        }
    }

}
