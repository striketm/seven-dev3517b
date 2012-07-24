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
        
        public void Update(GameTime gameTime, KeyboardState teclado_atual, KeyboardState teclado_anterior)
        {
            //preciso testar *primeiro* se a posição futura é válida para não travar, por isso a guardo...
            Vector3 posicaoFutura = Posicao;

            //estas duas variáveis temporárias servem para o giro e o movimento
            float quantoVirar = 0;
            Vector3 movimento = Vector3.Zero;

            if (teclado_atual.IsKeyDown(Keys.A) || teclado_atual.IsKeyDown(Keys.Left))
            {
                quantoVirar = 1;
            }
            else if (teclado_atual.IsKeyDown(Keys.D) || teclado_atual.IsKeyDown(Keys.Right))
            {
                quantoVirar = -1;
            }
            if (teclado_atual.IsKeyDown(Keys.Up))
            {
                //positivo, correto?
                movimento.Z = -1;
            }
            else if (teclado_atual.IsKeyDown(Keys.S) || teclado_atual.IsKeyDown(Keys.Down))
            {
                movimento.Z = 1;
            }
            
            //o pra onde ele vai virar é corrigido pela velocidade de giro
            DirecaoFrontal += quantoVirar * VelocidadeDeCurva;
            //isto está aqui apenas pq esta variável tem outro nome na hora de desenhar...
            RotacaoY = DirecaoFrontal;
            //preciso girar meu objeto naquela direção antes de andar
            Matrix matrixOrientacao = Matrix.CreateRotationY(DirecaoFrontal);
            //ando com ele aquela quantidade de movimento naquela direcao de orientacao
            Vector3 velocidade = Vector3.Transform(movimento, matrixOrientacao);
            //corrijo pela escala da velocidade
            velocidade *= Velocidade;
            //agora sim esta será a nova posicao do objeto
            posicaoFutura = Posicao + velocidade;

            if ((Math.Abs(posicaoFutura.X) > AlcanceMaximo))//ta dando a volta...
            {
                posicaoFutura.X = AlcanceMaximo;            
            }
            if ((Math.Abs(posicaoFutura.Z) > AlcanceMaximo))
            {
                posicaoFutura.Z = AlcanceMaximo;
            }

            Posicao = posicaoFutura;
                        
        }

        private bool ManterNoTabuleiro(Vector3 posicaoFutura)
        {
            if ((Math.Abs(posicaoFutura.X) > AlcanceMaximo) ||
                (Math.Abs(posicaoFutura.Z) > AlcanceMaximo))
                return false;
            return true;
        }
    }
}
