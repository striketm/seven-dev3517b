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

namespace CapturarObjetos.Nucleo
{
    class ObjetoJogo
    {
        #region Atributos
        
        Model modelo;
        /// <summary>
        /// Representa a malha do objeto
        /// </summary>
        public Model Modelo { get { return modelo; } set { modelo = value; } }
        Vector3 posicao;
        /// <summary>
        /// Representa a posição do objeto no espaço
        /// </summary>
        public Vector3 Posicao { get { return posicao; } set { posicao = value; PosicaoX = posicao.X; PosicaoY = posicao.Y; PosicaoZ = posicao.Z; } }
        bool ativo;
        /// <summary>
        /// Representa se o objeto está ativo ou não
        /// </summary>
        public bool Ativo { get { return ativo; } set { ativo = value; } }
        BoundingSphere esferaColisao;
        /// <summary>
        /// Representa a esfera de colisão do objeto
        /// </summary>
        public BoundingSphere EsferaColisao { get { return esferaColisao; } set { esferaColisao = value; } }

        BoundingBox caixaColisao;
        /// <summary>
        /// Representa a caixa de colisão do objeto
        /// </summary>
        public BoundingBox CaixaColisao { get { return caixaColisao; } set { caixaColisao = value; } }

        Matrix world;
        /// <summary>
        /// Representa a matrix de posicionamento do objeto no mundo
        /// </summary>
        public Matrix World { get { return world; } set { world = value; } }

        float rotacaoX;
        /// <summary>
        /// Representa a rotação em X do objeto no mundo
        /// </summary>
        public float RotacaoX { get { return rotacaoX; } set { rotacaoX = value; } }

        float rotacaoY;
        /// <summary>
        /// Representa a rotação em Y do objeto no mundo
        /// </summary>
        public float RotacaoY { get { return rotacaoY; } set { rotacaoY = value; } }

        float rotacaoZ;
        /// <summary>
        /// Representa a rotação em Z do objeto no mundo
        /// </summary>
        public float RotacaoZ { get { return rotacaoZ; } set { rotacaoZ = value; } }

        float posicaoX;
        /// <summary>
        /// Representa a posição em X do objeto no mundo
        /// </summary>
        public float PosicaoX { get { return posicaoX; } set { posicaoX = value; } }

        float posicaoY;
        /// <summary>
        /// Representa a posição em Y do objeto no mundo
        /// </summary>
        public float PosicaoY { get { return posicaoY; } set { posicaoY = value; } }

        float posicaoZ;
        /// <summary>
        /// Representa a posição em Z do objeto no mundo
        /// </summary>
        public float PosicaoZ { get { return posicaoZ; } set { posicaoZ = value; } }

        float escalaX;
        /// <summary>
        /// Representa a escala em X do objeto no mundo
        /// </summary>
        public float EscalaX { get { return escalaX; } set { escalaX = value; } }

        float escalaY;
        /// <summary>
        /// Representa a escala em Y do objeto no mundo
        /// </summary>
        public float EscalaY { get { return escalaY; } set { escalaY = value; } }

        float escalaZ;
        /// <summary>
        /// Representa a escala em Z do objeto no mundo
        /// </summary>
        public float EscalaZ { get { return escalaZ; } set { escalaZ = value; } }

        float escala;
        /// <summary>
        /// Representa a escala do objeto no mundo
        /// </summary>//fazer o reverso e criar um vetor escala e fazer isso pra todos
        public float Escala { get { return escala; } set { escala = EscalaX = EscalaY = EscalaZ  = value; } }

        public static List<ObjetoJogo> listaObjetos = new List<ObjetoJogo>();

        #endregion

        #region Métodos
        /// <summary>
        /// Construtor padrão com todos os atributos zerados
        /// </summary>
        public ObjetoJogo()
        {
            RotacaoX = RotacaoY = RotacaoZ = 0;
            EscalaX = EscalaY = EscalaZ = 1;
            PosicaoX = PosicaoY = PosicaoZ = 0;
            Modelo = null;
            Posicao = Vector3.Zero;
            Ativo = true;
            EsferaColisao = new BoundingSphere();
            CaixaColisao = new BoundingBox();
            World = Matrix.Identity;
            ObjetoJogo.listaObjetos.Add(this);
            AtualizarMundoEColisoes();
        }

        //todo chamar o update em todos os filhos pra atualizar a colisao das esferas e caixas e mundo

        public void AtualizarMundoEColisoes()
        {
            this.World = Matrix.CreateRotationX(RotacaoX) *
                                                    Matrix.CreateRotationY(RotacaoY) *
                                                    Matrix.CreateRotationZ(RotacaoZ) *
                                                    Matrix.CreateScale(EscalaX, EscalaY, EscalaZ) *
                                                    Matrix.CreateTranslation(PosicaoX, PosicaoY, PosicaoZ);

            //TODO colocar no get set!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            Posicao = new Vector3(PosicaoX, PosicaoY, PosicaoZ);

            this.CaixaColisao = AtualizarCaixaColisao();
            this.EsferaColisao = AtualizarEsferaColisao();
        }

