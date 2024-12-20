/*using var game = new Game09.Game09();
game.Run();*/
using Game09;

namespace Game09
{
    public static class Program
    {
        public static void Main()
        {
            using var game = new MenuState(); // เริ่มต้นที่เมนู
            game.Run();
        }
    }
}
