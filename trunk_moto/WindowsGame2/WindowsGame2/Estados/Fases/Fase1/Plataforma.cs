using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MotoGame.Estados.Fases.Fase1
{
    class Plataforma:Sprite
    {
        public Plataforma(ContentManager Content, GameWindow Window,  Vector2 posicaoInicial)
            :base(Content.Load<Texture2D>("Fases/Fase1/Textures/Plataforma"))
        {
            this.Window = Window;
            this.posicao = posicaoInicial;
           
        }
        public override void Update(GameTime gameTime)
        {
            
        }
        public void Update(GameTime gameTime, Vector2 aux)
        {
            posicao.X -= (int)(aux.X);
            posicao.Y -= (int)(aux.Y);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, new Rectangle((int)posicao.X, (int)posicao.Y, textura.Width, textura.Height), Color.White);
        }
    }
}
