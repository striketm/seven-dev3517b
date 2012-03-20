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
    abstract class Sprite
    {
        protected GameWindow window;
        protected Texture2D textura;
        protected Vector2 posicao;
        protected Vector2 velocidade;
        protected bool visivel;
        public float camada;

        public Sprite(Texture2D textura)
        {
            this.textura = textura;
            this.posicao = new Vector2(0, 0);
            this.velocidade = new Vector2(1, 1);
            this.visivel = true;
            //this.camada = 1;
        }

        public abstract void Update(GameTime gameTime);

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //if (visivel) spriteBatch.Draw(textura, posicao, Color.White);
            if (visivel)
                // 1- spriteBatch.Draw(textura, retanguloDestino, cor);
                // 2- spriteBatch.Draw(textura, vetorPosicaoDestino, cor);
                // 3 - spriteBatch.Draw(textura, retanguloDestino, retanguloFonte, cor);
                // 4 - spriteBatch.Draw(textura, vetorPosicaoDestino, retanguloFonte, cor);
                // 5 - spriteBatch.Draw(textura, retanguloDestino, retanguloFonte, cor, floatRotacao, vetorOrigem, SpriteEffects.None, floatCamada);
                // 6 - spriteBatch.Draw(textura, vetorPosicaoDestino, retanguloFonte, cor, floatRotacao, vetorOrigem, SpriteEffects.FlipHorizontally, floatCamada);
                // 7 - spriteBatch.Draw(textura, vetorPosicaoDestino, retanguloFonte, cor, floatRotacao, vetorOrigem, vetorEscala, SpriteEffects.FlipVertically, floatCamada);
                spriteBatch.Draw(textura, new Rectangle((int)posicao.X, (int)posicao.Y, 50,50), new Rectangle(0, 0, 50, 50), Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, camada);
        }
    }
}