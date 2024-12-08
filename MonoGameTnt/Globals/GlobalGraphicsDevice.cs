using Microsoft.Xna.Framework.Graphics;


namespace ThanaNita.MonoGameTnt
{
    public class GlobalGraphicsDevice
    {
        private static GraphicsDevice storedValue = null;
        public static GraphicsDevice Value 
        {
            get => storedValue;
            set
            {
                GlobalUtil.AssertOneTimeSetter(storedValue, value, "GlobalGraphicsDevice");
                storedValue = value;
            }
        }
    }
}
