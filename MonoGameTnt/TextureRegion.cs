using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace ThanaNita.MonoGameTnt
{
    public readonly struct TextureRegion
    {
        public Texture2D Texture { get; }
        public RectF Region { get; }

        public TextureRegion(Texture2D texture)
        {
            this.Texture = texture;
            this.Region = (RectF)texture.Bounds;
        }
        public TextureRegion(Texture2D texture, RectF region)
        {
            this.Texture = texture;
            this.Region = region;
        }

        public Vector2 Size { get => Region.Size; }

        public override string ToString() 
        {
            return $"[{Texture.Name},{Region}]";
        }
    }
}
