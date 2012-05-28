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
using MotoGame.Estados.Menu;

namespace MotoGame.Estados.Intro
{
    class Menu : EstadoBase
    {
        Botao jogar;
        Botao sair;

        enum EstadoBotoes { JOGAR, SAIR, NENHUM};

        EstadoBotoes estadoBotoes = EstadoBotoes.NENHUM;

        public Menu(ContentManager Content, GameWindow Window)
            : base(Content, Window)
        {
            this.Window = Window;
            this.caminho = "Menu/";
            this.fundo = Content.Load<Texture2D>(caminho+"Textures/menu");
            this.jogar = new Botao(Content.Load<Texture2D>(caminho+"Textures/jogar"));
            this.jogar.Posicao = new Vector2(400, 15);
            
            this.sair = new Botao(Content.Load<Texture2D>(caminho+"Textures/sair"));
            this.sair.Posicao = new Vector2(400, 300);
        }

        public override void Update(GameTime gameTime)
        {
            jogar.Update(gameTime);
            sair.Update(gameTime);

            if (Game1.mouse_atual.LeftButton == ButtonState.Pressed)
            {
                if(jogar.Colisao.Contains(Game1.mouse_atual.X, Game1.mouse_atual.Y))
                {
                    Game1.estado_atual = Game1.Estado.FASE1;
                }
            }

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.fundo, new Rectangle(0, 0, this.Window.ClientBounds.Width, this.Window.ClientBounds.Height), Color.White);
            jogar.Draw(gameTime, spriteBatch);
            sair.Draw(gameTime, spriteBatch);
        }

    }
}
