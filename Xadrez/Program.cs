using System;
using Tabuleiro;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            TabuleiroXadrez tabuleiro = new TabuleiroXadrez(8, 8);

            Tela.ImprimirTabuleiro(tabuleiro);

            Console.ReadLine();
        }
    }
}
