using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ThanaNita.MonoGameTnt
{
    public interface DrawTargetSimple
    {
        void Draw(VertexPositionColorTexture[] vertices, short[] indices, Texture2D texture)
        {
            Draw(vertices, vertices.Length, indices, indices.Length, texture);
        }
        void Draw(VertexPositionColorTexture[] vertices, int verticesCount, 
                    short[] indices, int indicesCount, Texture2D texture);
    }
    public interface EffectAdapter : DrawTargetSimple
    {
        Matrix ViewMatrix { get; set; }
        Matrix ProjectionMatrix { get; set; }
        void ProjectionMatrixRecalculate(); // มักคำนวณจาก GraphicsDevice.ViewPort
    }
    public interface DrawTarget : DrawTargetSimple
    {
        void Draw(VertexPositionColorTexture[] vertices, short[] indices, 
                    Texture2D texture, DrawState state)
        {
            Draw(vertices, vertices.Length, indices, indices.Length, texture, state);
        }
        void Draw(VertexPositionColorTexture[] vertices, int verticesCount,
                    short[] indices, int indicesCount, Texture2D texture, DrawState state);
        void Flush();
    }
    public interface Drawable
    {
        void Draw(DrawTarget target, DrawState state);
    }
}
