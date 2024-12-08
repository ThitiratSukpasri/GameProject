using Microsoft.Xna.Framework.Graphics;


namespace ThanaNita.MonoGameTnt
{
    // ตัวนี้สามารถ set Logger เป็นตัวใหม่ได้เรื่อยๆ ไม่จำเป็นว่าต้อง set ครั้งเดียวแล้วใช้ตัวเดิมไปตลอด
    // default ใช้ตัว DebugConsoleLogger
    public static class GlobalLogger
    {
        public static Logger Value { get; set; } = new DebugConsoleLogger();
    }
}
