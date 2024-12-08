using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ThanaNita.MonoGameTnt
{
    // adapted from : MonoGame.Extended.ShapeExtensions
    public class WhitePixel
    {
        private Texture2D _whitePixelTexture;

        public WhitePixel(GraphicsDevice device)
        {
            if (_whitePixelTexture == null)
            {
                _whitePixelTexture = new Texture2D(device, 1, 1, mipmap: false, SurfaceFormat.Color);
                _whitePixelTexture.SetData(new Color[1] { Color.White });
            }
        }

        public Texture2D Get()
        {
            return _whitePixelTexture;
        }

    }
}
