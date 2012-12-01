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

namespace SevenInvaders
{
    /// <summary>
    /// Esta classe vai servir para gerenciar todos os inimigos de modo geral independente de seu tipo
    /// Cada tipo terá uma imagem diferente e poderá ter diferentes pontos de vida e pontuação, e etc...
    /// </summary>
    abstract class Enemy:Sprite
    {
        public Enemy(Texture2D texture):base(texture)
        {

        }

        public override void Update(GameTime gameTime)
        {
         
        }
    }
}
