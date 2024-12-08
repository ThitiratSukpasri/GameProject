using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;

namespace ThanaNita.MonoGameTnt
{
    public class Actor : Transformable, Drawable
    {
        public object UserObject { get; set; }
        public Actor Parent { get; private set; }
        protected List<Actor> Children { get; private set; } // อาจต้องทำ List แบบที่ Delay การ delete หรือ add ได้
        private List<EventListener> listeners;
        private List<EventListener> captureListeners;
        private List<Action> Actions;
        public virtual Color Color { get; set; } = Color.White;

        // Size & Rect
        public Vector2 RawSize => RawRect.Size;
        public virtual RectF RawRect => RectF.Zero; // default เป็น 0,0,0,0 แต่ละ actor ควรตั้งค่าของตัวเอง หากต้องการใช้งาน
        public RectF BoundingBox => GetMatrix().TransformRectAABB(RawRect);
        public RectF GlobalBoundingBox => GlobalTransform.TransformRectAABB(RawRect);

        public Matrix3 GlobalTransform
        {
            get
            {
                Matrix3 parentTransform;
                if (this.Parent == null) // กรณีเป็น root จะไม่มี parent
                    parentTransform = Matrix3.Identity;
                else
                    parentTransform = this.Parent.GlobalTransform;

                return parentTransform * this.GetMatrix();
            }
        }
        public int ChildCount { get => Children == null? 0: Children.Count; }
        public Actor GetChild(int index) 
        {
            if (Children == null)
                throw new System.Exception("Actor: No children.");
            return Children[index];
        }

        // การแยกว่าเป็น capture หรือไม่ capture phase ใน libgdx จะรับเป็น bool อีกตัวหนึ่ง
        public virtual void Handle(Event eventObj)
        {
            // todo: ในนี้ ต้องวิ่งวนส่ง event ให้ listeners
            // listener ทุกตัวได้ event หมด 
            // หาก stopped เป็น true จะไม่ propagate ต่อไปยัง parent
        }

        public void Insert(int index, Actor actor)
        {
            if (Children == null)
                Children = new List<Actor>();
            Children.Insert(index, actor);
            actor.Parent = this;
        }
        public void Add(Actor actor)
        {
            actor.Detach();

            if (Children == null)
                Children = new List<Actor>();
            Children.Add(actor);
            actor.Parent = this;
        }
        public void Remove(Actor actor)
        {
            if (Children == null)
                return;
            Children.Remove(actor);
            actor.Parent = null;
        }
        public void RemoveAt(int childIndex)
        {
            Debug.Assert(Children != null);
            Debug.Assert(childIndex >= 0 && childIndex < Children.Count);
            var child = Children[childIndex];
            Children.RemoveAt(childIndex);
            child.Parent = null;
        }
        public void Clear()
        {
            if (Children == null)
                return;
            for (int i = Children.Count - 1; i >= 0; --i)
            {
                var child = Children[i];
                Children.RemoveAt(i);
                child.Parent = null;
            }                
        }
        public void Detach()
        {
            if (Parent != null)
            {
                Parent.Remove(this);
                Parent = null;
            }
        }
        public void AddAction(Action action)
        {
            if (Actions == null)
                Actions = new List<Action>();
            Actions.Add(action);
        }
        public void RemoveAction(Action action)
        {
            if (Actions == null)
                return;
            Actions.Remove(action);
        }
        public void ClearAction()
        {
            if (Actions == null)
                return;
            Actions.Clear();
        }

        // จะเรียก children.act ก่อน หรือ actions.act ก่อนดี
        // น่าจะเรียก actions ตัวเองก่อน
        public virtual void Act(float deltaTime)
        {
            // 1. call actions
            if (Actions != null)
            {
                for (int i = 0; i < Actions.Count; i++)
                {
                    bool finished = Actions[i].Act(deltaTime);
                    if (finished)
                    {
                        Actions.RemoveAt(i);
                        i--;
                    }
                }
            }

            // 2. call children's Act
            if(Children != null)
            {
                for (int i = 0; i < Children.Count; i++)
                    Children[i].Act(deltaTime);
            }
        }

        // draw ตัวเอง ก่อน draw children
        public virtual void Draw(DrawTarget target, DrawState state)
        {
            DrawSelf(target, state);
            DrawChildren(target, CombineState(state));
        }

        // ปกติควร override DrawSelf. หากยังคงต้องการ DrawChildren หลังจาก draw self อยู่
        protected virtual void DrawSelf(DrawTarget target, DrawState state)
        { 
        }

        protected void DrawChildren(DrawTarget target, DrawState combined)
        {
            if (Children == null)
                return;

            // เหตุที่เอา Origin ออก (ทำให้ child อ้างถึงมุมบนซ้ายของ parent)
            // 1. ปัญหากรณี parent จะปรับ origin บ่อยๆ เช่นชิดซ้ายชิดขวา ใน Label
            // 2. ถ้ามีจะเปลือง performance กว่า เพราะต้องสร้าง matrix ใหม่ทุกครั้ง
            // หากอยากให้ child อยู่ศูนย์กลาง ให้ set child.Position = parent.Origin
            var state = combined;//.Combine(Matrix3.CreateTranslation(Origin));
            for (int i = 0; i < Children.Count; i++)
                Children[i].Draw(target, state);
        }

        protected DrawState CombineState(DrawState state)
        {
            return state.Combine(GetMatrix(), (ColorF)Color);
        }
    }
}
