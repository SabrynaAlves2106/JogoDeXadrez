using tabuleiro;

namespace xadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Cor cor, Tabuleiro tab) : base(cor, tab) { }

        public override string ToString()
        {
            return "C";
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
            // Acima 
            pos.definirValores(posicao.Linha - 1, posicao.Coluna-2);
            if (tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //ne
            pos.definirValores(posicao.Linha - 2, posicao.Coluna - 1);
            if (tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //direta
            pos.definirValores(posicao.Linha -2, posicao.Coluna + 1);
            if (tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.definirValores(posicao.Linha - 1, posicao.Coluna + 2);
            if (tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.definirValores(posicao.Linha + 1, posicao.Coluna+2);
            if (tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.definirValores(posicao.Linha + 2, posicao.Coluna + 1);
            if (tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.definirValores(posicao.Linha+2, posicao.Coluna - 1);
            if (tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.definirValores(posicao.Linha + 1, posicao.Coluna - 2);
            if (tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            return mat;
        }

    }
}
