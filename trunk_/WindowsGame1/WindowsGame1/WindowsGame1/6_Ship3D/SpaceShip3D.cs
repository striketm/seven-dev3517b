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

//http://www.mxstudio.com.br/tecnologia/jogos/videogames-quebra-de-paradigma-do-2d-para-o-3d/
//http://comunidade.kanshin.com.br/thread-3532.html

namespace WindowsGame1._6_Ship3D
{
    /// <summary>
    /// Esta classe representará uma nave (jogador ou inimigo) no jogo
    /// TO DO : herdar de uma classe objeto 3d mais básica?
    /// </summary>
    class SpaceShip3D
    {
        /// <summary>
        /// Este model representa o .fbx carregado, com as texturas
        /// É equivalente ao Texture2D, a "imagem"...
        /// </summary>
        Model model;

        /// <summary>
        /// A matrix de mundo do objeto, aqui pode ser colocadas as transformações
        /// de posição, escala e rotação
        /// </summary>
        public Matrix world;

        /// <summary>
        /// Construtor básico de uma nave
        /// TO DO : overloads
        /// </summary>
        /// <param name="model">O modelo 3d da nave</param>
        public SpaceShip3D(Model model)
        {
            //atribuições:
            this.model = model;
            world = Matrix.Identity;
        }

        /// <summary>
        /// vazio por enquanto
        /// </summary>
        public void Update()
        {

        }

        /// <summary>
        /// Método de apresentação de um modelo 3d em cena
        /// TO DO : overloads
        /// </summary>
        /// <param name="camera">Recebe a câmera sob o qual ele será visualizado</param>
        public void Draw(BasicCamera camera)
        {
            //draw básico:
            model.Draw(this.world, camera.view, camera.projection);
        }
    }
}
