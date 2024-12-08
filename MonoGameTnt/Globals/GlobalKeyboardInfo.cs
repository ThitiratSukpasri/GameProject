using Microsoft.Xna.Framework.Graphics;


namespace ThanaNita.MonoGameTnt
{
    public static class GlobalKeyboardInfo
    {
        private static KeyboardInfo storedValue = null;
        public static KeyboardInfo Value 
        {
            get => storedValue;
            set
            {
                GlobalUtil.AssertOneTimeSetter(storedValue, value, "GlobalKeyboardInfo");
                storedValue = value;
            }
        }
    }
}
