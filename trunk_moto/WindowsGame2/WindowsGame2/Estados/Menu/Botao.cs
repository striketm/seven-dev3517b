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

namespace MotoGame.Estados.Menu
{
    class Botao:Sprite
    {
        animacao mouse_over;
        animacao mouse_up;
        animacao mouse_click;
        //animacao animacao_atual;

        public Botao(Texture2D textura)
            :base(textura)
        {
            mouse_over.qtd_quadros = 2;
            mouse_over.quadros_seg = 1;
            mouse_over.quadro_X = 186;
            mouse_over.quadro_Y = 106;
            mouse_over.Y_inicial = 0;
            //mouse_over.X_inicial = 0;

            mouse_up.qtd_quadros = 1;
            mouse_up.quadros_seg = 1;
            mouse_up.quadro_X = 186;
            mouse_up.quadro_Y = 106;
            mouse_up.Y_inicial = 106;
            //mouse_up.X_inicial = mouse_up.quadro_X;

            animacao_atual = mouse_over;
        }

        public override void Update(GameTime gameTime)
        {
            if (this.Colisao.Contains(Game1.mouse_atual.X, Game1.mouse_atual.Y))
            {
                //Console.WriteLine("in");
                animacao_atual = mouse_up;
            }
            else
            {
                //Console.WriteLine("out");
                animacao_atual = mouse_over;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch, animacao_atual);
        }
    }
}
