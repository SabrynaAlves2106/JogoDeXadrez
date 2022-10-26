using tabuleiro;

namespace xadrez
{
    class Peao : Peca
    {
        private PartidaDeXadrez partida; 
        public Peao(Cor cor, Tabuleiro tab,PartidaDeXadrez partida) : base(cor, tab) {
        this.partida = partida;
        }

        public override string ToString()
        {
            return "P";
        }
        private bool existeInimigo(Posicao pos)
        {
            Peca p = tab.Peca(pos);
            return p != null && p.cor != cor;
        }
        private bool livre(Posicao pos)
        {
            return tab.Peca(pos) == null;
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
            if (cor == Cor.Branca)
            {
                // Acima 
                pos.definirValores(posicao.Linha - 1, posicao.Coluna);
                if (tab.PosicaoValida(pos) && livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                //ne
                pos.definirValores(posicao.Linha - 2, posicao.Coluna);
                if (tab.PosicaoValida(pos) && livre(pos) && qtdMovimento == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                //direta
                pos.definirValores(posicao.Linha - 1, posicao.Coluna - 1);
                if (tab.PosicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.definirValores(posicao.Linha - 1, posicao.Coluna + 1);
                if (tab.PosicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                if (posicao.Linha == 3)
                {
                    Posicao esquerda = new Posicao(posicao.Linha, posicao.Coluna - 1);
                    if (tab.PosicaoValida(esquerda) && existeInimigo(esquerda) && tab.Peca(esquerda) == partida.vulneravelEnPasant)
                    {
                        mat[esquerda.Linha-1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(posicao.Linha, posicao.Coluna + 1);
                    if (tab.PosicaoValida(direita) && existeInimigo(direita) && tab.Peca(direita) == partida.vulneravelEnPasant)
                    {
                        mat[direita.Linha-1, direita.Coluna] = true;
                    }
                }
            }
            else
            {
                pos.definirValores(posicao.Linha + 1, posicao.Coluna);
                if (tab.PosicaoValida(pos) && livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.definirValores(posicao.Linha + 2, posicao.Coluna);
                if (tab.PosicaoValida(pos) && livre(pos) && qtdMovimento == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.definirValores(posicao.Linha + 1, posicao.Coluna - 1);
                if (tab.PosicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.definirValores(posicao.Linha + 1, posicao.Coluna + 1);
                if (tab.PosicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                if (posicao.Linha == 4)
                {
                    Posicao esquerda = new Posicao(posicao.Linha, posicao.Coluna - 1);
                    if (tab.PosicaoValida(esquerda) && existeInimigo(esquerda) && tab.Peca(esquerda) == partida.vulneravelEnPasant)
                    {
                        mat[esquerda.Linha+1, esquerda.Coluna] = true;
                    }
                
                    Posicao direita = new Posicao(posicao.Linha, posicao.Coluna + 1);
                    if (tab.PosicaoValida(direita) && existeInimigo(direita) && tab.Peca(direita) == partida.vulneravelEnPasant)
                    {
                        mat[direita.Linha+1, direita.Coluna] = true;
                    }
                }

            }
            return mat;
        }

    }
}
