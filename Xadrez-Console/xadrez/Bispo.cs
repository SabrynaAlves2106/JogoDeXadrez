using tabuleiro;

namespace xadrez
{
    class Bispo : Peca
    {
        public Bispo(Cor cor, Tabuleiro tab) : base(cor, tab) { }

        public override string ToString()
        {
            return "B";
        }
        private bool podeMover(Posicao pos)
        {
            Peca p = tab.Peca(pos);
            return p == null || p.cor != this.cor;
        }
        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];
            Posicao pos = new Posicao(0, 0);
            // ND 
            pos.definirValores(posicao.Linha - 1, posicao.Coluna-1);
            while (tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (tab.Peca(pos) != null && tab.Peca(pos).cor != cor)
                {
                    break;
                }
                pos.definirValores(pos.Linha - 1, pos.Coluna - 1);
            }
            //NE
            pos.definirValores(posicao.Linha - 1, posicao.Coluna+1);
            while (tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (tab.Peca(pos) != null && tab.Peca(pos).cor != cor)
                {
                    break;
                }
                pos.definirValores(pos.Linha - 1, pos.Coluna + 1);
            }
            //SE
            pos.definirValores(posicao.Linha+1, posicao.Coluna + 1);
            while (tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (tab.Peca(pos) != null && tab.Peca(pos).cor != cor)
                {
                    break;
                }
                pos.definirValores(pos.Linha + 1, pos.Coluna + 1);
            }
            //SO
            pos.definirValores(posicao.Linha+1, posicao.Coluna - 1);
            while (tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (tab.Peca(pos) != null && tab.Peca(pos).cor != cor)
                {
                    break;
                }
                pos.definirValores(pos.Linha + 1, pos.Coluna - 1);
            }
            return mat;
        }

    }
}
