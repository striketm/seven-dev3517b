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

namespace ColisaoPorPixel
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        // A imagem
        Texture2D texturaMoto;
        // Os dados de cores para as imagens, usado para colis�o por pixel
        Color[] dadosTexturaMoto1;////////////////////////
        Color[] dadosTexturaMoto2;///////////////////////
        // Posicao 
        Vector2 posicaoMoto1;//////////
        Vector2 posicaoMoto2;/////////
        // Constante de velocidade 
        const int velocidadeMoto = 2;
// Variavel para quando uma colis�o for detectada
        bool motoAtingida = false;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            //Posicao inicial 
            posicaoMoto1 = new Vector2(10, 300);
            posicaoMoto2 = new Vector2(757, 300);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Carrega a textura 
            texturaMoto = Content.Load<Texture2D>("moto");
            // Extrai os dados para colis�o 
            dadosTexturaMoto1 =
                new Color[texturaMoto.Width * texturaMoto.Height];
            //guarda no array
            texturaMoto.GetData(dadosTexturaMoto1);
                        
            dadosTexturaMoto2 =
                new Color[texturaMoto.Width * texturaMoto.Height];
            texturaMoto.GetData(dadosTexturaMoto2);
        }
        protected override void UnloadContent()
        {
            
        }
        protected override void Update(GameTime gameTime)
        {
            // Pegando o estado do teclado
            KeyboardState teclado = Keyboard.GetState();
            //1
            #region
            if (teclado.IsKeyDown(Keys.D))
                posicaoMoto1.X += velocidadeMoto;
            if (teclado.IsKeyDown(Keys.A))
                posicaoMoto1.X -= velocidadeMoto;
            if (teclado.IsKeyDown(Keys.W))
                posicaoMoto1.Y -= velocidadeMoto;
            if (teclado.IsKeyDown(Keys.S))
                posicaoMoto1.Y += velocidadeMoto;
            #endregion
            //  2
            #region
            if (teclado.IsKeyDown(Keys.Right))
                posicaoMoto2.X += velocidadeMoto;
            if (teclado.IsKeyDown(Keys.Left))
                posicaoMoto2.X -= velocidadeMoto;
            if (teclado.IsKeyDown(Keys.Up))
                posicaoMoto2.Y -= velocidadeMoto;
            if (teclado.IsKeyDown(Keys.Down))
                posicaoMoto2.Y += velocidadeMoto;
            #endregion

            // Define o retangulo delimitador  1
            Rectangle RetanguloMoto1 =
                new Rectangle((int)posicaoMoto1.X, (int)posicaoMoto1.Y,
                texturaMoto.Width, texturaMoto.Height);

            // Define o retangulo delimitador 2
            Rectangle RetanguloMoto2 =
                new Rectangle((int)posicaoMoto2.X, (int)posicaoMoto2.Y,
                texturaMoto.Width, texturaMoto.Height);

            motoAtingida = false;

            if (ColisaoPorPixel(RetanguloMoto1, dadosTexturaMoto1, RetanguloMoto2, dadosTexturaMoto2))
            {
                motoAtingida = true;
            }

            if (motoAtingida)
                Window.Title = "bateu";
            else
                Window.Title = "n�o bateu";

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            // Desenhando 
            spriteBatch.Draw(texturaMoto, posicaoMoto1, Color.White);

            spriteBatch.Draw(texturaMoto, posicaoMoto2, Color.Red);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        // Determina se h� sobreposi��o dos pixels n�o-transparentes entre os dois retangulos
        public bool ColisaoPorPixel(Rectangle retangulo1, Color[] dados1, Rectangle retangulo2, Color[] dados2)
        {
            // Encontra os limites do retangulo de interse��o
            int cima = Math.Max(retangulo1.Top, retangulo2.Top);
            int baixo = Math.Min(retangulo1.Bottom, retangulo2.Bottom);
            int esquerda = Math.Max(retangulo1.Left, retangulo2.Left);
            int direita = Math.Min(retangulo1.Right, retangulo2.Right);

            // Verifica todos os pontos dentro do limite de interese��o 
            //TODO verificar se os retangulos estao um ao lado do outro
            for (int y = cima; y < baixo; y++)
            {
                for (int x = esquerda; x < direita; x++)
                {
                    // Verifica a cor de ambos os pixels neste momento
                    Color color1 = dados1[(x - retangulo1.Left) +
                    (y - retangulo1.Top) * retangulo1.Width];
                    Color color2 = dados2[(x - retangulo2.Left) +
                    (y - retangulo2.Top) * retangulo2.Width];

                    // Se ambos os p�xels n�o s�o completamente diferentes
                    if (color1.A != 0 && color2.A != 0)
                    {
                        // Um cruzamento de pixel foi encontrado
                        return true;
                    }
                }
            }
            // N�o foi encontrado cruzamento entre os pixels
            return false;
        }
    }
}
