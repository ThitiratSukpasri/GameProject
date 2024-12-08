using FontStashSharp;
using System.Collections.Generic;
using System.IO;

namespace ThanaNita.MonoGameTnt
{
    public class FontCache
    {
        private static Dictionary<string, FontSystem> cache = new Dictionary<string, FontSystem>();
        public static FontSystem Get(string fontPathAndName)
        {
            FontSystem fontSystem;
            if (cache.TryGetValue(fontPathAndName, out fontSystem))
                return fontSystem;
            fontSystem = new FontSystem();
            fontSystem.AddFont(File.ReadAllBytes(fontPathAndName));
            cache[fontPathAndName] = fontSystem;
            return fontSystem;
        }
    }
}
