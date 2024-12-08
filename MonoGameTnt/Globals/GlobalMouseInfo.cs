using Microsoft.Xna.Framework.Graphics;


namespace ThanaNita.MonoGameTnt
{
    public static class GlobalMouseInfo
    {
        private static MouseInfo storedValue = null;
        public static MouseInfo Value 
        {
            get => storedValue;
            set
            {
                GlobalUtil.AssertOneTimeSetter(storedValue, value, "GlobalMouseInfo");
                storedValue = value;
            }
        }
    }
}
