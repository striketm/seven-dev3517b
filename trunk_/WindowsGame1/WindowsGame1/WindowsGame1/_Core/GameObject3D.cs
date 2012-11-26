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

namespace WindowsGame1._Core
{
    /// <summary>
    /// 
    /// </summary>
    abstract class GameObject3D
    {
        #region Atributos

        Model model;
        /// <summary>
        /// Representa a malha do objeto
        /// </summary>
        public Model MyModel { get { return model; } set { model = value; } }

        Vector3 position;
        /// <summary>
        /// Representa a posição do objeto no espaço, cuidado para não confundir o Vector3 com cada um dos floats
        /// </summary>
        public Vector3 Position { get { return position; } set { position = value; PositionX = position.X; PositionY = position.Y; PositionZ = position.Z; } }

        bool active;
        /// <summary>
        /// Representa se o objeto está ativo ou não
        /// </summary>
        public bool Active { get { return active; } set { active = value; } }

        BoundingSphere collisionSphere;
        /// <summary>
        /// Representa a esfera de colisão do objeto
        /// </summary>
        public BoundingSphere CollisionSphere { get { return collisionSphere; } set { collisionSphere = value; } }

        BoundingBox collisionBox;
        /// <summary>
        /// Representa a caixa de colisão do objeto
        /// </summary>
        public BoundingBox CollisionBox { get { return collisionBox; } set { collisionBox = value; } }

        Matrix world;
        /// <summary>
        /// Representa a matrix de posicionamento do objeto no mundo
        /// </summary>
        public Matrix World { get { return world; } set { world = value; } }

        float rotationX;
        /// <summary>
        /// Representa a rotação em X do objeto no mundo, em GRAUS!!!
        /// </summary>
        public float RotationX { get { return rotationX; } set { rotationX = value; } }
        //TO DO REFACTOR OPTIMIZE evitar conversão desnecessária a cada draw...

        float rotationY;
        /// <summary>
        /// Representa a rotação em Y do objeto no mundo, em GRAUS!!!
        /// </summary>
        public float RotationY { get { return rotationY; } set { rotationY = value; } }

        float rotationZ;
        /// <summary>
        /// Representa a rotação em Z do objeto no mundo, em GRAUS!!!
        /// </summary>
        public float RotationZ { get { return rotationZ; } set { rotationZ = value; } }

        float positionX;
        /// <summary>
        /// Representa a posição em X do objeto no mundo
        /// </summary>
        public float PositionX { get { return positionX; } set { positionX = value; }}//Position = new Vector3(value, Position.Y, Position.Z); } }//to do loop?

        float positionY;
        /// <summary>
        /// Representa a posição em Y do objeto no mundo
        /// </summary>
        public float PositionY { get { return positionY; } set { positionY = value; }}// Position = new Vector3(Position.X, value, Position.Z); } }

        float positionZ;
        /// <summary>
        /// Representa a posição em Z do objeto no mundo
        /// </summary>
        public float PositionZ { get { return positionZ; } set { positionZ = value; }}//Position = new Vector3(Position.X, Position.Y, value); } }

        float scaleX;
        /// <summary>
        /// Representa a escala em X do objeto no mundo
        /// </summary>
        public float ScaleX { get { return scaleX; } set { scaleX = value; } }

        float scaleY;
        /// <summary>
        /// Representa a escala em Y do objeto no mundo
        /// </summary>
        public float ScaleY { get { return scaleY; } set { scaleY = value; } }

        float scaleZ;
        /// <summary>
        /// Representa a escala em Z do objeto no mundo
        /// </summary>
        public float ScaleZ { get { return scaleZ; } set { scaleZ = value; } }

        float scale;
        /// <summary>
        /// Representa a escala do objeto no mundo
        /// </summary>//fazer o reverso e criar um vetor escala e fazer isso pra todos
        public float Scale { get { return scale; } set { scale = ScaleX = ScaleY = ScaleZ = value; } }

        /// <summary>
        /// 
        /// </summary>
        public static List<GameObject3D> list = new List<GameObject3D>();

