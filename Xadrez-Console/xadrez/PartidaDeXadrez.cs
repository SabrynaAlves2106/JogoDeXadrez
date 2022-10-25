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

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            ColocarPecas();
        }
        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPecas(origem);
            p.IncrementarqtdMovimentos();
            Peca pecaCapturada = tab.RetirarPecas(destino);
            tab.ColocarPecas(p, destino);
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
            if (tab.Peca(pos).ExisteMovimentosPossiveis())
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
        private void ColocarPecas()
        {

            tab.ColocarPecas(new Torre(Cor.Branca, tab), new PosicaoXadrez('c', 1).toPosicao());
            tab.ColocarPecas(new Torre(Cor.Branca, tab), new PosicaoXadrez('c', 2).toPosicao());
            tab.ColocarPecas(new Torre(Cor.Branca, tab), new PosicaoXadrez('d', 2).toPosicao());
            tab.ColocarPecas(new Torre(Cor.Branca, tab), new PosicaoXadrez('e', 2).toPosicao());
            tab.ColocarPecas(new Torre(Cor.Branca, tab), new PosicaoXadrez('e', 1).toPosicao());
            tab.ColocarPecas(new Rei(Cor.Branca, tab), new PosicaoXadrez('d', 1).toPosicao());

            tab.ColocarPecas(new Torre(Cor.Preta, tab), new PosicaoXadrez('c', 7).toPosicao());
            tab.ColocarPecas(new Torre(Cor.Preta, tab), new PosicaoXadrez('c', 8).toPosicao());
            tab.ColocarPecas(new Torre(Cor.Preta, tab), new PosicaoXadrez('d', 7).toPosicao());
            tab.ColocarPecas(new Torre(Cor.Preta, tab), new PosicaoXadrez('e', 7).toPosicao());
            tab.ColocarPecas(new Torre(Cor.Preta, tab), new PosicaoXadrez('e', 8).toPosicao());
            tab.ColocarPecas(new Rei(Cor.Preta, tab), new PosicaoXadrez('d', 8).toPosicao());

        }
    }
}
