﻿using System;
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
                    try
                    {
                        Console.Clear();
                        Tela.ImprimirTabuleiro(partida.Tabuleiro);
                        Console.WriteLine($"\nTurno: {partida.Turno}");
                        Console.WriteLine($"Aguardando jogada: {partida.JogadorAtual}");

                        Console.Write("\nOrigem: ");
                        Posicao posicaoOrigem = Tela.LerPosicaoXadrez().ToPosicao();
                        partida.ValidarPosicaoOrigem(posicaoOrigem);

                        bool[,] posicoesPossiveis = partida.Tabuleiro.Peca(posicaoOrigem).MovimentosPossiveis();

                        Console.Clear();
                        Tela.ImprimirTabuleiro(partida.Tabuleiro, posicoesPossiveis);

                        Console.Write("\nDestino: ");
                        Posicao posicaoDestino = Tela.LerPosicaoXadrez().ToPosicao();

                        partida.RealizaJogada(posicaoOrigem, posicaoDestino);
                    }
                    catch(TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
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
