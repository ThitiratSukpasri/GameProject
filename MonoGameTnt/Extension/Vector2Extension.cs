using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanaNita.MonoGameTnt
{
    public static class Vector2Extension
    {
        public static Vector2 GetDirection(this Vector2 v)
        {
            var direction = new Vector2();
            direction.X = v.X > 0 ? 1 : (v.X < 0 ? -1 : 0);
            direction.Y = v.Y > 0 ? 1 : (v.Y < 0 ? -1 : 0);
            return direction;
        }

    }
}
