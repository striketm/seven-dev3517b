using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Charco.Tools
{

    public class Garbage : DrawableGameComponent
    {
        public float TimeLeftNextUpdate = 2;
        public float TimeNextUpdate = 0.1f;
        public int SnapCount = 500;


        float ElapsedTimeBetweenCollect = 0;
        float time = 0;

        long[] Snaps;

        public override void Initialize()
        {
            Build();
            base.Initialize();
        }

        public float Height = 100;
        int start, end;
        long max, min;

        private void Build()
        {
            if (Snaps == null) Snaps = new long[SnapCount];
            else if (SnapCount>Snaps.Length)
            {
                Array.Resize<long>( ref Snaps, SnapCount );
            }
            start = end = 0;
            min = max = GC.GetTotalMemory( false ) >> 10;
            for ( int i=0; i < SnapCount; i++ ) Snaps[i] =min;               
        }


        public Garbage(Game game) : base(game) { DrawOrder = int.MaxValue; }

        public override void Update(GameTime gameTime)
        {
            float Seconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            time += Seconds;
            TimeLeftNextUpdate -= Seconds;

            while (TimeLeftNextUpdate < 0)
            {
                int old_end = end;
                end = (end + 1) % SnapCount;
                TimeLeftNextUpdate += TimeNextUpdate;
                Snaps[end] = GC.GetTotalMemory(false) >> 10;
                max = Math.Max( Snaps[end], max );
                min = Math.Min( Snaps[end], min );

                if ( Snaps[end] < Snaps[old_end] )
                {
                    ElapsedTimeBetweenCollect = time;
                    time = 0;
                }
            }
            base.Update(gameTime);
        }

        public Vector2 Position = Vector2.One * 50;

        public override void Draw(GameTime gameTime)
        {
            float dx;
            int i, j;
            Vector2 v1 , v2;

            v1 = v2 = Vector2.Zero;

            Matrix World = Matrix.CreateScale(150,-50,1) * Matrix.CreateTranslation(Position.X, Position.Y, 0);

            float d = max - min;

            if ( d != 0 )
            {
                j = (end + 2) % SnapCount;
                dx = 1.0f / SnapCount;
                v2.X = 0;
                v2.Y = (d > 0) ? (Snaps[j] - min) / d : 0;
                do
                {
                    i = j;
                    j = (j + 1) % SnapCount;
                    v1 = v2;
                    v2.X += dx;
                    v2.Y = (d > 0) ? (Snaps[j] - min) / d : 0;
                    Lineas.Instancia.AddLine2D( World, v1, v2, Color.Red );
                }
                while ( j != (end + SnapCount - 1) % SnapCount );
            }

            j = (end + SnapCount - 1) % SnapCount;

            Logger.Write( "Mem ({0}, {1:#.##}, {2})", Snaps[end], ElapsedTimeBetweenCollect, Snaps[end] - Snaps[j] );

            
            base.Draw(gameTime);
        }
    }
}
