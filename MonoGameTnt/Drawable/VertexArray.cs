using Microsoft.Xna.Framework.Graphics;

namespace ThanaNita.MonoGameTnt
{
    // Design as struct to reduce the need of object creation for DrawableVertexArray[] type
    //  (each array element don't need to create)
    // Having the same Draw() method header as the Drawable Interface
    // but not declared to implement the Drawable because the semantic difference
    // of struct vs. class.
    public struct VertexArray
    {
        public VertexPositionColorTexture[] vertices = null;
        public short[] indices = null;
        public Texture2D texture = null;

        public VertexArray()
        {
        }

        public void Draw(DrawTarget target, DrawState state)
        {
            target.Draw(vertices, indices, texture, state);
        }
    }
}
