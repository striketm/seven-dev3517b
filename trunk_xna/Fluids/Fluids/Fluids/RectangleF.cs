using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Astro
{
     // Resumen:
    //     Almacena un conjunto de cuatro números de punto flotante que representan
    //     la posición y tamaño de un rectángulo. Para las funciones de región más avanzadas,
    //     utilice un objeto System.Drawing.Region.
    [Serializable]
    public struct RectangleF
    {
        // Resumen:
        //     Representa una instancia de la clase System.Drawing.RectangleF con los miembros
        //     sin inicializar.
        public static readonly RectangleF Empty;

        public float X;
        public float Y;
        public float Width;
        public float Height;

        public float Right { get { return X + Width; } }
        public float Bottom { get { return Y + Height; } }

        public RectangleF( float x, float y, float width, float height )
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

    }
}
