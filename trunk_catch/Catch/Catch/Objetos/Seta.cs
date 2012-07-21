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
    class Seta : Sprite
    {
        public Seta(Texture2D textura)
            :base(textura)
        {

        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (visivel)
            {
               // int frame = (int)(gameTime.TotalGameTime.TotalSeconds * _animacao.quadros_seg) % _animacao.qtd_quadros;

                spriteBatch.Draw(
                        textura,
                        new Rectangle(
                            (int)posicao.X,
                            (int)posicao.Y,
                            60,//_animacao.quadro_X,
                            60),//_animacao.quadro_Y),
                        new Rectangle(
                            0,//frame * _animacao.quadro_X,
                            0,//_animacao.Y_inicial,
                            60,//_animacao.quadro_X,
                            60),//_animacao.quadro_Y),
                        new Color(
                            1.0f * alfa,
                            1.0f * alfa,
                            1.0f * alfa,
                            alfa),
                        rotacao,
                        pivo,
                        (direita) ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                        camada);
            }
        }
    }
}
