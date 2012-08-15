
#region File Description
//----------------------------------------------------------------------------------//
//                                                                                  //
//  Name : TextureNumber                                                            //
//                                                                                  //
//  Type : Visual C# Source File                                                    //
//                                                                                  //
//  Author : Hadi Mohammadi                                                         //
//                                                                                  //
//----------------------------------------------------------------------------------//
#endregion


#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
#endregion


namespace Xna.Auxiliary.Draws
{
    
    public class TextureNumber
    {

        #region Pablic Varibles
        public Color color = Color.Black;
        public int Space = 0;
        #endregion

        #region Private Varibles
        private SpriteBatch spriteBatch;
        private Texture2D m_texture;
        private int m_width;
        private int m_height;
        private Rectangle m_rect;
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        /// <param name="texture"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public TextureNumber(Game game, Texture2D texture, int width, int height)
        {

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(game.GraphicsDevice);

            // Define Private Varibles
            m_width = width;
            m_height = height;
            m_texture = texture;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="position"></param>
        public void Draw(Int64 number, Vector2 position)
        {

            String str = number.ToString();

            spriteBatch.Begin();

            for (int i = 0; i < str.Length; i++)
            {

                if (str[i] == '-')
                {
                    m_rect = new Rectangle(10 * m_width, 0, m_width, m_height);
                    spriteBatch.Draw(m_texture, position, m_rect, color);
                }
                else
                {
                    int value = str[i] - 48;
                    m_rect = new Rectangle(value * m_width, 0, m_width, m_height);
                    spriteBatch.Draw(m_texture, position, m_rect, color);
                }

                position.X += m_width + Space;

            }

            spriteBatch.End();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Draw(Int64 number, float x, float y)
        {

            Draw(number, new Vector2(x, y));

        }

    }

}