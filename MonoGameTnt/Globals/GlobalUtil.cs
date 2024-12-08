using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ThanaNita.MonoGameTnt
{
    public static class GlobalUtil
    {

        public static void AssertOneTimeSetter(object storedValue, object newValue, string fieldName)
        {
            if (storedValue != null)
                throw new Exception($"{fieldName} should be set only once.");
            if (newValue == null)
                throw new Exception($"{fieldName} set value should not be null.");
        }

    }
}
