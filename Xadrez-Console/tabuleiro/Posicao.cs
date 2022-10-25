namespace tabuleiro
{
    public class Posicao
    {
        public int Linha { get; set; }
        public int Coluna { get; set; }

        public Posicao(int Linha, int Coluna)
        {
            this.Linha = Linha;
            this.Coluna = Coluna;
        }
        public void definirValores(int linha, int coluna)
        {
            this.Linha = linha;
            this.Coluna = coluna;
        }
        public override string ToString()
        {
            return Linha +
                ", "
                + Coluna;
        }
    }
}
