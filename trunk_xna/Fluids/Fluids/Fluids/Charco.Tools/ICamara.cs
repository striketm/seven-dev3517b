using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Charco.Tools
{
    public interface ICamara
    {
        Matrix View { get; }
        Matrix Projection { get; }
        Matrix ViewProjection { get; }        
    }
}
