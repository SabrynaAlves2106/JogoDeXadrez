using tabuleiro;

namespace xadrez
{
    class Rei:Peca 
    {
        private PartidaDeXadrez partida;
        public Rei(Cor cor ,Tabuleiro tab,PartidaDeXadrez partida) : base(cor,  tab) {
            this.partida = partida;
        }

        public override string ToString()
        {
            return "R";
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
            pos.definirValores(posicao.Linha - 1, posicao.Coluna);
            if(tab.PosicaoValida(pos)&& podeMover(pos))
            {
                mat[pos.Linha,pos.Coluna] = true;
            }
            //ne
            pos.definirValores(posicao.Linha - 1, posicao.Coluna+1);
            if (tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //direta
            pos.definirValores(posicao.Linha, posicao.Coluna+1);
            if (tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.definirValores(posicao.Linha + 1, posicao.Coluna+1);
            if (tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.definirValores(posicao.Linha + 1, posicao.Coluna);
            if (tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.definirValores(posicao.Linha + 1, posicao.Coluna - 1);
            if (tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.definirValores(posicao.Linha, posicao.Coluna - 1);
            if (tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.definirValores(posicao.Linha - 1, posicao.Coluna - 1);
            if (tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            // jogadaEspecial roque
            if(qtdMovimento == 0 && !partida.xeque)
            {
                Posicao posT1 = new Posicao(posicao.Linha, posicao.Coluna + 3);
                if (testeTorreParaRoque(posT1))
                {
                    Posicao p1 = new Posicao(posicao.Linha, posicao.Coluna + 1);
                    Posicao p2 = new Posicao(posicao.Linha, posicao.Coluna + 2);
                    if(tab.Peca(p1)==null && tab.Peca(p2) == null)
                    {
                        mat[posicao.Linha, posicao.Coluna+2] = true;
                    }

                }
                Posicao posT2 = new Posicao(posicao.Linha, posicao.Coluna - 4);
                if (testeTorreParaRoque(posT2))
                {
                    Posicao p1 = new Posicao(posicao.Linha, posicao.Coluna - 1);
                    Posicao p2 = new Posicao(posicao.Linha, posicao.Coluna - 2);
                    Posicao p3 = new Posicao(posicao.Linha, posicao.Coluna - 3);

                    if (tab.Peca(p1) == null && tab.Peca(p2) == null && tab.Peca(p3) == null)
                    {
                        mat[posicao.Linha, posicao.Coluna - 2] = true;
                    }

                }
            }
            return mat;
        }
        private bool testeTorreParaRoque(Posicao pos)
        {
            Peca p = tab.Peca(pos);
            return p != null && p is Torre && p.cor == cor && p.qtdMovimento == 0;
        }
    }
}
