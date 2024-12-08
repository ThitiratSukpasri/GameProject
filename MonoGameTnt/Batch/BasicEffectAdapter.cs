using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ThanaNita.MonoGameTnt
{
    public class BasicEffectAdapter : EffectAdapter
    {
        private BasicEffect effect;
        private GraphicsDevice GraphicsDevice;
        private Viewport oldViewport;
        public Matrix ViewMatrix { get => effect.View; set => effect.View = value; }
        public Matrix ProjectionMatrix { get => effect.Projection; set => effect.Projection = value; }

        // created one time per game
        public BasicEffectAdapter(GraphicsDevice device)
        {
            this.GraphicsDevice = device;
            effect = new BasicEffect(GraphicsDevice);
            effect.VertexColorEnabled = true;
            oldViewport = new Viewport();
        }

        // called every frames
        private void StartFrame()
        {
            // ตั้งทุกครั้ง เผื่อ viewport เปลี่ยนขนาด
            Viewport viewport = GraphicsDevice.Viewport;
            if (oldViewport.Width != viewport.Width || oldViewport.Height != viewport.Height)
            {
                // ต้อง set ไม่งั้น Camera & ViewportAdapter จะทำงานเพี้ยน
                effect.Projection = CalculateProjection(viewport);
                oldViewport = viewport;
            }
        }
        private Matrix CalculateProjection(Viewport viewport)
        {
            if (!GlobalConfig.GeometricalYAxis)
                return Matrix.CreateOrthographicOffCenter(0, viewport.Width, viewport.Height, 0, -10, 10);
            else
                return Matrix.CreateOrthographicOffCenter(0, viewport.Width, 0, viewport.Height, -10, 10);
        }
        public void ProjectionMatrixRecalculate()
        {
            effect.Projection = CalculateProjection(GraphicsDevice.Viewport);
        }
        public void Draw(VertexPositionColorTexture[] vertices, int verticesCount,
                    short[] indices, int indicesCount, Texture2D texture)
        {
            effect.Texture = texture;
            effect.TextureEnabled = (texture != null);

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                StartFrame();
                pass.Apply();

                GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColorTexture>(
                    PrimitiveType.TriangleList, vertices, 0, verticesCount, indices, 0, indicesCount / 3);
            }
        }
    }
}
