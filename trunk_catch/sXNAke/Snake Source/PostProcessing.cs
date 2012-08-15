
#region File Description
//----------------------------------------------------------------------------------//
//                                                                                  //
//  Name : PostProcessing                                                           //
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


namespace Xna.Auxiliary.Components
{

    public class PostProcessing : Microsoft.Xna.Framework.DrawableGameComponent
    {


        #region Public Varibles
        public bool Start_Level = false;
        public bool Start_Bomb = false;
        #endregion

        #region Private Varibles
        private Game m_game;
        private ResolveTexture2D m_screen;
        private Effect m_effect_level;
        private Effect m_effect_bomb;
        private SpriteBatch m_spriteBatch;
        private ContentManager m_content;
        private PresentationParameters m_pp;
        private long m_time_l, m_lasttime_l, m_currenttime_l;
        private long m_time_b, m_lasttime_b, m_currenttime_b;
        #endregion


        public PostProcessing(Game game)
            : base(game)
        {
            m_game = game;
        }


        public override void Initialize()
        {

            // define content manager
            m_content = new ContentManager(m_game.Services, "Content");

            // get pp from GraphicsDevice
            m_pp = m_game.GraphicsDevice.PresentationParameters;

            // init ResolveTexture2D
            m_screen = new ResolveTexture2D(
                m_game.GraphicsDevice, 
                m_pp.BackBufferWidth, 
                m_pp.BackBufferHeight, 
                1, 
                m_pp.BackBufferFormat);

            base.Initialize();
        }


        protected override void LoadContent()
        {

            m_spriteBatch = new SpriteBatch(m_game.GraphicsDevice);
            m_effect_level = m_content.Load<Effect>(@"Effects\Effect_Next_Level");
            m_effect_bomb = m_content.Load<Effect>(@"Effects\Effect_Bomb");
            
        }


        public override void Draw(GameTime gameTime)
        {

            m_game.GraphicsDevice.ResolveBackBuffer(m_screen);

            if (Start_Level == false) m_time_l = 0;
            else
            {
                m_currenttime_l = gameTime.TotalRealTime.Ticks;

                if (m_currenttime_l - m_lasttime_l >= 1) m_time_l++;

                EffectParameter ep = m_effect_level.Parameters["alpha"];
                
                if (m_time_l < 100)
                    ep.SetValue((float)m_time_l/100.0f);
                else
                    ep.SetValue((float)1.0f - (m_time_l / 200.0f));



                if (m_time_l >= 200)
                {
                    ep.SetValue(0);
                    Start_Level = false;
                }

                m_effect_level.CommitChanges();

                m_lasttime_l = m_currenttime_l;

            }


            if (Start_Bomb == false) m_time_b = 0;
            else
            {
                m_currenttime_b = gameTime.TotalRealTime.Ticks;

                if (m_currenttime_b - m_lasttime_b >= 1) m_time_b++;

                EffectParameter ep = m_effect_bomb.Parameters["alpha"];
                ep.SetValue((float)m_time_b / 200.0f);

                if (m_time_b >= 200)
                {
                    ep.SetValue(1.0f);
                    Start_Bomb = false;
                }

                m_effect_bomb.CommitChanges();

                m_lasttime_b = m_currenttime_b;

            }

            if (Start_Level)
            {

                m_effect_level.Begin();
                m_spriteBatch.Begin(SpriteBlendMode.None, SpriteSortMode.Immediate, SaveStateMode.SaveState);
                foreach (EffectPass pass in m_effect_level.CurrentTechnique.Passes)
                {
                    pass.Begin();
                    m_spriteBatch.Draw(m_screen, Vector2.Zero, Color.White);
                    pass.End();
                }
                m_spriteBatch.End();
                m_effect_level.End();

            }
            else if (Start_Bomb)
            {

                m_effect_bomb.Begin();
                m_spriteBatch.Begin(SpriteBlendMode.None, SpriteSortMode.Immediate, SaveStateMode.SaveState);
                foreach (EffectPass pass in m_effect_bomb.CurrentTechnique.Passes)
                {
                    pass.Begin();
                    m_spriteBatch.Draw(m_screen, Vector2.Zero, Color.White);
                    pass.End();
                }
                m_spriteBatch.End();
                m_effect_bomb.End();

            }
            else
            {

                m_spriteBatch.Begin(SpriteBlendMode.None, SpriteSortMode.Immediate, SaveStateMode.SaveState);
                m_spriteBatch.Draw(m_screen, Vector2.Zero, Color.White);
                m_spriteBatch.End();

            }
            


            base.Draw(gameTime);

        }


    }

}