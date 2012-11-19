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

namespace WindowsGame1._6_Ship3D
{
    /// <summary>
    /// Classe que representa uma câmera básica, mínima, padrão, sem movimentos, no jogo
    /// </summary>
    class BasicCamera
    {
        /// <summary>
        /// A matrix de visualização (frustum)
        /// </summary>
        public Matrix view;

        /// <summary>
        /// A matrix de projeção (da câmera na tela)
        /// </summary>
        public Matrix projection;

        /// <summary>
        /// Onde a câmera está
        /// </summary>
        Vector3 position;

        /// <summary>
        /// Para onde a câmera está olhando
        /// </summary>
        Vector3 look_at;

        /// <summary>
        /// Para onde fica o "cima" da câmera
        /// </summary>
        Vector3 orientation;

        /// <summary>
        /// O ângulo de abertura da câmera, em graus
        /// </summary>
        float aperture;

        /// <summary>
        /// A razão de projeção da tela
        /// </summary>
        float ratio;

        /// <summary>
        /// O plano de distância mínima que a câmera renderiza
        /// </summary>
        float near;

        /// <summary>
        /// O plano de distância máxima que a câmera renderiza
        /// </summary>
        float far;
        
        /// <summary>
        /// Cosntrutor de uma câmera básica
        /// </summary>
        /// <param name="position">Onde está a câmera</param>
        /// <param name="look_at">Para onde ela está apontando</param>
        /// <param name="orientation">Para onde é o "cima"</param>
        public BasicCamera(Vector3 position, Vector3 look_at, Vector3 orientation)
        {
            //atribuições
            this.position = position;
            this.look_at = look_at;
            this.orientation = orientation;

            this.aperture = 90.0f;
            this.ratio = 16.0f / 9.0f;
            this.near = 1.0f;
            this.far = 100.0f;

            //não esquecer de chamar logo o update senão pode tentar o view sem new...
            Update();
        }

        /// <summary>
        /// A câmera apenas atualiza, é utilizada em draws, mas não tem um...
        /// </summary>
        public void Update()
        {
            //TO DO : mudar apenas se houver mudança...

            view = Matrix.CreateLookAt(position, look_at, orientation);

            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(aperture),
                ratio,
                near,
                far);
        }
    }
}
