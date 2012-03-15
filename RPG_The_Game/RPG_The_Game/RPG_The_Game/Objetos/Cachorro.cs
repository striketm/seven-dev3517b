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

        public Cachorro(Texture2D textura)
            :base(textura)
        {
            posicao.X = random.Next(100, 600);
            posicao.Y = random.Next(100, 600);
        }

        public override void Update(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }
    }
}
