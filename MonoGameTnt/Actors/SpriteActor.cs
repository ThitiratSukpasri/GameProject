using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ThanaNita.MonoGameTnt
{
    public class SpriteActor : Actor
    {
        private SpriteDrawable sprite;
        public override RectF RawRect => sprite.RawRect;

        public SpriteActor()
        {
            sprite = new SpriteDrawable();
        }
        public SpriteActor(Texture2D texture)
            : this(new TextureRegion(texture))
        {
        }
        public SpriteActor(TextureRegion region)
        {
            sprite = new SpriteDrawable(region);
        }
        public void SetTextureRegion(TextureRegion region)
        {
            sprite.SetTextureRegion(region);
        }
        public void SetTexture(Texture2D texture)
        {
            SetTextureRegion(new TextureRegion(texture));
        }

        protected override void DrawSelf(DrawTarget target, DrawState state)
        {
            base.DrawSelf(target, state);
            sprite.Draw(target, CombineState(state));
        }
    }
}
