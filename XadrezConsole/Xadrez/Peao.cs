using Tabuleiro;

namespace Xadrez
{
    class Peao : Peca
    {
        private Partida Partida;

        public Peao(TabuleiroXadrez tabuleiro, Cor cor, Partida partida) : base(tabuleiro, cor)
        {
            this.Partida = partida;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool ExisteInimigo(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca != null && peca.Cor != Cor;
        }

        private bool Livre(Posicao posicao)
        {
            return Tabuleiro.Peca(posicao) == null;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matrizMovimentosPossiveis = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao posicao = new Posicao(0, 0);

            if (Cor == Cor.Branca)
            {
                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao))
                {
                    matrizMovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao) && QuantidadeMovimentos == 0)
                {
                    matrizMovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matrizMovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matrizMovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
                }

                #region Jogada Especial - En Passant
                if (Posicao.Linha == 3)
                {
                    Posicao posicaoEsquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tabuleiro.PosicaoValida(posicaoEsquerda) && ExisteInimigo(posicaoEsquerda) && Tabuleiro.Peca(posicaoEsquerda) == Partida.VulneravelEnPassant)
                    {
                        matrizMovimentosPossiveis[posicaoEsquerda.Linha - 1, posicaoEsquerda.Coluna] = true;
                    }

                    Posicao posicaoDireita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tabuleiro.PosicaoValida(posicaoDireita) && ExisteInimigo(posicaoDireita) && Tabuleiro.Peca(posicaoDireita) == Partida.VulneravelEnPassant)
                    {
                        matrizMovimentosPossiveis[posicaoDireita.Linha - 1, posicaoDireita.Coluna] = true;
                    }
                }
                #endregion
            }
            else
            {
                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao))
                {
                    matrizMovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao) && QuantidadeMovimentos == 0)
                {
                    matrizMovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matrizMovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matrizMovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
                }

                #region Jogada Especial - En Passant
                if (Posicao.Linha == 4)
                {
                    Posicao posicaoEsquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tabuleiro.PosicaoValida(posicaoEsquerda) && ExisteInimigo(posicaoEsquerda) && Tabuleiro.Peca(posicaoEsquerda) == Partida.VulneravelEnPassant)
                    {
                        matrizMovimentosPossiveis[posicaoEsquerda.Linha + 1, posicaoEsquerda.Coluna] = true;
                    }

                    Posicao posicaoDireita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tabuleiro.PosicaoValida(posicaoDireita) && ExisteInimigo(posicaoDireita) && Tabuleiro.Peca(posicaoDireita) == Partida.VulneravelEnPassant)
                    {
                        matrizMovimentosPossiveis[posicaoDireita.Linha + 1, posicaoDireita.Coluna] = true;
                    }
                }
                #endregion
            }

            return matrizMovimentosPossiveis;
        }
    }
}
