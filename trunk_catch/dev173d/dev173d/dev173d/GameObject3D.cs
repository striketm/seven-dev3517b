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
    //abstract class ObjetoJogo3D
    public class ObjetoJogo3D
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

        public ObjetoJogo3D(GraphicsDevice placaVideo, Model model)
        {
            this.efeitoBasico = new BasicEffect(placaVideo);

            this.placaVideo = placaVideo;

            escalaX = escalaY = escalaZ = 0.01f;
            rotacaoX = rotacaoY = rotacaoZ = 0;
            posicaoX = posicaoY = posicaoZ = 0;

            this.matrizMundo = Matrix.Identity;

            this.modelo = model;

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

        public void Draw(GameTime tempoJogo, BasicCamera camera, Light light)
        {
            //foreach (ModelMesh mm in modelo.Meshes)
            //{
            //    foreach (ModelMeshPart mmp in mm.MeshParts)
            //    {
            //        efeitoBasico.World = matrizMundo;
            //        placaVideo.SetVertexBuffer(mmp.VertexBuffer, mmp.VertexOffset);
            //        placaVideo.Indices = mmp.IndexBuffer;
            //        efeitoBasico.CurrentTechnique.Passes[0].Apply();
            //        placaVideo.DrawIndexedPrimitives(
            //            PrimitiveType.TriangleList, 0, 0,
            //            mmp.NumVertices, mmp.StartIndex, mmp.PrimitiveCount);
            //    }
            //}

            foreach (ModelMesh mesh in this.modelo.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = this.matrizMundo;// *transforms[mesh.ParentBone.Index];
                    effect.View = camera.ViewMatrix;
                    effect.Projection = camera.ProjectionMatrix;

                    effect.LightingEnabled = light.efeitoLuz.LightingEnabled;
                    effect.DirectionalLight0.Enabled = light.efeitoLuz.DirectionalLight0.Enabled;
                    effect.DirectionalLight0.DiffuseColor = light.efeitoLuz.DirectionalLight0.DiffuseColor;
                    effect.DirectionalLight0.Direction = light.efeitoLuz.DirectionalLight0.Direction;
                    
                    //effect.EnableDefaultLighting();
                    //effect.PreferPerPixelLighting = true;
                }
                mesh.Draw();
            }

        }

    }
}

