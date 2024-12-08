
using Microsoft.Xna.Framework;

namespace ThanaNita.MonoGameTnt
{
    public abstract class GenericButton : Actor
    {
        private RectF rawRect;
        public override RectF RawRect => rawRect;
        //------------------- Public Interface -------------------------------
        public delegate void ButtonClickedDelegate(GenericButton button);
        public event ButtonClickedDelegate ButtonClicked = delegate { };

        //------------------- Protected Interface ----------------------------
        protected enum ButtonState { Pressed, Highlight, Normal };
        protected abstract void UpdateVisual(ButtonState state);
        protected void InvokeClick()
        {
            ButtonClicked.Invoke(this); // ยิง event
        }
        protected GenericButton(Vector2 buttonSize)
        {
            rawRect = new RectF(Vector2.Zero, buttonSize);
        }

        //------------------- Private Implementation Details -----------------
        private bool bPressed = false; // กดโดนที่ปุ่ม
        private bool bHover = false;   // Mouse อยู่บริเวณปุ่ม

        public override void Act(float deltaTime)
        {
            base.Act(deltaTime);
            var events = GlobalMouseInfo.Value.GenerateEvents();
            foreach(var e in events)
            {
                if (e.Type == MouseEvent.EventType.MouseMoved)
                    MouseMoved(e);
                else if (e.Type == MouseEvent.EventType.MousePressed)
                    MouseButtonPressed(e);
                else if (e.Type == MouseEvent.EventType.MouseReleased)
                    MouseButtonReleased(e);
            }
        }
        private void MouseMoved(MouseEvent e)
        {
            //base.MouseMoved(e);
            var worldPoint = e.MousePosition;
            var localPoint = this.GlobalTransform.GetInverse().Transform(worldPoint);

            bHover = RawRect.Contains(localPoint);
            UpdateVisual();
        }
        private void MouseButtonPressed(MouseEvent e)
        {
            //base.MouseButtonPressed(e);
            if (e.Button != MouseButtons.Left)
                return;

            if (bHover)
                bPressed = true;

            UpdateVisual();
        }
        private void MouseButtonReleased(MouseEvent e)
        {
            //base.MouseButtonReleased(e);
            if (e.Button != MouseButtons.Left)
                return;

            if (bPressed && bHover)
                InvokeClick(); // ยิง event

            bPressed = false;
            UpdateVisual();
        }
        private void UpdateVisual()
        {
            ButtonState state;
            if (bHover && bPressed)
                state = ButtonState.Pressed;
            else if (bHover && !bPressed || bPressed && !bHover)
                state = ButtonState.Highlight;
            else
                state = ButtonState.Normal;

            UpdateVisual(state);
        }
        public void UpdateCurrentVisual()
        {
            UpdateVisual();
        }
    }
}
