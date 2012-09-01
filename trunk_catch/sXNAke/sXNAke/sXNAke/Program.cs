using System;

namespace sXNAke
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            //porquê o using aqui?
            /*
             * Porque o using define um escopo, fora do qual o/s objeto/s será descartado
             * 
             * C#, usando o framework .NET (e o CLR, commom language runtime, o executável)
             * automaticamente libera a memória usada para armazenar objetos que não são mais necessários.
             * A liberação da memória é não-determinística (fudeeeu!), a memória é liberada quando o CLR
             * decidir fazer a coleta de lixo. É melhor liberar recursos limitados (como arquivos e conexões)
             * o quanto antes. O using permite ao programador especificar quando objetos que  usam recursos
             * devem liberá-los. No nosso caso, asism que o jogo terminar de rodar libera toda a memória!
             */
            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
#endif
}

