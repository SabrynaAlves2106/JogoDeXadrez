using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez
{
    public class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            ColocarPecas();
        }
        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPecas(origem);
            p.IncrementarqtdMovimentos();
            Peca pecaCapturada = tab.RetirarPecas(destino);
            tab.ColocarPecas(p, destino);
            if(pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
        }
        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in aux)
            {
                if(x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }
        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in aux)
            {
                if(x.cor == cor)
                {
                    aux.Add(x);
                }
            }
             aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            mudaJogador();
        }
        public void ValidarPosicaoOrigem(Posicao pos)
        {
            if (tab.Peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça nessa posição");
            }
            if(JogadorAtual!= tab.Peca(pos).cor)
            {
                throw new TabuleiroException("Não esta na vez desse jogador");
            }
            if (!tab.Peca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não existem movimentos possiveis para essa peça");
            }
        }
        public void validarPosicaodeDestino(Posicao origem,Posicao destino)
        {
            if (!tab.Peca(origem).podeMoverPara(destino))
                throw new TabuleiroException("Posição de destino invalida");
        }
        private void mudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
                JogadorAtual = Cor.Preta;
            else
                JogadorAtual = Cor.Branca;
        }
        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.ColocarPecas(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }
        private void ColocarPecas()
        {
            ColocarNovaPeca('c', 1, new Torre(Cor.Branca, tab));
            ColocarNovaPeca('c', 2,new Torre(Cor.Branca, tab));
            ColocarNovaPeca('d', 2,new Torre(Cor.Branca, tab));
            ColocarNovaPeca('e', 2,new Torre(Cor.Branca, tab));
            ColocarNovaPeca('e', 1, new Torre(Cor.Branca, tab));
            ColocarNovaPeca('d', 1, new Rei(Cor.Branca, tab));

            ColocarNovaPeca('c', 7,new Torre(Cor.Preta, tab));
            ColocarNovaPeca('c', 8,new Torre(Cor.Preta, tab));
            ColocarNovaPeca('d', 7,new Torre(Cor.Preta, tab));
            ColocarNovaPeca('e', 7,new Torre(Cor.Preta, tab));
            ColocarNovaPeca('e', 8,new Torre(Cor.Preta, tab));
            ColocarNovaPeca('d', 8, new Rei(Cor.Preta, tab));

        }
    }
}
