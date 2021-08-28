using System;
using Tabuleiro;
using Xadrez;

namespace XadrezConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try { 
            TabuleiroXadrez tabuleiro = new TabuleiroXadrez(8, 8);

            tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(0, 0));
            tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(1, 9));
            tabuleiro.ColocarPeca(new Rei(tabuleiro, Cor.Preta), new Posicao(0, 2));
            //tabuleiro.ColocarPeca(???, new Posicao(6, 4));
            //tabuleiro.ColocarPeca(???, new Posicao(7, 0));
            //tabuleiro.ColocarPeca(???, new Posicao(7, 5));
            //tabuleiro.ColocarPeca(???, new Posicao(7, 7));

            Tela.ImprimirTabuleiro(tabuleiro);
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
