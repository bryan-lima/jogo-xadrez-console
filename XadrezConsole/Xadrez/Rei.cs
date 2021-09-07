using Tabuleiro;

namespace Xadrez
{
    class Rei : Peca
    {
        public Rei(TabuleiroXadrez tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        public override string ToString()
        {
            return "R";
        }

        private bool PodeMover(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matrizMovimentosPossiveis = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao posicao = new Posicao(0, 0);

            #region Norte
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matrizMovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
            }
            #endregion

            #region Nordeste
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matrizMovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
            }
            #endregion

            #region Leste / Direita
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matrizMovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
            }
            #endregion

            #region Sudeste
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matrizMovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
            }
            #endregion

            #region Sul
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matrizMovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
            }
            #endregion

            #region Sudoeste
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matrizMovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
            }
            #endregion

            #region Oeste / Esquerda
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matrizMovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
            }
            #endregion

            #region Noroeste
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matrizMovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
            }
            #endregion

            return matrizMovimentosPossiveis;
        }
    }
}
