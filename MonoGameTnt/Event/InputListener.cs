
namespace ThanaNita.MonoGameTnt
{

    // สนใจ Event ที่เป็น MouseEvent และ KeyboardEvent
    public class InputListener : EventListener
    {
        public void handle(Event eventObj)
        {
            MouseEvent e = eventObj as MouseEvent;
            if (e == null)
                return;
        }
    }
}
