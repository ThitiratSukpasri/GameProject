

namespace ThanaNita.MonoGameTnt
{
    // ไม่ต้องสั่ง start() เพราะพอ add เข้า Actor จะถือว่า start ทันที
    // ไม่ต้อง remove ออก เพราะเมื่อจบ Actor จะ remove ออกให้อัตโนมัติ
    public interface Action
    {
        bool Act(float deltaTime);
        void Restart();
        bool IsFinished();
    }
}
