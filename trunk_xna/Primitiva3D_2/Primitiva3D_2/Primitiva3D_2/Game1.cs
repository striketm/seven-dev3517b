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

namespace Primitivas3D
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Um conjunto (array) de vértices com posição e cor
        VertexPositionColor[] primitivas;

        //Um shader é um programa/efeito especial usado para apresentar imagens na tela
        BasicEffect efeitoBasico;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            efeitoBasico = new BasicEffect(GraphicsDevice);

            //definindo quantas posições temos no array
            primitivas = new VertexPositionColor[3];

            //definindo a posição 1 do array
            primitivas[0] = new VertexPositionColor();
            primitivas[0].Position = new Vector3(0, 1, 0);
            primitivas[0].Color = Color.Red;






            primitivas[1] = new VertexPositionColor();
            primitivas[1].Position = new Vector3(1, -1, 0);
            primitivas[1].Color = Color.Green;

            primitivas[2] = new VertexPositionColor();
            primitivas[2].Position = new Vector3(-1, -1, 0);
            primitivas[2].Color = Color.Blue;

        
            //as três matrizes básicas do mundo 3d

            //matriz de visão 
            efeitoBasico.View = Matrix.CreateLookAt( //CAMERA
                new Vector3(0, 0, 3),//posição
                Vector3.Zero,//olhando para
                Vector3.Up);//cima

            //matriz de projeção
            efeitoBasico.Projection = Matrix.CreatePerspectiveFieldOfView( //PROJEÇÃO/TELA
                MathHelper.PiOver4,//abertura
                graphics.GraphicsDevice.Viewport.AspectRatio,//proporção/razão
                0.1f,//perto
                100.0f);//longe

            //Habilita o efeito de cores nos vertices
            efeitoBasico.VertexColorEnabled = true;

        }

        protected override void UnloadContent()
        {
        
        }

        protected override void Update(GameTime gameTime)
        {
        
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            efeitoBasico.CurrentTechnique.Passes[0].Apply();

            GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(
                PrimitiveType.TriangleStrip,//tipo da primitiva
                primitivas,//vertex data - conjunto das primitivas
                0,//offset
                1);//contagem das primitivas

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

//exercícios: mover a camera frente tras e lados e zoom, sem distorcer o objeto, e mover o objeto translate, scale e rotate