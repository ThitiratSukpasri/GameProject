using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ThanaNita.MonoGameTnt
{
    public class SpriteEffectAdapter : EffectAdapter
    {
        private SpriteEffect2 effect;
        private GraphicsDevice GraphicsDevice;
        private WhitePixel whitePixel;
        public Matrix ViewMatrix { get => effect.TransformMatrix.Value; set => effect.TransformMatrix = value; } 
        public Matrix ProjectionMatrix { get => effect.Projection; set => effect.Projection = value; }

        // created one time per game
        public SpriteEffectAdapter(GraphicsDevice device)
        {
            this.GraphicsDevice = device;
            effect = new SpriteEffect2(GraphicsDevice);
            whitePixel = new WhitePixel(device);

            ViewMatrix = Matrix.Identity;
        }
        public void ProjectionMatrixRecalculate()
        {
            effect.ProjectionMatrixRecalculate();
        }
        public void Draw(VertexPositionColorTexture[] vertices, int verticesCount,
                    short[] indices, int indicesCount, Texture2D texture)
        {
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.Textures[0] = GetTexture(texture);               // 2. ต้อง set texture หลังจาก apply

                GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColorTexture>(
                    PrimitiveType.TriangleList, vertices, 0, verticesCount, indices, 0, indicesCount / 3);
            }
        }

        private Texture2D GetTexture(Texture2D texture)
        {
            if (texture == null)
                return whitePixel.Get();
            return texture;
        }
    }
}
