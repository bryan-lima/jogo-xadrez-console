using Tabuleiro;

namespace Xadrez
{
    class Torre : Peca
    {
        public Torre(TabuleiroXadrez tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        public override string ToString()
        {
            return "T";
        }
    }
}
