using System;
using System.Collections.Generic;
using Tabuleiro;

namespace Xadrez
{
    class Partida
    {
        public TabuleiroXadrez Tabuleiro { get; private set; }

        public int Turno { get; private set; }

        public Cor JogadorAtual { get; private set; }

        public bool Terminada { get; private set; }

        private HashSet<Peca> Pecas;

        private HashSet<Peca> Capturadas;

        public bool Xeque { get; private set; }

        public Partida()
        {
            Tabuleiro = new TabuleiroXadrez(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca ExecutaMovimento(Posicao posicaOrigem, Posicao posicaoDestino)
        {
            Peca peca = Tabuleiro.RetirarPeca(posicaOrigem);
            peca.IncrementarQuantidadeMovimentos();
            Peca pecaCapturada = Tabuleiro.RetirarPeca(posicaoDestino);
            Tabuleiro.ColocarPeca(peca, posicaoDestino);

            if (pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }

            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao posicaoOrigem, Posicao posicaoDestino, Peca pecaCapturada)
        {
            Peca peca = Tabuleiro.RetirarPeca(posicaoDestino);
            peca.DecrementarQuantidadeMovimentos();

            if (pecaCapturada != null)
            {
                Tabuleiro.ColocarPeca(pecaCapturada, posicaoDestino);
                Capturadas.Remove(pecaCapturada);
            }

            Tabuleiro.ColocarPeca(peca, posicaoOrigem);
        }

        public void RealizaJogada(Posicao posicaoOrigem, Posicao posicaoDestino)
        {
            Peca pecaCapturada = ExecutaMovimento(posicaoOrigem, posicaoDestino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(posicaoOrigem, posicaoDestino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            if (EstaEmXeque(Adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if (TesteXequeMate(Adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }
        }

        public void ValidarPosicaoOrigem(Posicao posicaoOrigem)
        {
            if (Tabuleiro.Peca(posicaoOrigem) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }

            if (JogadorAtual != Tabuleiro.Peca(posicaoOrigem).Cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }

            if (!Tabuleiro.Peca(posicaoOrigem).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void ValidarPosicaoDestino(Posicao posicaoOrigem, Posicao posicaoDestino)
        {
            if (!Tabuleiro.Peca(posicaoOrigem).MovimentoPossivel(posicaoDestino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        private void MudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> pecasCapturadas = new HashSet<Peca>();

            foreach (Peca peca in Capturadas)
            {
                if (peca.Cor == cor)
                {
                    pecasCapturadas.Add(peca);
                }
            }

            return pecasCapturadas;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> pecasEmJogo = new HashSet<Peca>();

            foreach (Peca peca in Pecas)
            {
                if (peca.Cor == cor)
                {
                    pecasEmJogo.Add(peca);
                }
            }

            pecasEmJogo.ExceptWith(PecasCapturadas(cor));

            return pecasEmJogo;
        }

        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca Rei(Cor cor)
        {
            foreach (Peca peca in PecasEmJogo(cor))
            {
                if (peca is Rei)
                {
                    return peca;
                }
            }

            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca rei = Rei(cor);

            if (rei == null)
            {
                throw new TabuleiroException($"Não tem Rei da cor {cor} no tabuleiro!");
            }

            foreach (Peca peca in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] matrixMovimentosPossiveis = peca.MovimentosPossiveis();
                if (matrixMovimentosPossiveis[rei.Posicao.Linha, rei.Posicao.Coluna])
                {
                    return true;
                }
            }

            return false;
        }

        public bool TesteXequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }

            foreach (Peca peca in PecasEmJogo(cor))
            {
                bool[,] matrixMovimentosPossiveis = peca.MovimentosPossiveis();
                for (int linha = 0; linha < Tabuleiro.Linhas; linha++)
                {
                    for (int coluna = 0; coluna < Tabuleiro.Colunas; coluna++)
                    {
                        if (matrixMovimentosPossiveis[linha, coluna])
                        {
                            Posicao posicaoOrigem = peca.Posicao;
                            Posicao posicaoDestino = new Posicao(linha, coluna);
                            Peca pecaCapturada = ExecutaMovimento(posicaoOrigem, posicaoDestino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(posicaoOrigem, posicaoDestino, pecaCapturada);

                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('a', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('b', 1, new Cavalo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('c', 1, new Bispo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('d', 1, new Dama(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('e', 1, new Rei(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('f', 1, new Bispo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('g', 1, new Cavalo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('h', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('a', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('b', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('c', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('d', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('e', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('f', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('g', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('h', 2, new Peao(Tabuleiro, Cor.Branca));

            ColocarNovaPeca('a', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('b', 8, new Cavalo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('c', 8, new Bispo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 8, new Dama(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 8, new Rei(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('f', 8, new Bispo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('g', 8, new Cavalo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('a', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('b', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('c', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('f', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('g', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('h', 7, new Peao(Tabuleiro, Cor.Preta));
        }
    }
}
