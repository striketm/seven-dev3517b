using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using CapturarObjetos.Nucleo;

namespace CapturarObjetos.Objetos
{
    class Barreira : ObjetoJogo
    {
        public string Tipo { get; set; }

        public static List<Barreira> listaBarreiras = new List<Barreira>();

        public Barreira(ContentManager Content, string _modelo)
            : base()
        {
            Modelo = Content.Load<Model>(_modelo);
            Tipo = _modelo;

            Barreira.listaBarreiras.Add(this);
        }

        
    }
}
