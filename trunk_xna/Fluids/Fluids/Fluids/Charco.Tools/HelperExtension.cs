using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Charco.Tools
{

    public struct Size
    {
        public int Width, Height;

        public Size( int side ) : this( side, side ) { }

        public Size( int width, int height )
        {
            Width = width;
            Height = height;
        }

        public static explicit operator Point( Size size )
        {
            return new Point( size.Width, size.Height );
        }
    }

    public struct SizeF
    {
        public float Width, Height;

        public SizeF( float side ) : this( side, side ) { }

        public SizeF( float width, float height )
        {
            Width = width;
            Height = height;
        }

        public static explicit operator Vector2  (SizeF size)
        {
            return new Vector2(size.Width, size.Height);
        }

        public static SizeF operator * ( SizeF A, float B )
        {
            A.Width *= B;
            A.Height *= B;
            return A;
        }

        public static SizeF operator *(SizeF A, Vector2 B)
        {
            A.Width *= B.X;
            A.Height *= B.Y;
            return A;
        }

        public static Vector2 operator +(SizeF A, Vector2 B)
        {
            B.X += A.Width;
            B.Y += A.Height;
            return B;
        }
        public static Vector2 operator +(Vector2 B, SizeF A)
        {
            B.X += A.Width;
            B.Y += A.Height;
            return B;
        }
    }

    public struct Padding
    {
        public int Top, Bottom, Left, Right;
    }

    public static class HelperExtension
    {
        public static Vector2 ToVector2( this Point p )
        {
            return new Vector2( p.X, p.Y );
        }



        public static Point ToPoint( this Vector2 v )
        {
            return new Point( ( int ) v.X, ( int ) v.Y );
        }

        public static SizeF ToSizeF( this Vector2 v )
        {
            return new SizeF( ( int ) v.X, ( int ) v.Y );
        }

        public static Size ToSize( this Point p )
        {
            return new Size( p.X, p.Y );
        }

        public static uint RotateLeft( this uint value, int count )
        {
            return (value << count) | (value >> (32 - count));
        }

        public static uint RotateRight( this uint value, int count )
        {
            return (value >> count) | (value << (32 - count));
        }

    }
}
