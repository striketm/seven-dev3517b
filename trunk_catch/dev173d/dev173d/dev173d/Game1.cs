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

namespace dev173d
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        BasicPrimitive chao;
        BasicPrimitive teto;
        BasicPrimitive parede_frente;
        BasicPrimitive parede_direita;
        BasicPrimitive parede_esquerda;
        BasicPrimitive parede_tras;

        Model caixa_01;
                
        BasicCamera camera;

        KeyboardState ks, old_ks;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            chao = new BasicPrimitive(GraphicsDevice, Content.Load<Texture2D>("texturachao"), Color.White);
            teto = new BasicPrimitive(GraphicsDevice, Content.Load<Texture2D>("texturateto"), Color.White);
            parede_frente = new BasicPrimitive(GraphicsDevice, Content.Load<Texture2D>("texturaparede"), Color.White);
            parede_direita = new BasicPrimitive(GraphicsDevice, Content.Load<Texture2D>("texturaparede"), Color.White);
            parede_esquerda = new BasicPrimitive(GraphicsDevice, Content.Load<Texture2D>("texturaparede"), Color.White);
            parede_tras = new BasicPrimitive(GraphicsDevice, Content.Load<Texture2D>("texturaparede"), Color.White);

            chao.World = Matrix.CreateRotationX(MathHelper.ToRadians(-90.0f))
                * Matrix.CreateScale(10) *
                Matrix.CreateTranslation(0, -1.5f, 0);

            teto.World = Matrix.CreateRotationX(MathHelper.ToRadians(90.0f))
                * Matrix.CreateScale(10) *
                Matrix.CreateTranslation(0, 1.5f, 0);

            parede_frente.World = /*Matrix.CreateRotationX(MathHelper.ToRadians(90.0f)) * */
                Matrix.CreateScale(10, 3.5f, 1) * //o meio é pra esconder um defeito na imagem
                Matrix.CreateTranslation(0, 0, -5f);

            parede_direita.World = Matrix.CreateRotationY(MathHelper.ToRadians(270.0f))//=-90
                * Matrix.CreateScale(1, 3.5f, 10) * //reparem como os eixos de escala tiveram que mudar
                Matrix.CreateTranslation(5f, 0, 0);
            
            parede_esquerda.World = Matrix.CreateRotationY(MathHelper.ToRadians(-270.0f))//=90
                * Matrix.CreateScale(1, 3.5f, 10) * //reparem como os eixos de escala tiveram que mudar
                Matrix.CreateTranslation(-5f, 0, 0);

            parede_tras.World = Matrix.CreateRotationY(MathHelper.ToRadians(180.0f))//rotação total
               * Matrix.CreateScale(10, 3.5f, 1) * //não seria melhor deixar a rotação para todos, mesmo que seja 0, para padronizar?
               Matrix.CreateTranslation(0, 0, 5f);

            caixa_01 = Content.Load<Model>("caixas/caixa_vazada");            
        
            camera = new BasicCamera(GraphicsDevice);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            teto.color = Color.Green;

            old_ks = ks;
            ks = Keyboard.GetState();

            #region atualização do target da camera

            //frente
            if (ks.IsKeyDown(Keys.Home))
                camera.Target = new Vector3(0,0,-1.0f);

            //trás
            if (ks.IsKeyDown(Keys.End))
                camera.Target = new Vector3(0, 0, 1.0f);

            //esquerda
            if (ks.IsKeyDown(Keys.Delete))
                camera.Target = new Vector3(-1.0f, 0, 0);

            //direita
            if (ks.IsKeyDown(Keys.PageDown))
                camera.Target = new Vector3(1.0f, 0, 0);

            //cima
            if (ks.IsKeyDown(Keys.Insert))
                camera.Target = new Vector3(0, 1.0f, 0);

            //baixo
            if (ks.IsKeyDown(Keys.PageUp))
                camera.Target = new Vector3(0, -1.0f, 0);

            #endregion

            camera.Update(gameTime);

            chao.Update(gameTime);
            teto.Update(gameTime);
            parede_frente.Update(gameTime);
            parede_direita.Update(gameTime);
            parede_esquerda.Update(gameTime);
            parede_tras.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            RasterizerState myRasterizerStateNormal = new RasterizerState();
            RasterizerState myRasterizerStateWired = new RasterizerState();
            RasterizerState myRasterizerStateUnCulled = new RasterizerState();

            myRasterizerStateNormal.FillMode = FillMode.Solid;//padrão
            myRasterizerStateNormal.CullMode = CullMode.CullCounterClockwiseFace;//padrão
            
            myRasterizerStateWired.FillMode = FillMode.WireFrame;
            
            myRasterizerStateUnCulled.CullMode = CullMode.None;

            //GraphicsDevice.RasterizerState = myRasterizerStateNormal;

            //? slow?
            chao.Draw(gameTime, camera);
            teto.Draw(gameTime, camera);
            parede_frente.Draw(gameTime, camera);
            parede_direita.Draw(gameTime, camera);
            parede_esquerda.Draw(gameTime, camera);
            parede_tras.Draw(gameTime, camera);
            Matrix temp = Matrix.Identity;
            temp *= Matrix.CreateScale(1f) * Matrix.CreateTranslation(Vector3.Zero);

            caixa_01.Draw(temp, camera.ViewMatrix, camera.ProjectionMatrix);
            
            //temp *= Matrix.CreateScale(0.008f) * Matrix.CreateTranslation(new Vector3(0.9f, 0, 0));
            //caixa_02.Draw(temp, camera.ViewMatrix, camera.ProjectionMatrix);
            
            base.Draw(gameTime);
        }
    }
}


