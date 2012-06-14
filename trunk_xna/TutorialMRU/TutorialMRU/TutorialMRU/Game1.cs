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

namespace TutorialMRU
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        // Informações do carro (pode ser colocado em uma classe específição)
        private Vector2 posicao;
        private float velocidade;
        private Texture2D imagem;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
        }

        protected override void Initialize()
        {
            // Posição inicial do carro
            posicao = new Vector2(0.0f, 300.0f);
            // Configura a velocidade do carro
            velocidade = 150.0f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Carrega a textura do carro (car.png)
            imagem = Content.Load<Texture2D>("car");
        }

        protected override void Update(GameTime gameTime)
        {
            // Tempo passado desde a ultima atualização
            float tempo = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / 1000);
            // Atualiza a posição do carro
            // S = So + V * T
            posicao.X += velocidade * tempo;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            // Desenha o carro
            spriteBatch.Draw(imagem, posicao, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
