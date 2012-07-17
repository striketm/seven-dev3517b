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
        public Vector3 Posicao { get { return posicao; } set { posicao = value; } }
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
        /// </summary>
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
                        effect.EnableDefaultLighting();
                        effect.PreferPerPixelLighting = true;
                        effect.World = Matrix.CreateRotationX(RotacaoX) *
                                                    Matrix.CreateRotationY(RotacaoY) *
                                                    Matrix.CreateRotationZ(RotacaoZ) *
                                                    Matrix.CreateScale(EscalaX, EscalaY, EscalaZ) * 
                                                    Matrix.CreateTranslation(PosicaoX, PosicaoY, PosicaoZ) *
                                                    transforms[mesh.ParentBone.Index];
 
                        effect.View = camera.MatrizVisualizacao;
                        effect.Projection = camera.MatrizProjecao;

                        effect.EnableDefaultLighting();
                        effect.PreferPerPixelLighting = true;
                    }
                    mesh.Draw();
                }
            }
        }
               
        /// <summary>
        /// Atualiza a caixa de colisão do objeto
        /// </summary>
        /// <param name="model">Recebe o modelo do objeto</param>
        /// <param name="worldTransform">Recebe a matriz de transformação de mundo do objeto</param>
        /// <returns>Uma caixa de colisão com o tamanho certo do objeto</returns>
        protected BoundingBox UpdateBoundingBox(Model model, Matrix worldTransform)
        {
            //Inicia os cantos da caixa com valores maximo e minimo
            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            //para cada malha e submalha no modelo
            foreach (ModelMesh mesh in model.Meshes)
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
                        Vector3 transformedPosition = Vector3.Transform(new Vector3(vertexData[i], vertexData[i + 1], vertexData[i + 2]), worldTransform);

                        //calcula os pontos, o mínimo e o máximo
                        min = Vector3.Min(min, transformedPosition);
                        max = Vector3.Max(max, transformedPosition);
                    }
                }
            }

            //cria e retorna o BoundingBox segundo os valores calculados para o tamanho
            return new BoundingBox(min, max);

         }

        #endregion

    }
}