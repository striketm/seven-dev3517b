using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Astro
{
    public static class Vector2Extension
    {
        public static Vector2 PerpendicularRight(this Vector2 A)
        {
            return new Vector2( -A.Y, A.X );
        }
    }
}
