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
        Vector3 up;

        /// <summary>
        /// Cosntrutor de uma câmera básica
        /// </summary>
        /// <param name="position">Onde está a câmera</param>
        /// <param name="look_at">Para onde ela está apontando</param>
        /// <param name="up">Para onde é o "cima"</param>
        public BasicCamera(Vector3 position, Vector3 look_at, Vector3 up)
        {
            //atribuições
            this.position = position;
            this.look_at = look_at;
            this.up = up;

            //não esquecer de chamar logo o update senão pode tentar o view sem new...
            Update();
        }

        /// <summary>
        /// A câmera apenas atualiza, é utilizada em draws, mas não tem um...
        /// </summary>
        public void Update()
        {
            //TO DO : mudar apenas se houver mudança...

            view = Matrix.CreateLookAt(position, look_at, up);

            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(90.0f),
                16.0f / 9.0f,
                1.0f,
                100.0f);
        }
    }
}
