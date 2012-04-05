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

namespace RPG_The_Game.Objetos
{
    class Cachorro:Sprite
    {
        public static Random random = new Random();

        public static List<Cachorro> listaCachorros = new List<Cachorro>();
        
        public static animacao nenhuma;

        public Cachorro(Texture2D textura)
            :base(textura)
        {
            posicao.X = random.Next(100, 600);
            posicao.Y = random.Next(100, 600);

            nenhuma = new animacao();
            nenhuma.qtd_quadros = 1;
            nenhuma.quadros_seg = 1;
            nenhuma.Y = 0;
            nenhuma.quadro_X = textura.Width / nenhuma.qtd_quadros;
            nenhuma.quadro_Y = textura.Height;
        }

        public override void Update(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }
    }
}
