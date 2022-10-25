namespace tabuleiro
{
    public abstract class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; set; }
        public int qtdMovimento { get; protected set; }
        public Tabuleiro tab { get; protected set; }

        public Peca(Cor cor ,Tabuleiro tab)
        {
            this.posicao = null;
            this.cor = cor;
            this.tab = tab;
            qtdMovimento = 0;
        }
        public void IncrementarqtdMovimentos()
        {
            qtdMovimento++;
        }
        public void decrementarqtdMovimentos()
        {
            qtdMovimento--;
        }
        public bool ExisteMovimentosPossiveis()
        {
            bool[,] math = movimentosPossiveis();
            for(int i = 0; i < tab.linhas; i++)
            {
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (math[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool MovimentoPossivel(Posicao pos)
        {
            return movimentosPossiveis()[pos.Linha, pos.Coluna];
        }
        public abstract bool[,] movimentosPossiveis();
    }
}
