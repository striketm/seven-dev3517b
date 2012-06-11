using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Globalization;

namespace Charco.Tools
{
    public class Logger : DrawableGameComponent
    {
        public bool ShowFps = true;

        float LifeTime = 3; // Segundos
        float DeadTime = 2;
        
        public static Logger Instancia;

        class Node
        {
            public string Texto;
            public float LifeTime;
            public Vector4 Color;
            public Node(string texto, float lifeTime, Color color)
            {
                Texto = texto;
                LifeTime = lifeTime;
                Color = color.ToVector4();
            }
        }

        LinkedList<Node> cola;
        Queue<Node> lista;

        public Vector2 QueuePos, ListPos;
        SpriteBatch batch;
        bool NotUpdated = true;

        public SpriteFont Font { get; private set; }
        
        Logger(Game game)
            : base(game)
        {
            UpdateOrder = int.MaxValue;
            DrawOrder = int.MaxValue;
            cola = new LinkedList<Node>();
            lista = new Queue<Node>();
#if XBOX
            position_cola = new Vector2(game.GraphicsDevice.Viewport.Width*0.1f + 10, game.GraphicsDevice.Viewport.Height * 0.86f);
            position_lista = new Vector2(game.GraphicsDevice.Viewport.Width*0.1f + 10, game.GraphicsDevice.Viewport.Height * 0.15f);
#else
            QueuePos = new Vector2(50, game.GraphicsDevice.Viewport.Height * 0.94f);
            ListPos = new Vector2(10, game.GraphicsDevice.Viewport.Height * 0.06f);
#endif
        }

       static string loggerFont="Fonts/Small";

        public static void Initialize(Game game, string Font)
        {
            if ( Instancia == null )
            {
                Instancia = new Logger( game );
                Instancia.DrawOrder = int.MaxValue;
                game.Components.Add( Instancia );
                loggerFont = Font; 
                Instancia.Initialize( );
            } 
        }
      
        protected override void LoadContent()
        {
            batch = new SpriteBatch(GraphicsDevice);
            Font = this.Game.Content.Load<SpriteFont>(loggerFont);
        }

        
        public static void Log(String texto, params object[] list)
        {
           
            if ( Instancia != null && Instancia.Enabled && Instancia.NotUpdated )
            {

                Instancia.cola.AddFirst( new Node( String.Format( CultureInfo.InvariantCulture, texto, list ), Instancia.LifeTime, Color.Yellow ) );
            }
        }

        public static void Log(float Life, String texto, params object[] list)
        {
            if ( Instancia != null && Instancia.Enabled && Instancia.NotUpdated )
            {
                Instancia.cola.AddFirst( new Node( String.Format( CultureInfo.InvariantCulture, texto, list ), Life, Color.Yellow ) );
            }
        }

        public static void Write(String texto, params object[] list)
        {
            Write(Color.Red, texto, list);
        }

        public static void Write(Color color, String texto, params object[] list)
        {
            if (Instancia != null && Instancia.Enabled && Instancia.NotUpdated)
            {
                Instancia.lista.Enqueue(new Node(String.Format(CultureInfo.InvariantCulture, texto, list), 0, color));
            }
        }

        double lastTime;
        int frameRate = 0;
        int frameCounter = 0;
        float elapsedTime = 0;

        public override void Update(GameTime time)
        {

            if ( NotUpdated )
            {
                NotUpdated = false;

              

                float value = ( float ) (time.TotalGameTime.TotalMilliseconds - lastTime) / 1000;
                lastTime = time.TotalGameTime.TotalMilliseconds;

                LinkedListNode<Node> node = Instancia.cola.First;
                while ( node != null )
                {
                    node.Value.LifeTime -= value;
                    if ( node.Value.LifeTime <= 0 )
                    {
                        cola.Remove( node );
                    }
                    else if ( node.Value.LifeTime < DeadTime )
                    {
                        node.Value.Color.W = node.Value.LifeTime / DeadTime;
                    }
                    node = node.Next;
                }

            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (!Enabled) return;

            elapsedTime += (float) gameTime.ElapsedGameTime.TotalSeconds;

            if ( elapsedTime > 1)
            {
                elapsedTime -= 1;
                frameRate = frameCounter;
                frameCounter = 0;
            }

            Vector2 newPos = Instancia.QueuePos;
            batch.Begin();

            frameCounter++;            

            LinkedListNode<Node> node = Instancia.cola.First;
            if (node != null)
            {
                while (node != null)
                {
                    batch.DrawString(Font, node.Value.Texto, newPos, new Color(node.Value.Color));
                    newPos.Y -= Font.LineSpacing;
                    node = node.Next;
                }
            }

            newPos = ListPos;
            while (lista.Count > 0)
            {
                Node nodo = lista.Dequeue();
                if (nodo != null)
                {
                    batch.DrawString(Font, nodo.Texto, newPos - new Vector2(1, 1), Color.Black);
                    batch.DrawString(Font, nodo.Texto, newPos, new Color(nodo.Color));
                    newPos.Y += Font.LineSpacing;
                }
            }
            batch.End();

            NotUpdated = true;

            if (ShowFps)
            {
                Write("FPS: " + frameRate);
            }

           
        }
    }       
}
