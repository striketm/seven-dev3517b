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
    class Camera
    {
        #region Atributos

        Vector3 posicao;
        /// <summary>
        /// Representa a posição da câmera, onde ela está.
        /// </summary>
        public Vector3 Posicao { get { return posicao; } set { posicao = value; } }

        Vector3 alvo;
        /// <summary>
        /// Representa o alvo da câmera, para onde ela aponta.
        /// </summary>
        public Vector3 Alvo { get { return alvo; } set { alvo = value; } }

        Vector3 orientacao;
        /// <summary>
        /// Representa a orientação da câmera, qual o seu "cima".
        /// </summary>
        public Vector3 Orientacao { get { return orientacao; } set { orientacao = value; } }

        float anguloAbertura;
        /// <summary>
        /// Representa o ângulo de abertura da "lente" da câmera.
        /// </summary>
        public float AnguloAbertura { get { return anguloAbertura; } set { anguloAbertura = value; } }

        float razaoTela;
        /// <summary>
        /// Representa a razão / proporção da tela / monitor (4/3, 16/9...).
        /// </summary>
        public float RazaoTela { get { return razaoTela; } set { razaoTela = value; } }

        float planoPerto;
        /// <summary>
        /// Representa o plano de corte de proximidade, mais perto que isso a câmera não mostra
        /// </summary>
        public float PlanoPerto { get { return planoPerto; } set { planoPerto = value; } }
        
        float planoLonge;
        /// <summary>
        /// Representa o plano de corte de distância, mais longe que isso a câmera não mostra
        /// </summary>
        public float PlanoLonge { get { return planoLonge; } set { planoLonge = value; } }
        
        Vector3 distanciaJogador;
        /// <summary>
        /// Representa o quão distante a câmera está do jogador (para "trás" e para "cima")
        /// </summary>
        public Vector3 DistanciaJogador { get { return distanciaJogador; } set { distanciaJogador = value; } }

        Vector3 distanciaAlvo;
        /// <summary>
        /// Representa o quão distante o alvo da câmera está do jogador (para "cima")
        /// </summary>
        public Vector3 DistanciaAlvo { get { return distanciaAlvo; } set { distanciaAlvo = value; } }
        
        Matrix matrizVisualizacao;
        /// <summary>
        /// Representa a matriz de visualização da câmera
        /// </summary>
        public Matrix MatrizVisualizacao { get { return matrizVisualizacao; } set { matrizVisualizacao = value; } }
        
        Matrix matrizProjecao;
        /// <summary>
        /// Representa a matriz de projeção da câmera
        /// </summary>
        public Matrix MatrizProjecao { get { return matrizProjecao; } set { matrizProjecao = value; } }

        public static List<Camera> listaCameras = new List<Camera>();

        //camera atual, e pode haver mais de uma view?

        #endregion

        #region Métodos

        /// <summary>
        /// Construtor com valores padrões de atributos
        /// </summary>
        public Camera()
        {
            Posicao = new Vector3(0, 0, 0);
            Alvo = Vector3.Zero;
            Orientacao = Vector3.Up;

            AnguloAbertura = 45.0f;
            RazaoTela = (4 / 3);
            PlanoPerto = 1.0f;
            PlanoLonge = 100.0f;

            DistanciaJogador = new Vector3(0, 10, 15);
            DistanciaAlvo = new Vector3(0, 5, 0);

            MatrizVisualizacao = Matrix.CreateLookAt(posicao, alvo, orientacao);

            MatrizProjecao = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(anguloAbertura),
                razaoTela,
                planoPerto,
                planoLonge);

            Camera.listaCameras.Add(this);

        }

        /// <summary>
        /// Atualização da câmera
        /// </summary>
        /// <param name="anguloRotacao">ângulo em relação ao jogador, para qual este aponta</param>
        /// <param name="_posicao">posição do jogador!</param>
        public void Update(float anguloRotacao, Vector3 _posicao)
        {
            //TODO corrigir

            Matrix matrizRotacao = Matrix.CreateRotationY(anguloRotacao);

            Vector3 distanciaCorrigida = Vector3.Transform(DistanciaJogador, matrizRotacao);
            Vector3 alvoCorrigido = Vector3.Transform(DistanciaAlvo, matrizRotacao);

            Posicao = _posicao + distanciaCorrigida;
            Alvo = _posicao + alvoCorrigido;

            MatrizVisualizacao = Matrix.CreateLookAt(Posicao, Alvo, Orientacao);

            MatrizProjecao = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(AnguloAbertura),
                RazaoTela,
                PlanoPerto,
                PlanoLonge);
        }

        #endregion

    }
}
