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
    class Energia:ObjetoJogo
    {
        public bool Recolhida { get; set; }

        public static List<Energia> listaEnergias = new List<Energia>();

        public static int QTDTotal = 5;

        public Energia(ContentManager Content, string _modelo)
            : base()
        {
            Recolhida = false;
            Modelo = Content.Load<Model>(_modelo);

            Energia.listaEnergias.Add(this);

            base.AtualizarMundoEColisoes();
            
        }


    }
}
