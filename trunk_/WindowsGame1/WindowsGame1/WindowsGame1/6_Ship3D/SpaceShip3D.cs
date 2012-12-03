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
using WindowsGame1._Core;

namespace WindowsGame1._6_Ship3D
{
    class SpaceShip3D:GameObject3D
    {
        public float DirecaoFrontal { get; set; }
        
        public const float Velocidade = 0.75f;
        public const float VelocidadeDeCurva = 0.025f;

        public SpaceShip3D(Model model):base(model)
        {
            DirecaoFrontal = 0.0f;
        }

        public override void Update(GameTime gameTime)
        {
            //preciso testar *primeiro* se a posição futura é válida para não travar, por isso a guardo...
            Vector3 posicaoFutura = Position;

            //estas duas variáveis temporárias servem para o giro e o movimento
            float quantoVirar = 0;
            Vector3 movimento = Vector3.Zero;

            if (Game1.Instance.keyboardIsPressing(Keys.A) ||
                Game1.Instance.keyboardIsPressing(Keys.Left))
            {
                quantoVirar = 1;
            }
            else if (Game1.Instance.keyboardIsPressing(Keys.D) ||
                        Game1.Instance.keyboardIsPressing(Keys.Right))
            {
                quantoVirar = -1;
            }
            if (Game1.Instance.keyboardIsPressing(Keys.Up)
                || Game1.Instance.keyboardIsPressing(Keys.W))
            {
                movimento.Z = -1;
            }
            else if (Game1.Instance.keyboardIsPressing(Keys.Down) ||
                        Game1.Instance.keyboardIsPressing(Keys.S))
            {
                movimento.Z = 1;
            }

            //o pra onde ele vai virar é corrigido pela velocidade de giro
            DirecaoFrontal += quantoVirar * VelocidadeDeCurva;
            //isto está aqui apenas pq esta variável tem outro nome na hora de desenhar...
            RotationY = DirecaoFrontal;
            //preciso girar meu objeto naquela direção antes de andar
            Matrix matrixOrientacao = Matrix.CreateRotationY(DirecaoFrontal);
            //ando com ele aquela quantidade de movimento naquela direcao de orientacao
            Vector3 velocidade = Vector3.Transform(movimento, matrixOrientacao);
            //corrijo pela escala da velocidade
            velocidade *= Velocidade;
            //agora sim esta será a nova posicao do objeto
            posicaoFutura = Position + velocidade;

            //tests

            Position = posicaoFutura;

            base.UpdateWorldAndCollisions();
        }
    }
}
