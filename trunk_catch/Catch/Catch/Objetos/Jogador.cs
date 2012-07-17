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
using CapturarObjetos.Nucleo;

namespace CapturarObjetos.Objetos
{
    class Jogador : ObjetoJogo
    {
        public float DirecaoFrontal { get; set; }
        public int AlcanceMaximo { get; set; }

        public const float Velocidade = 0.75f;
        public const float VelocidadeDeCurva = 0.025f;

        public static List<Jogador> listaJogadores = new List<Jogador>();

        public Jogador(ContentManager Content, string _modelo)
            : base()
        {
            Modelo = Content.Load<Model>(_modelo);
            DirecaoFrontal = 0.0f;
            AlcanceMaximo = 100;

            Jogador.listaJogadores.Add(this);
        }
        
        public void Desenhar2(Camera camera)
        {
            Matrix[] transforms = new Matrix[Modelo.Bones.Count];
            Modelo.CopyAbsoluteBoneTransformsTo(transforms);

            Matrix worldMatrix = Matrix.Identity;
            Matrix rotationYMatrix = Matrix.CreateRotationY(DirecaoFrontal);
            Matrix translateMatrix = Matrix.CreateTranslation(Posicao);

            worldMatrix = rotationYMatrix * translateMatrix;

            foreach (ModelMesh mesh in Modelo.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = worldMatrix * transforms[mesh.ParentBone.Index]; ;
                    effect.View = camera.MatrizVisualizacao;
                    effect.Projection = camera.MatrizProjecao;

                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                }
                mesh.Draw();
            }
        }
    }
}
