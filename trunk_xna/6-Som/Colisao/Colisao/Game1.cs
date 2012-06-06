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

namespace Colisao
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D caveira;
        Vector2 vetorcaveira;
        Vector2 velocidadecaveira = new Vector2(6, 6);

        Texture2D nave;
        Vector2 vetornave;
       
        
        SpriteFont fonte;
        int pontos;
        int numaleatorio;

        AudioEngine motoraudio;
        WaveBank bancowave;
        SoundBank bancosom;
        Cue somfundo;
        Cue somcolisao;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

             base.Initialize();
        }

        /// <summary>
        /// Gera um numero aleatório
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private int GeraAleatorio(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }



        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            caveira = Content.Load<Texture2D>(@"Imagens\Caveira");
            nave = Content.Load<Texture2D>(@"Imagens\NaveEspacial");
            fonte = Content.Load<SpriteFont>(@"Fontes\SpriteFont1");

            vetorcaveira = new Vector2(10, 10);
            vetornave = new Vector2(Window.ClientBounds.Width / 2 - nave.Width / 2, Window.ClientBounds.Height / 2 - nave.Height / 2);


            motoraudio = new AudioEngine(@"Content\Audio\AudioColisao.xgs");
            bancowave = new WaveBank(motoraudio, @"Content\Audio\Wave Bank.xwb");
            bancosom = new SoundBank(motoraudio, @"Content\Audio\Sound Bank.xsb");

           
            somfundo = bancosom.GetCue("1234_Rock_it");
            somfundo.Play();
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            nave.Dispose();
            caveira.Dispose();
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState teclado = Keyboard.GetState();
            //Verifica se alguma tecla de movimento esta pressionada
            if (teclado.IsKeyDown(Keys.Left))
                   vetornave.X -= 3;
            if (teclado.IsKeyDown(Keys.Right))
                    vetornave.X += 3;
            if (teclado.IsKeyDown(Keys.Up))
                    vetornave.Y -= 3;
            if (teclado.IsKeyDown(Keys.Down))
                     vetornave.Y += 3;
         
            //detecta limites horizontais da tela e inverte a velocidade
            if (vetorcaveira.X > Window.ClientBounds.Width - caveira.Width || vetorcaveira.X < 0)
                velocidadecaveira.X *= -1;
            //detecta limites verticais da tela e inverte a velciodade
            if (vetorcaveira.Y > Window.ClientBounds.Height - caveira.Height || vetorcaveira.Y < 0)
                velocidadecaveira.Y *= -1;

            //Muda a direção da caveira, aleatóriamente
            numaleatorio = GeraAleatorio(1, 100);
            switch (numaleatorio)
            {   case 2:
                    velocidadecaveira.Y *= -1;
                    break;
                case 3:
                    velocidadecaveira.X *= -1;
                    break;
                case 4:
                    velocidadecaveira.Y *= -1;
                    velocidadecaveira.X *= -1;
                    break;
            }
          
             //atualiza a posição do sprite
            vetorcaveira.X += velocidadecaveira.X;
            vetorcaveira.Y += velocidadecaveira.Y;

            //detecta colisão, reduz pontos em caso de verdadeiro, 
            //incrementa em caso negativo
            if (DetectaColisao())
            {
                pontos -= 15;
                if (somcolisao == null || somcolisao.IsStopped)
                {
                    somcolisao = bancosom.GetCue("Explosao");
                    somcolisao.Play();
                } 
            }
            else
            pontos +=1;

            base.Update(gameTime);
        }
    
        /// <summary>
        /// Detecção de Colisão
        /// </summary>
        /// <returns></returns>
        public bool DetectaColisao()
        {
         if (vetorcaveira.X + caveira.Width > vetornave.X &&
         vetorcaveira.X < vetornave.X + nave.Width &&
         vetorcaveira.Y + caveira.Height > vetornave.Y &&
         vetorcaveira.Y < vetornave.Y + nave.Width)
                return true;
            else
                return false;
        }  


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

           spriteBatch.Begin();
           spriteBatch.Draw(nave, vetornave,Color.White);
           spriteBatch.Draw(caveira, vetorcaveira, Color.White);
           spriteBatch.DrawString(fonte,  pontos.ToString(), Vector2.Zero, Color.White);
           spriteBatch.End();
    

            base.Draw(gameTime);
        }
    }
}
