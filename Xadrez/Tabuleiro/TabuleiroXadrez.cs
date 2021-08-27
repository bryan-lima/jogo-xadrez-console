namespace Tabuleiro
{
    class TabuleiroXadrez
    {
        public int Linhas { get; set; }

        public int Colunas { get; set; }

        private Peca[,] Pecas;

        public TabuleiroXadrez(int linhas, int colunas)
        {
            this.Linhas = linhas;
            this.Colunas = colunas;
            Pecas = new Peca[linhas, colunas];
        }
    }
}
