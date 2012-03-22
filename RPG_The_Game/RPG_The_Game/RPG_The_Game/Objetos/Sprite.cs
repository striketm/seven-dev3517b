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
        protected Rectangle origem;
        protected Rectangle destino;
        protected float rotacao;
        protected Vector2 pivo;
        protected bool direita;
        protected bool visivel;
        protected float camada;
        protected float alfa;
        protected Color cor;

        public struct animacao
        {
            public int quadro_X;//get set origem destino
            public int quadro_Y;
            public int qtd_quadros;
            public int quadros_seg;
            public int Y;
            
        }

        public Sprite(Texture2D textura)
        {
            this.textura = textura;
            this.posicao = new Vector2(0, 0);
            this.velocidade = new Vector2(1, 1);
            this.origem = new Rectangle(0, 0, textura.Width, textura.Height);
            this.destino = new Rectangle(0, 0, origem.Width, origem.Height);
            this.rotacao = 0;// MathHelper.ToRadians(0);
            this.pivo = Vector2.Zero;// Vector2(destino.Width / 2, destino.Height / 2);//influencia tudo...
            this.direita = true;
            this.visivel = true;
            this.camada = 1.0f;
            this.alfa = 1f;
            this.cor = new Color(1.0f, 1.0f, 1.0f, alfa);//not totally ok yet, anda see alpha blend in blendstate...
        }

        public abstract void Update(GameTime gameTime);

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, animacao _animacao)
        {
            if (visivel)
            {
                int frame = (int)(gameTime.TotalGameTime.TotalSeconds* _animacao.quadros_seg)%_animacao.qtd_quadros;

                spriteBatch.Draw(
                        textura,
                        new Rectangle(
                            (int)posicao.X,
                            (int)posicao.Y,
                            _animacao.quadro_X,
                            _animacao.quadro_Y),
                        new Rectangle(
                            frame*_animacao.quadro_X,
                            _animacao.Y,
                            _animacao.quadro_X,
                            _animacao.quadro_Y),
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