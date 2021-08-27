﻿namespace Tabuleiro
{
    class Peca
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
    }
}
