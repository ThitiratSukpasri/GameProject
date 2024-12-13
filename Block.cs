using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

namespace Game09
{
    public class Block : SpriteActor // Use SpriteActor to handle textures
    {
        private bool isVisible; // Tracks if the block is visible

        public Block(RectF rect)
        {
            // Load the texture and set the texture region
            var texture = TextureCache.Get("brick2resize.png");
            SetTextureRegion(new TextureRegion(texture, new RectF(Vector2.Zero, rect.Size)));

            // Set the position of the block
            Position = rect.Position;

            // Initialize visibility
            isVisible = false;

            // Set up collision detection
            var collisionObj = CollisionObj.CreateWithRect(this, 2);
            collisionObj.DebugDraw = false; // Enable debug drawing
            collisionObj.OnCollide = OnCollide; // Trigger collision logic
            Add(collisionObj);
        }

        private void OnCollide(CollisionObj objB, CollideData data)
        {
            if (objB.Parent is Girl girl)
            {
                // Make the block visible upon collision
                isVisible = true;
            }
        }

        // Override the Draw method to control visibility
        public override void Draw(DrawTarget target, DrawState state)
        {
            // Only draw the block if it is visible
            if (isVisible)
            {
                base.Draw(target, state); // Call base draw to render the sprite
            }
        }
    }
}
