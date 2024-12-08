using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanaNita.MonoGameTnt;

namespace ThanaNita.MonoGameTnt
{
    // - Enable/Disable ได้
    // - ตัวมันเอง เป็น infinite action
    // - ส่วน action ที่มันไปเรียก  หากจบแล้ว มันจะ remove ออกให้ และ set Action เป็น null
    public class PlaceHolderAction : Action
    {
        Action Action { get; set; } = null;
        public bool Enable { get; set; } = true;

        public PlaceHolderAction(Action action)
        {
            Action = action;
        }
        public PlaceHolderAction()
        {
        }

        public bool Act(float deltaTime)
        {
            if (Enable && Action != null)
            {
                var finished = Action.Act(deltaTime);
                if(finished)
                    Action = null;
            }

            return false;
        }
        public bool IsFinished() => false;

        public void Restart()
        {
            Action?.Restart();
        }
    }
}
