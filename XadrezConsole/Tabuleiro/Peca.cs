namespace Tabuleiro
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }

        public Cor Cor { get; protected set; }

        public int QuantidadeMovimentos { get; protected set; }

        public TabuleiroXadrez Tabuleiro { get; set; }

        public Peca(TabuleiroXadrez tabuleiro, Cor cor)
        {
            this.Posicao = null;
            this.Tabuleiro = tabuleiro;
            this.Cor = cor;
            this.QuantidadeMovimentos = 0;
        }

        public void IncrementarQuantidadeMovimentos()
        {
            QuantidadeMovimentos++;
        }

        public bool ExisteMovimentosPossiveis()
        {
            bool[,] matrizMovimentosPossiveis = MovimentosPossiveis();

            for (int linha = 0; linha < Tabuleiro.Linhas; linha++)
            {
                for (int coluna = 0; coluna < Tabuleiro.Colunas; coluna++)
                {
                    if (matrizMovimentosPossiveis[linha, coluna])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}
