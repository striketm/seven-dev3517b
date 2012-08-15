
#region File Description
//----------------------------------------------------------------------------------//
//                                                                                  //
//  Name : Cursor                                                                   //
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

    public class Cursor : Microsoft.Xna.Framework.DrawableGameComponent
    {

        #region Public Varibles
        public Texture2D    Texture;
        public Rectangle    RestrictZone;
        public Color        color           = Color.White;
        public bool         Blink           = false;
        public float        BlinkSpeed      = 1.0f;
        public Vector2      Position;
        #endregion

        #region Private Varibles
        private SpriteBatch spriteBatch;
        private MouseState State;
        #endregion


        public Cursor(Game game) : base(game)
        {
        }

        
        public override void Initialize()
        {
            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        
        public override void Update(GameTime gameTime)
        {

            State = Mouse.GetState();

            Position.X = State.X;
            Position.Y = State.Y;

            if (State.X < RestrictZone.X) Position.X = RestrictZone.X;
            if (State.Y < RestrictZone.Y) Position.Y = RestrictZone.Y;
            if (State.X > RestrictZone.Width) Position.X = RestrictZone.Width;
            if (State.Y > RestrictZone.Height) Position.Y = RestrictZone.Height;

            base.Update(gameTime);

        }


        public override void Draw(GameTime gameTime)
        {

            if (Blink)
            {

                Vector4 vec = color.ToVector4();
                vec.W = (float)Math.Abs(Math.Sin(Environment.TickCount / (500.0f / BlinkSpeed)));
                color = new Color(vec);

            }
            else
            {

                Vector4 vec = color.ToVector4();
                vec.W = 1.0f;
                color = new Color(vec);

            }

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            spriteBatch.Draw(Texture, Position, color);
            spriteBatch.End();

            base.Draw(gameTime);

        }

    }
}


/*

    //  MousePointer
    Cursor cursor = new Cursor(this);
    cursor.Blink = true;
    cursor.BlinkSpeed = 1.0f;
    cursor.color = Color.White;
    cursor.DrawOrder = 1000;
    cursor.Texture = Content.Load<Texture2D>("Arrow");
    cursor.RestrictZone = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
    this.Components.Add( cursor );

*/