        #endregion

        #region Métodos

        /// <summary>
        /// Construtor padrão com todos os atributos zerados
        /// </summary>
        public GameObject3D(Model model)
        {
            MyModel = model;

            RotationX = RotationY = RotationZ = 0;
            ScaleX = ScaleY = ScaleZ = 1;
            PositionX = PositionY = PositionZ = 0;
                        
            //Position = Vector3.Zero;

            Active = true;

            CollisionSphere = new BoundingSphere();
            CollisionBox = new BoundingBox();

            World = Matrix.Identity;

            GameObject3D.list.Add(this);

            UpdateWorldAndCollisions();
        }

        //todo chamar o update em todos os filhos pra atualizar a colisao das esferas e caixas e mundo

        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// 
        /// </summary>
        public void UpdateWorldAndCollisions()
        {
            this.World = Matrix.CreateRotationX(MathHelper.ToRadians(RotationX)) *
                                                    Matrix.CreateRotationY(MathHelper.ToRadians(RotationY)) *
                                                    Matrix.CreateRotationZ(MathHelper.ToRadians(RotationZ)) *
                                                    Matrix.CreateScale(ScaleX, ScaleY, ScaleZ) *
                                                    Matrix.CreateTranslation(PositionX, PositionY, PositionZ);

            //TODO colocar no get set!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //Position = new Vector3(PositionX, PositionY, PositionZ);//ok!?

            this.CollisionBox = UpdateCollisionBox();
            this.CollisionSphere = UpdateCollisionSphere();
        }

        /// <summary>
        /// Draw padrão
        /// </summary>
        /// <param name="camera"></param>
        public void Draw(BasicCamera camera)
        {
            if (Active)
            {
                //Matrix[] transforms = new Matrix[MyModel.Bones.Count];
                //MyModel.CopyAbsoluteBoneTransformsTo(transforms);

                foreach (ModelMesh mesh in this.MyModel.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        //effect.World = this.World * transforms[mesh.ParentBone.Index];
                        //effect.View = camera.ViewMatrix;
                        //effect.Projection = camera.ProjectionMatrix;
                        effect.EnableDefaultLighting();
                        effect.PreferPerPixelLighting = true;
                    }
                    //mesh.Draw();
                }
                MyModel.Draw(this.World, camera.ViewMatrix, camera.ProjectionMatrix);
            }

        }

        /// <summary>
        /// Um draw sendo preparado diferente
        /// </summary>
        /// <param name="camera"></param>
        public void ModelDraw(BasicCamera camera)
        {
            Matrix[] transforms = new Matrix[MyModel.Bones.Count];
            MyModel.CopyAbsoluteBoneTransformsTo(transforms);

            // Compute camera matrices.
            Matrix view = camera.ViewMatrix;

            Matrix projection = camera.ProjectionMatrix;

            // Draw the model. A model can have multiple meshes, so loop.
            foreach (ModelMesh mesh in MyModel.Meshes)
            {
                // This is where the mesh orientation is set, as well as our camera and projection.
                foreach (BasicEffect effect in mesh.Effects)
                {
                    //effect.World = transforms[mesh.ParentBone.Index] *
                        World = Matrix.CreateRotationX(RotationX) *
                        Matrix.CreateRotationY(RotationY) *
                        Matrix.CreateRotationZ(RotationZ) *
                        Matrix.CreateScale(Scale) *
                        Matrix.CreateWorld(Position, Vector3.Forward, Vector3.Up);

                    effect.View = view;
                    effect.Projection = projection;
                }
                mesh.Draw();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public BoundingBox UpdateCollisionBox()
        {
            if (MyModel == null) return new BoundingBox();

            return new BoundingBox(Position, new Vector3(Position.X+1, Position.Y+1, Position.Z+1));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public BoundingSphere UpdateCollisionSphere()
        {
            if (MyModel == null) return new BoundingSphere();

            return new BoundingSphere(Position, 1f);

        }

        #endregion

    }
}
