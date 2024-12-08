using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ThanaNita.MonoGameTnt
{
    // Collision Detection Unit จะค้นหาทุก obj ใน tree ที่ implement interface นี้
    // แล้วเอามาเช็คการชนกัน
    //   - แจ้งการชนที่ OnCollide(...)
    // แบบ basic ประกอบด้วย shape เดียว
    public class CollisionObj : Actor
    {
        public CollisionRect Shape { get; private set; }
        public int GroupCode { get; set; } = 0;
        public List<CollisionObj> LastObjList = new List<CollisionObj>();
        public List<CollisionObj> CurrentObjList = new List<CollisionObj>();
        public bool DebugDraw { get; set; } = false;
        public delegate void OnCollideDelegate(CollisionObj objB, CollideData data);
        public OnCollideDelegate OnCollide = null;
        public Actor Actor { get; private set; }

        public CollisionObj(CollisionRect shape, Actor actor, int groupCode=0)
        {
            Shape = shape;
            shape.Parent = actor;
            Actor = actor;
            GroupCode = groupCode;
        }
        public void SetNewShape(CollisionRect shape)
        {
            Shape = shape;
            shape.Parent = Actor;
        }
        public static CollisionObj CreateWithRect(Actor actor, int groupCode=0)
        {
            return new CollisionObj(new CollisionRect(actor.RawRect), actor, groupCode);
        }
        public static CollisionObj CreateWithRect(Actor actor, RectF rawRect, int groupCode = 0)
        {
            return new CollisionObj(new CollisionRect(rawRect), actor, groupCode);
        }

        public void InvokeCollide(CollisionObj objB, CollideData collideData)
        {
            if (OnCollide != null)
            {
                collideData.objA = this;
                collideData.objB = objB;
                OnCollide(objB, collideData);
            }
        }

        public Vector2 RelativeDirection(RectF overlap)
        {
            return Shape.RelativeDirection(overlap);
        }
        public override void Draw(DrawTarget target, DrawState state)
        {
            if(DebugDraw)
                Shape.DebugDraw(target);
        }
    }
}
