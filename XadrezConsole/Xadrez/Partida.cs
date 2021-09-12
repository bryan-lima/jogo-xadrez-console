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

        public Peca VulneravelEnPassant { get; private set; }

        public Partida()
        {
            Tabuleiro = new TabuleiroXadrez(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            VulneravelEnPassant = null;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca ExecutaMovimento(Posicao posicaoOrigem, Posicao posicaoDestino)
        {
            Peca peca = Tabuleiro.RetirarPeca(posicaoOrigem);
            peca.IncrementarQuantidadeMovimentos();
            Peca pecaCapturada = Tabuleiro.RetirarPeca(posicaoDestino);
            Tabuleiro.ColocarPeca(peca, posicaoDestino);
            if (pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }

            #region Jogada Especial - Roque Pequeno
            if (peca is Rei && posicaoDestino.Coluna == posicaoOrigem.Coluna + 2)
            {
                Posicao posicaoOrigemTorre = new Posicao(posicaoOrigem.Linha, posicaoOrigem.Coluna + 3);
                Posicao posicaoDestinoTorre = new Posicao(posicaoOrigem.Linha, posicaoOrigem.Coluna + 1);
                Peca torre = Tabuleiro.RetirarPeca(posicaoOrigemTorre);
                torre.IncrementarQuantidadeMovimentos();
                Tabuleiro.ColocarPeca(torre, posicaoDestinoTorre);
            }
            #endregion

            #region Jogada Especial - Roque Grande
            if (peca is Rei && posicaoDestino.Coluna == posicaoOrigem.Coluna - 2)
            {
                Posicao posicaoOrigemTorre = new Posicao(posicaoOrigem.Linha, posicaoOrigem.Coluna - 4);
                Posicao posicaoDestinoTorre = new Posicao(posicaoOrigem.Linha, posicaoOrigem.Coluna - 1);
                Peca torre = Tabuleiro.RetirarPeca(posicaoOrigemTorre);
                torre.IncrementarQuantidadeMovimentos();
                Tabuleiro.ColocarPeca(torre, posicaoDestinoTorre);
            }
            #endregion

            #region Jogada Especial - En Passant
            if (peca is Peao)
            {
                if (posicaoOrigem.Coluna != posicaoDestino.Coluna && pecaCapturada == null)
                {
                    Posicao posicaoPeaoCapturado;

                    if (peca.Cor == Cor.Branca)
                    {
                        posicaoPeaoCapturado = new Posicao(posicaoDestino.Linha + 1, posicaoDestino.Coluna);
                    }
                    else
                    {
                        posicaoPeaoCapturado = new Posicao(posicaoDestino.Linha - 1, posicaoDestino.Coluna);
                    }

                    pecaCapturada = Tabuleiro.RetirarPeca(posicaoPeaoCapturado);
                    Capturadas.Add(pecaCapturada);
                }
            }
            #endregion

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

            #region Jogada Especial - Roque Pequeno
            if (peca is Rei && posicaoDestino.Coluna == posicaoOrigem.Coluna + 2)
            {
                Posicao posicaoOrigemTorre = new Posicao(posicaoOrigem.Linha, posicaoOrigem.Coluna + 3);
                Posicao posicaoDestinoTorre = new Posicao(posicaoOrigem.Linha, posicaoOrigem.Coluna + 1);
                Peca torre = Tabuleiro.RetirarPeca(posicaoDestinoTorre);
                torre.DecrementarQuantidadeMovimentos();
                Tabuleiro.ColocarPeca(torre, posicaoOrigemTorre);
            }
            #endregion

            #region Jogada Especial - Roque Grande
            if (peca is Rei && posicaoDestino.Coluna == posicaoOrigem.Coluna - 2)
            {
                Posicao posicaoOrigemTorre = new Posicao(posicaoOrigem.Linha, posicaoOrigem.Coluna - 4);
                Posicao posicaoDestinoTorre = new Posicao(posicaoOrigem.Linha, posicaoOrigem.Coluna - 1);
                Peca torre = Tabuleiro.RetirarPeca(posicaoDestinoTorre);
                torre.DecrementarQuantidadeMovimentos();
                Tabuleiro.ColocarPeca(torre, posicaoOrigemTorre);
            }
            #endregion

            #region Jogada Especial - En Passant
            if (peca is Peao)
            {
                if (posicaoOrigem.Coluna != posicaoDestino.Coluna && pecaCapturada == VulneravelEnPassant)
                {
                    Peca peao = Tabuleiro.RetirarPeca(posicaoDestino);
                    Posicao posicaoPeao;
                    if (peao.Cor == Cor.Branca)
                    {
                        posicaoPeao = new Posicao(3, posicaoDestino.Coluna);
                    }
                    else
                    {
                        posicaoPeao = new Posicao(4, posicaoDestino.Coluna);
                    }

                    Tabuleiro.ColocarPeca(peao, posicaoPeao);
                }
            }
            #endregion
        }

        public void RealizaJogada(Posicao posicaoOrigem, Posicao posicaoDestino)
        {
            Peca pecaCapturada = ExecutaMovimento(posicaoOrigem, posicaoDestino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(posicaoOrigem, posicaoDestino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            Peca peca = Tabuleiro.Peca(posicaoDestino);

            #region Jogada Especial - Promoção
            if (peca is Peao)
            {
                if ((peca.Cor == Cor.Branca && posicaoDestino.Linha == 0) || (peca.Cor == Cor.Preta && posicaoDestino.Linha == 7))
                {
                    peca = Tabuleiro.RetirarPeca(posicaoDestino);
                    Pecas.Remove(peca);
                    Peca dama = new Dama(Tabuleiro, peca.Cor);
                    Tabuleiro.ColocarPeca(dama, posicaoDestino);
                    Pecas.Add(dama);
                }
            }
            #endregion

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

            #region Jogada Especial - En Passant
            if (peca is Peao && (posicaoDestino.Linha == posicaoOrigem.Linha - 2 || posicaoDestino.Linha == posicaoOrigem.Linha + 2))
            {
                VulneravelEnPassant = peca;
            }
            else
            {
                VulneravelEnPassant = null;
            }
            #endregion
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
            ColocarNovaPeca('e', 1, new Rei(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('f', 1, new Bispo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('g', 1, new Cavalo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('h', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('a', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('b', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('c', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('d', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('e', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('f', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('g', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('h', 2, new Peao(Tabuleiro, Cor.Branca, this));

            ColocarNovaPeca('a', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('b', 8, new Cavalo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('c', 8, new Bispo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 8, new Dama(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 8, new Rei(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('f', 8, new Bispo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('g', 8, new Cavalo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('a', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('b', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('c', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('d', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('e', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('f', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('g', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('h', 7, new Peao(Tabuleiro, Cor.Preta, this));
        }
    }
}
