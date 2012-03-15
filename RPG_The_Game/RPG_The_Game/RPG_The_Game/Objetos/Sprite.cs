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


        public Sprite(Texture2D textura)
        {

            this.textura = textura;
            this.posicao = new Vector2(0,0);
            this.velocidade = new Vector2(1, 1);
            this.visivel = true;
                             
        
        }
        public abstract void Update(GameTime gameTime);
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
       {

           if (visivel) spriteBatch.Draw(textura, posicao, Color.White);
       }  


    }


}



