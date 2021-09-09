using System;
using System.Collections.Generic;
using Tabuleiro;
using Xadrez;

namespace XadrezConsole
{
    class Tela
    {
        public static void ImprimirPartida(Partida partida)
        {
            ImprimirTabuleiro(partida.Tabuleiro);
            Console.WriteLine();
            ImprimirPecasCapturadas(partida);
            Console.WriteLine($"\nTurno {partida.Turno}");
            
            if (!partida.Terminada)
            {
                Console.WriteLine($"Aguardando jogada: {partida.JogadorAtual}");
                if (partida.Xeque)
                {
                    Console.WriteLine("XEQUE!");
                }
            }
            else
            {
                Console.WriteLine("XEQUE-MATE!");
                Console.WriteLine($"Vencedor: {partida.JogadorAtual}");
            }
        }

        public static void ImprimirPecasCapturadas(Partida partida)
        {
            Console.WriteLine("Peças capturadas:");
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branca));
            Console.Write("\nPretas: ");
            ConsoleColor consoleColorForegroundDefault = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(partida.PecasCapturadas(Cor.Preta));
            Console.ForegroundColor = consoleColorForegroundDefault;
            Console.WriteLine();
        }

        public static void ImprimirConjunto(HashSet<Peca> pecas)
        {
            Console.Write("[");
            foreach (Peca peca in pecas)
            {
                Console.Write($"{peca} ");
            }
            Console.Write("]");
        }

        public static void ImprimirTabuleiro(TabuleiroXadrez tabuleiro)
        {
            for (int linha = 0; linha < tabuleiro.Linhas; linha++)
            {
                Console.Write(8 - linha + " ");

                for (int coluna = 0; coluna < tabuleiro.Colunas; coluna++)
                {
                    ImprimirPeca(tabuleiro.Peca(linha, coluna));
                }

                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirTabuleiro(TabuleiroXadrez tabuleiro, bool[,] possicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoPosicaoPossivel = ConsoleColor.DarkGray;

            for (int linha = 0; linha < tabuleiro.Linhas; linha++)
            {
                Console.Write(8 - linha + " ");

                for (int coluna = 0; coluna < tabuleiro.Colunas; coluna++)
                {
                    if (possicoesPossiveis[linha, coluna])
                    {
                        Console.BackgroundColor = fundoPosicaoPossivel;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }

                    ImprimirPeca(tabuleiro.Peca(linha, coluna));
                    Console.BackgroundColor = fundoOriginal;
                }

                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundoOriginal;
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string posicaoInformada = Console.ReadLine();
            char coluna = posicaoInformada[0];
            int linha = int.Parse(posicaoInformada[1] + "");

            return new PosicaoXadrez(coluna, linha);
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.Cor == Cor.Branca)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor consoleForegroundColorDefault = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = consoleForegroundColorDefault;
                }

                Console.Write(" ");
            }
        }
    }
}
