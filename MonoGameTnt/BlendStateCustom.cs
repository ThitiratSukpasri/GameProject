using Microsoft.Xna.Framework.Graphics;

namespace ThanaNita.MonoGameTnt
{
    public static class BlendStateCustom
    {
        public static readonly BlendState Tinting;

        static BlendStateCustom()
        {
            Tinting = new BlendState()
            {
                ColorSourceBlend = Blend.Zero,
                ColorDestinationBlend = Blend.SourceColor,
                AlphaSourceBlend = Blend.Zero,
                AlphaDestinationBlend = Blend.One,
            };

        }
    }
}