        public void Desenhar(Camera camera)
        {
            if (Ativo)
            {
                Matrix[] transforms = new Matrix[Modelo.Bones.Count];
                Modelo.CopyAbsoluteBoneTransformsTo(transforms);
                
                foreach (ModelMesh mesh in this.Modelo.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.World = this.World * transforms[mesh.ParentBone.Index];
                         effect.View = camera.MatrizVisualizacao;
                        effect.Projection = camera.MatrizProjecao;

                        effect.EnableDefaultLighting();
                        effect.PreferPerPixelLighting = true;
                    }
                    mesh.Draw();
                }
            }
        }

        public void ModelDraw(GraphicsDevice device, Vector3 cameraPosition, Vector3 cameraTarget, float farPlaneDistance)
        {
            Matrix[] transforms = new Matrix[Modelo.Bones.Count];
            Modelo.CopyAbsoluteBoneTransformsTo(transforms);

            // Compute camera matrices.
            Matrix view = Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.Right);

            //Calculate the aspect ratio
            float aspectRatio = (float)device.Viewport.Width /
                                        (float)device.Viewport.Height;

            Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), aspectRatio,
                1.0f, farPlaneDistance);

            // Draw the model. A model can have multiple meshes, so loop.
            foreach (ModelMesh mesh in Modelo.Meshes)
            {
                // This is where the mesh orientation is set, as well as our camera and projection.
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = transforms[mesh.ParentBone.Index] *
                        Matrix.CreateRotationX(RotacaoX) *
                        Matrix.CreateRotationY(RotacaoY) *
                        Matrix.CreateRotationZ(RotacaoZ) *
                        Matrix.CreateScale(Escala) *
                        Matrix.CreateWorld(Posicao, Vector3.Forward, Vector3.Up);

                    effect.View = view;
                    effect.Projection = projection;
                }
                mesh.Draw();
            }
        }

        /// <summary>
        /// Atualiza a caixa de colisão do objeto
        /// </summary>
        /// <param name="model">Recebe o modelo do objeto</param>
        /// <param name="worldTransform">Recebe a matriz de transformação de mundo do objeto</param>
        /// <returns>Uma caixa de colisão com o tamanho certo do objeto</returns>
        //protected BoundingBox UpdateBoundingBox(Model model, Matrix worldTransform)
        public BoundingBox AtualizarCaixaColisao()
        {
            //Inicia os cantos da caixa com valores maximo e minimo
            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            if (Modelo == null) return new BoundingBox();

            //para cada malha e submalha no modelo
            foreach (ModelMesh mesh in Modelo.Meshes)
            {
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {
                    int vertexStride = meshPart.VertexBuffer.VertexDeclaration.VertexStride;//o número de bytes de um vértice para o próximo
                    int vertexBufferSize = meshPart.NumVertices * vertexStride;//o tamanho do buffer (array) de vértices
                    float[] vertexData = new float[vertexBufferSize / sizeof(float)];//um array [] com o tamanho certo para caber o vertexBufferSize
                    meshPart.VertexBuffer.GetData<float>(vertexData);//pegar GetData a informação de float e preencher o buffer de vértices
                    //percorrer de i = 0 até o tamanho do buffer de vértices em float andando de float em float (ver cada ponto)
                    for (int i = 0; i < vertexBufferSize / sizeof(float); i += vertexStride / sizeof(float))
                    {
                        //cria um vertor transformado com os tres pontos em relação ao mundo
                        Vector3 transformedPosition = Vector3.Transform(new Vector3(vertexData[i], vertexData[i + 1], vertexData[i + 2]), this.World);

                        //calcula os pontos, o mínimo e o máximo
                        min = Vector3.Min(min, transformedPosition);
                        max = Vector3.Max(max, transformedPosition);
                    }
                }
            }

            //cria e retorna o BoundingBox segundo os valores calculados para o tamanho
            return new BoundingBox(min, max);

         }

        protected BoundingSphere AtualizarEsferaColisao()
        {
            if (Modelo == null) return new BoundingSphere();

            return new BoundingSphere(Posicao, 1f);

            /*
            BoundingSphere mergedSphere = new BoundingSphere();
            BoundingSphere[] boundingSpheres;
            int index = 0;
            int meshCount = Modelo.Meshes.Count;

            boundingSpheres = new BoundingSphere[meshCount];
            foreach (ModelMesh mesh in Modelo.Meshes)
            {
                boundingSpheres[index++] = mesh.BoundingSphere;
            }

            mergedSphere = boundingSpheres[0];
            if ((Modelo.Meshes.Count) > 1)
            {
                index = 1;
                do
                {
                    mergedSphere = BoundingSphere.CreateMerged(mergedSphere,
                        boundingSpheres[index]);
                    index++;
                } while (index < Modelo.Meshes.Count);
            }
            mergedSphere.Center.Y = 0;
            return mergedSphere;
             * */

        }

        #endregion

    }
}