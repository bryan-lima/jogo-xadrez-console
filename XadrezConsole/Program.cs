using System;
using Tabuleiro;
using Xadrez;

namespace XadrezConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            PosicaoXadrez posicaoXadrez = new PosicaoXadrez('a', 1);

            Console.WriteLine(posicaoXadrez);

            Console.WriteLine(posicaoXadrez.ToPosicao());

            Console.ReadLine();
        }
    }
}
