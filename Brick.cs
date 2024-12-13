using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanaNita.MonoGameTnt;

namespace Game09
{
    public class Brick :SpriteActor
    {
        public Brick(RectF rect)
        {
            var texture = TextureCache.Get("brick2resize.png");
            SetTextureRegion(new TextureRegion(texture, new RectF(Vector2.Zero,rect.Size)));
            Position = rect.Position;
            var collisionObj = CollisionObj.CreateWithRect(this, 2);
            //collisionObj.Debug = true;
            Add(collisionObj);
        }
    }
}
