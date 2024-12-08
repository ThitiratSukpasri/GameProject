using Microsoft.Xna.Framework.Graphics;


namespace ThanaNita.MonoGameTnt
{
    public class GlobalGraphicsDeviceConfig
    {
        private static GraphicsDeviceConfig storedValue = null;
        public static GraphicsDeviceConfig Value 
        {
            get => storedValue;
            set
            {
                GlobalUtil.AssertOneTimeSetter(storedValue, value, "GlobalGraphicsDeviceConfig");
                storedValue = value;
            }
        }
    }
}
