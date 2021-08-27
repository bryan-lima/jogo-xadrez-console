using System;
using Tabuleiro;

namespace XadrezConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            TabuleiroXadrez tabuleiro = new TabuleiroXadrez(8, 8);

            //tabuleiro.ColocarPeca(???, new Posicao(0, 0));
            //tabuleiro.ColocarPeca(???, new Posicao(1, 3));
            //tabuleiro.ColocarPeca(???, new Posicao(2, 4));
            //tabuleiro.ColocarPeca(???, new Posicao(6, 4));
            //tabuleiro.ColocarPeca(???, new Posicao(7, 0));
            //tabuleiro.ColocarPeca(???, new Posicao(7, 5));
            //tabuleiro.ColocarPeca(???, new Posicao(7, 7));

            Tela.ImprimirTabuleiro(tabuleiro);

            Console.ReadLine();
        }
    }
}
