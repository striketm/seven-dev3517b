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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace LancamentoFlecha
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Vector2 posicao;            // posicao da flecha
        private float angulo;               // angulo de lançamento da flecha
        private float orientacao;           // orientacao da flecha
        private Texture2D texturaFlecha;    // imagem usada na fleca
        private Vector2 vetor;              // vetor de direcao da flecha
        private float velocidade;           // velocidade
        private float gravidade;            // gravidade

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            gravidade = 0.05f;                               
            angulo = 45 * (float)Math.PI / 180; // Convertando o angulo em graus para radianos
            velocidade = 7.0f;
            vetor = new Vector2();
            vetor.X = velocidade * (float)Math.Cos(angulo);
            vetor.Y = velocidade * (float)-Math.Sin(angulo);
            posicao = new Vector2(0, 450);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            texturaFlecha = Content.Load<Texture2D>(@"flecha");
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
          
        }

        protected override void Update(GameTime gameTime)
        {
            // Calculo da próxima posição
            Vector2 novaPosicao = new Vector2();
            novaPosicao.X = posicao.X + vetor.X;
            novaPosicao.Y = posicao.Y + vetor.Y;
            // Somando a gravidade em Y
            vetor.Y = vetor.Y + gravidade;
            // Colocando uma resistencia do ar na flecha
            Vector2 resistenciaAr = new Vector2();
            resistenciaAr.X = 0.01f * (float)Math.Cos(orientacao);
            resistenciaAr.Y = 0.01f * (float)-Math.Sin(orientacao);
            // Subtraindo a resistencia do ar do nosso vetor direcao
            vetor.X = vetor.X - resistenciaAr.X;
            vetor.Y = vetor.Y + resistenciaAr.Y;
            // Verificando a orientacao da flecha
            orientacao = (float)Math.Atan2(novaPosicao.Y - posicao.Y,
                novaPosicao.X - posicao.X);
            // Atribuindo a nova posição a posição da flecha
            posicao = novaPosicao;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            // Desenhando a flecha
            spriteBatch.Draw(texturaFlecha,
                posicao,
                new Rectangle(0, 0, texturaFlecha.Width, texturaFlecha.Height),
                Color.White,
                orientacao,
                new Vector2(texturaFlecha.Width, texturaFlecha.Height / 2),
                1,
                SpriteEffects.None,
                0);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
