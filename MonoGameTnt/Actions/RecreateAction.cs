using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanaNita.MonoGameTnt
{
    // ตัวครอบ Action โดยจะสร้าง Action แล้วส่งต่องานไปยัง action นั้น
    // หาก restart ก็จะสร้างใหม่
    // จะเรียก creationFunc เพื่อสร้าง Action เมื่อเริ่มต้น และเมื่อ restart แล้วเข้า Act() ครั้งแรก
    public class RecreateAction : Action
    {
        public delegate Action CreateFunc();
        private CreateFunc creationFunc;
        private bool created = false;
        private Action action = null;
        private bool finished = false;
        public bool IsFinished() => finished;

        public RecreateAction(CreateFunc creationFunc)
        {
            this.creationFunc = creationFunc;
            Restart();
        }

        public bool Act(float deltaTime)
        {
            if(!created)
            {
                action = creationFunc();
                created = true;
            }

            finished = action.Act(deltaTime);
            return finished;
        }

        public void Restart()
        {
            created = false;
            action = null;
        }
    }
}
