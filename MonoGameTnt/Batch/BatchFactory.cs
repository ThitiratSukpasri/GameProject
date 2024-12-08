using Microsoft.Xna.Framework.Graphics;

namespace ThanaNita.MonoGameTnt
{
    public class BatchFactory
    {
        public static GenericBatch Create(GraphicsDevice device)
        {
            var adapter = new SpriteEffectAdapter(device);
            var batch = new GenericBatch(adapter);
            return batch;
        }
    }
}
