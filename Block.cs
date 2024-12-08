using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

namespace Game09
{
    public class Block : RectangleActor
    {
        private bool isVisible; // Tracks if the block is visible

        public Block(RectF rect)
            : base(Color.White, rect) // Set initial color to white
        {
            isVisible = false; // Block is initially invisible

            // Set up collision detection
            var collisionObj = CollisionObj.CreateWithRect(this, 2);
            collisionObj.DebugDraw = true; // Enable debug drawing
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
                base.Draw(target, state); // Call base draw to render the rectangle
            }
        }
    }
}
