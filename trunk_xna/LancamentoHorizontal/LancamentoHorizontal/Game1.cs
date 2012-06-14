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

namespace LancamentoHorizontal
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //  Informações da bola
        Texture2D texture;
        Vector2 position;
        Vector2 speed;
        // Informação do ambiente
        float gravity;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // Inicializa a posição da bola
            position = new Vector2(0.0f, 0.0f);
            // Configura o valor da gravidade
            gravity = 700.0f;
            // Configura a velocidade inicial em X e Y
            speed = new Vector2(100.0f, 0.0f);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Carrega a textura da bola
            texture = Content.Load<Texture2D>("ball");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            // Tempo passado desde a ultima atualização
            float time = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f);
            // Atualiza o movimento em X da bola
            position.X += speed.X * time;
            // Ataualiza a posição Y da bola
            // S = So * t + (g * t*t)/2
            position.Y += (speed.Y * time) + (gravity * (float)Math.Pow(time, 2)) * 0.5f;
            // Atualiza a velocidade Y do objeto ( v = vo + g * t )
            speed.Y += gravity * time;
            // Verfifica se atingiu a velocidade máxima é 600.0f
            speed.Y = (speed.Y > 600.0f) ? 600.0f : speed.Y;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            // Desenha a bola
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
