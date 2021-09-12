using Tabuleiro;

namespace Xadrez
{
    class Rei : Peca
    {
        private Partida Partida;

        public Rei(TabuleiroXadrez tabuleiro, Cor cor, Partida partida) : base(tabuleiro, cor)
        {
            this.Partida = partida;
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

        private bool TesteTorraParaRoque(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);
            
            return peca != null 
                        && peca is Torre 
                        && peca.Cor == Cor 
                        && peca.QuantidadeMovimentos == 0;
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

            #region Jogada Especial - Roque
            if (QuantidadeMovimentos == 0 && !Partida.Xeque)
            {
                #region Jogada Especial - Roque Pequeno
                Posicao posicaoTorreRoquePequeno = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
                if (TesteTorraParaRoque(posicaoTorreRoquePequeno))
                {
                    Posicao posicaoReiMais1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao posicaoReiMais2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);

                    if (Tabuleiro.Peca(posicaoReiMais1) == null && Tabuleiro.Peca(posicaoReiMais2) == null)
                    {
                        matrizMovimentosPossiveis[Posicao.Linha, Posicao.Coluna + 2] = true;
                    }
                }
                #endregion

                #region Jogada Especial - Roque Grande
                Posicao posicaoTorreRoqueGrande = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
                if (TesteTorraParaRoque(posicaoTorreRoqueGrande))
                {
                    Posicao posicaoReiMenos1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao posicaoReiMenos2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao posicaoReiMenos3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);

                    if (Tabuleiro.Peca(posicaoReiMenos1) == null && Tabuleiro.Peca(posicaoReiMenos2) == null && Tabuleiro.Peca(posicaoReiMenos3) == null)
                    {
                        matrizMovimentosPossiveis[Posicao.Linha, Posicao.Coluna - 2] = true;
                    }
                }
                #endregion
            }
            #endregion

            return matrizMovimentosPossiveis;
        }
    }
}
