using Microsoft.Xna.Framework;

namespace ThanaNita.MonoGameTnt
{
    // concrete obj ของ CollisionShape
    // สิ่งที่ต้องมีให้คือ GlobalTransformProvider 
    // สิ่งที่มีให้คือ บอกได้ว่าชนกับ shape อื่นหรือไม่
    public class CollisionRect
    {
        private RectF rect;
        public Actor Parent { get; set; }
        private RectF GlobalRect => Parent!.GlobalTransform.TransformRectAABB(rect);

        public CollisionRect(RectF rect)
        {
            this.rect = rect;
            this.Parent = null; // จะถูกตั้งค่าจาก CollisionObj
        }
        public bool IsCollide(CollisionRect shapeB, out CollideData collideData)
        {
            var rectA = this.GlobalRect;
            var rectB = shapeB.GlobalRect;

            collideData = new CollideData();
            return rectA.Intersects(rectB, out collideData.OverlapRect);
        }

        public Vector2 RelativeDirection(RectF overlap)
        {
            return RelativeDirectionSingle(GlobalRect, overlap);
        }

        // return (0, 0) or (1, 1) or (1, 0) or (-1, 0) or ...
        private static Vector2 RelativeDirection(RectF a, RectF overlap)
        {
            var point = overlap.CenterPoint - a.CenterPoint;
            return point.GetDirection();
        }
        // return up (0,-1), down (0,1), left(-1,0), right(1,0) or none (0,0)
        private static Vector2 RelativeDirectionSingle(RectF a, RectF overlap)
        {
            //if(overlap.hittest(a.center))
            //    return 0,0;
            var point = overlap.CenterPoint - a.CenterPoint;
            if (point.Y * a.Width > a.Height * point.X) // down or left
                if (point.Y * a.Width > -a.Height * point.X) // down or right
                    return new Vector2(0, 1); // down
                else
                    return new Vector2(-1, 0); // left
            else
                if (point.Y * a.Width > -a.Height * point.X) // down or right
                    return new Vector2(1, 0); // right
                else
                    return new Vector2(0, -1); // up

        }

        // จะไม่สนใจค่า states.Transform เพราะจะวาดใน global coordinate อยู่แล้ว
        public void DebugDraw(DrawTarget target)
        {
            var shape = new HollowRectActor(Color.Red, 1, GlobalRect.CreateExpand(-0.5f)); 
            shape.Draw(target, DrawState.Identity);
        }
    }
}
