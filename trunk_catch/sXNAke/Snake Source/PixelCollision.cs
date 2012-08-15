
#region File Description
//----------------------------------------------------------------------------------//
//                                                                                  //
//  Name : PixelCollision                                                           //
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


namespace Xna.Auxiliary.Collision
{

    class PixelCollision
    {

        static public bool Vector2(Vector2 vec, Texture2D texture, Rectangle frame, byte alpha)
        {

            if ((vec.X >= frame.X && vec.X < frame.Right) && (vec.Y >= frame.Y && vec.Y < frame.Bottom))
            {

                Color[] data = new Color[1];

                texture.GetData<Color>(0, new Rectangle((int)vec.X - frame.X, (int)vec.Y - frame.Y, 1, 1), data, 0, 1);

                if (data[0].A >= alpha) return true;

            }

            return false;

        }

    }

}
