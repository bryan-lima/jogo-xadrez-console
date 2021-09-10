using Tabuleiro;

namespace Xadrez
{
    class Peao : Peca
    {
        public Peao(TabuleiroXadrez tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

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
                posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao))
                {
                    matrizMovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(posicao.Linha - 2, posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao) && QuantidadeMovimentos == 0)
                {
                    matrizMovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matrizMovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matrizMovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
                }
            }
            else
            {
                posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao))
                {
                    matrizMovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(posicao.Linha + 2, posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao) && QuantidadeMovimentos == 0)
                {
                    matrizMovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matrizMovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matrizMovimentosPossiveis[posicao.Linha, posicao.Coluna] = true;
                }
            }

            return matrizMovimentosPossiveis;
        }
    }
}
