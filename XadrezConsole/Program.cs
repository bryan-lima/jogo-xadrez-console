using System;
using Tabuleiro;
using Xadrez;

namespace XadrezConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Partida partida = new Partida();

                while (!partida.Terminada)
                {
                    Console.Clear();
                    
                    Tela.ImprimirTabuleiro(partida.Tabuleiro);

                    Console.Write("\nOrigem: ");
                    Posicao posicaoOrigem = Tela.LerPosicaoXadrez().ToPosicao();
                    
                    Console.Write("Destino: ");
                    Posicao posicaoDestino = Tela.LerPosicaoXadrez().ToPosicao();

                    partida.ExecutaMovimento(posicaoOrigem, posicaoDestino);
                }
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
