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
    abstract class ObjetoJogo3D
    {
        GraphicsDevice placaVideo;

        Model modelo;

        BasicEffect efeitoBasico;

        Matrix matrizMundo;

        float rotacaoX;
        float rotacaoY;
        float rotacaoZ;
        float escalaX;
        float escalaY;
        float escalaZ;
        float posicaoX;
        float posicaoY;
        float posicaoZ;

        Matrix orientacao;

        public ObjetoJogo3D(GraphicsDevice placaVideo)
        {
            this.efeitoBasico = new BasicEffect(placaVideo);

            this.matrizMundo = Matrix.Identity;

            AtualizarMundo();
        }

        public void Atualizar(GameTime tempoJogo)
        {
            AtualizarMundo();
        }

        void AtualizarMundo()
        {
            this.matrizMundo =
                Matrix.CreateRotationX(MathHelper.ToRadians(rotacaoX)) *
                Matrix.CreateRotationY(MathHelper.ToRadians(rotacaoY)) *
                Matrix.CreateRotationZ(MathHelper.ToRadians(rotacaoZ)) *
                Matrix.CreateScale(escalaX, escalaY, escalaZ) *
                Matrix.CreateTranslation(posicaoX, posicaoY, posicaoZ);
        }

        public void Draw(GameTime tempoJogo, BasicCamera camera)
        {
            modelo.Draw(this.matrizMundo, camera.ViewMatrix, camera.ProjectionMatrix);
        }

    }
}
