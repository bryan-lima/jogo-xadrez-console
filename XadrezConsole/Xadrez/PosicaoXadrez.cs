using Tabuleiro;

namespace Xadrez
{
    class PosicaoXadrez
    {
        public char Coluna { get; set; }

        public int Linha { get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            this.Linha = linha;
            this.Coluna = coluna;
        }

        public Posicao ToPosicao()
        {
            return new Posicao(8 - Linha, Coluna - 'a');
        }

        public override string ToString()
        {
            return "" + Coluna + Linha;
        }
    }
}
