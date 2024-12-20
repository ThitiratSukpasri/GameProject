using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ThanaNita.MonoGameTnt;

namespace Game09
{
    public class InvisibleTrap : SpriteActor
    {
        private bool isVisible; // Tracks if the trap is visible

        public InvisibleTrap(Vector2 position, int textureIndex)
        {
            Position = position;
            Origin = new Vector2(30, 30); // Adjust origin if necessary
            Scale = new Vector2(1.5f, 1.5f); // Adjust size as needed

            // Load trap texture
            //var texture = TextureCache.Get("trap_1.png"); // Ensure you have this texture in your content
            var texture = TextureCache.Get($"trap_{textureIndex}.png"); // Use texture based on index
            
            
            //SetTextureRegion(new TextureRegion(texture, new RectF(Vector2.Zero, new Vector2(80, 80)))); // Size based on texture 80*80
            SetTextureRegion(new TextureRegion(texture, new RectF(new Vector2(0,0), new Vector2(80, 67))));
            // Initially, the trap is invisible
            isVisible = false;
            
            // Set up collision detection      

            var collisionObj = CollisionObj.CreateWithRect(this, RawRect.CreateAdjusted(0.7f, 0.2f), 2); //0.2f
            collisionObj.OnCollide = OnCollide; // Define what happens on collision
            collisionObj.DebugDraw = false;

            Add(collisionObj);
        }

        private void OnCollide(CollisionObj objB, CollideData data)
        {
            if (objB.Parent is Girl girl)
            {
                // Make the trap visible upon collision
                isVisible = true;

                // Stop the girl at the trap
                //girl.StopAtTrap();
                girl.Die();
            }
        }

        // Override the Draw method with the required parameters
        public override void Draw(DrawTarget target, DrawState state)
        {
            // Only draw the trap if it is marked as visible
            if (isVisible)
            {
                base.Draw(target, state); // Call the base draw to render the texture
            }
        }
    }
}
