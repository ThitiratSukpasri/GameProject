using Microsoft.Xna.Framework;

namespace ThanaNita.MonoGameTnt
{
    public class Event
    {
        //public bool CapturePhase { get; } = false; // ช่วงต้นอาจยังไม่ทำ
        public Actor Target { get; internal set; } // should be set by Event Dispatcher

        // Read / Write
        public bool Handled { get; set; } = false;  // สื่อสารระหว่าง actor ด้วยกัน ว่ามีการ process event แล้ว (ทั้งสองฝ่ายต้องตกลงสื่อสารกันด้วยตัวนี้)
        public bool Stopped { get; set; } = false;  // สื่อสารกับ dispatcher ว่าไม่ต้องส่งต่อยัง actor อื่นๆ
    }

    public class MouseEvent : Event
    {
        public enum EventType { MousePressed, MouseReleased, MouseMoved, MouseScrolled };
        public EventType Type { get; }
        public MouseButtons Button { get; } = MouseButtons.None; // จะมีค่าเฉพาะกรณี MouseDown กับ MouseUp
        public Vector2 MousePosition { get; } // ในรูป World Coordinate // มีค่าเสมอ
        public int DeltaScroll { get; } // มีค่าเสมอ  ถ้า frame นั้นไม่ scrolled, จะเป็น 0

        /*public MouseEvent(EventType type, Vector2 mousePosition)
            : this(type, mousePosition, 0)
        {
        }*/
        // กรณี Moved, Scrolled ตัว button ควรเป็น None
        // button ต้องตั้งค่าเข้ามา กรณี pressed, released
        public MouseEvent(EventType type, Vector2 mousePosition, int deltaScroll, MouseButtons button)
        {
            Type = type;
            MousePosition = mousePosition;
            DeltaScroll = deltaScroll;
            Button = button;
        }
    }

    public interface EventListener
    {
        void handle(Event e);
    }

}
