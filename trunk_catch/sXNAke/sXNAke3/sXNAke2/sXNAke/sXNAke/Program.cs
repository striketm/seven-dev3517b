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
            //porqu� o using aqui?
            /*
             * Porque o using define um escopo, fora do qual o/s objeto/s ser� descartado
             * 
             * C#, usando o framework .NET (e o CLR, commom language runtime, o execut�vel)
             * automaticamente libera a mem�ria usada para armazenar objetos que n�o s�o mais necess�rios.
             * A libera��o da mem�ria � n�o-determin�stica (fudeeeu!), a mem�ria � liberada quando o CLR
             * decidir fazer a coleta de lixo. � melhor liberar recursos limitados (como arquivos e conex�es)
             * o quanto antes. O using permite ao programador especificar quando objetos que  usam recursos
             * devem liber�-los. No nosso caso, asism que o jogo terminar de rodar libera toda a mem�ria!
             */
            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
#endif
}

