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

namespace TutorialLancamentoVertical
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //  Informações da bola
        Texture2D imagem;
        Vector2 posicao;
        Vector2 velocidade;
        // Informação do ambiente (gravidade = g)
        float gravidade;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            Window.Title = "Tutorial de Lançamento Vertical - http://www.pontov.com.br";
        }

        protected override void Initialize()
        {
            // Inicializa a posição da bola
            posicao = new Vector2(360.0f, 500.0f);
            // Configura o valor da gravidade
            gravidade = 400.0f;
            // Configura a velocidade inicial em X e Y
            velocidade = new Vector2(0.0f, -440.0f);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Carrega a textura da bola
            imagem = Content.Load<Texture2D>(@"ball");
        }

        protected override void Update(GameTime gameTime)
        {
            // Tempo passado desde a ultima atualização
            float tempo = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f);
            // Atualiza a posição Y da bola
            posicao += velocidade * tempo;
            // Se não atingiu a velocidade máxima, sua gravidade (h = vo * t + (g * t*t)/2)
            posicao.Y += (velocidade.Y * tempo) + (gravidade * (float)Math.Pow(tempo, 2)) / 2.0f;
            // Atualiza a velocidade Y do objeto ( v = vo + g * t )
            velocidade.Y += gravidade * tempo;
            // A velocidade máxima é 300.0f
            velocidade.Y = (velocidade.Y > 300.0f) ? 300.0f : velocidade.Y;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            // Desenha a bola
            spriteBatch.Draw(imagem, posicao, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
