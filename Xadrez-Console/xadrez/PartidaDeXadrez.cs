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
        public bool xeque { get; private set; }
        public Peca vulneravelEnPasant { get; private set; }
        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            terminada = false;
            vulneravelEnPasant = null;
            xeque = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }
        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Preta)
            {
                return Cor.Branca;
            }
            else
            {
                return Cor.Preta;
            }
        }
        private Peca Rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }
        public bool estaEmXeque(Cor cor)
        {
            Peca R = Rei(cor);
            if (R == null)
                throw new TabuleiroException("não tem rei da cor " + cor + " no tabuleiro");
            foreach (Peca x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.posicao.Linha, R.posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }
        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);
            if (estaEmXeque(JogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em cheque");
            }
            if (estaEmXeque(adversaria(JogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }
            if (testeXequeMate(adversaria(JogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                mudaJogador();
                Turno++;
            }
            Peca p = tab.Peca(destino);
            if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {
                vulneravelEnPasant = p;
            }
            else
            {
                vulneravelEnPasant = null;
            }
        }
        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.RetirarPecas(destino);
            p.decrementarqtdMovimentos();
            if (pecaCapturada != null)
            {
                tab.ColocarPecas(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.ColocarPecas(p, origem);

            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tab.RetirarPecas(destino);
                T.decrementarqtdMovimentos();
                tab.ColocarPecas(T, origemT);
            }
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tab.RetirarPecas(destinoT);
                T.decrementarqtdMovimentos();
                tab.ColocarPecas(T, origemT);
            }
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == vulneravelEnPasant)
                {
                    Peca peao = tab.RetirarPecas(destino);
                    Posicao posP;
                    if (p.cor is Cor.Branca)
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);
                    else
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    tab.ColocarPecas(peao, posP);
                }
            }
        }
        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPecas(origem);
            p.IncrementarqtdMovimentos();
            Peca pecaCapturada = tab.RetirarPecas(destino);
            tab.ColocarPecas(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tab.RetirarPecas(origemT);
                T.IncrementarqtdMovimentos();
                tab.ColocarPecas(T, destinoT);
            }
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tab.RetirarPecas(origemT);
                T.IncrementarqtdMovimentos();
                tab.ColocarPecas(T, destinoT);
            }
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada is null)
                {
                    Posicao posP;
                    if (p.cor is Cor.Branca)
                    {
                        posP = new Posicao(destino.Linha + 1, origem.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.Linha - 1, origem.Coluna);
                    }
                    pecaCapturada = tab.RetirarPecas(posP);
                    capturadas.Add(pecaCapturada);
                }
            }
            return pecaCapturada;
        }
        public void ValidarPosicaoOrigem(Posicao pos)
        {
            if (tab.Peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça nessa posição");
            }
            if (JogadorAtual != tab.Peca(pos).cor)
            {
                throw new TabuleiroException("Não esta na vez desse jogador");
            }
            if (!tab.Peca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não existem movimentos possiveis para essa peça");
            }
        }
        public bool testeXequeMate(Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in pecasEmJogo(cor))
            {
                Posicao origem = x.posicao;
                bool[,] mat = x.movimentosPossiveis();
                for (int i = 0; i < tab.linhas; i++)
                {
                    for (int j = 0; j < tab.colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        public void validarPosicaodeDestino(Posicao origem, Posicao destino)
        {
            if (!tab.Peca(origem).MovimentoPossivel(destino))
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
            ColocarNovaPeca('a', 1, new Torre(Cor.Branca, tab));
            ColocarNovaPeca('b', 1, new Cavalo(Cor.Branca, tab));
            ColocarNovaPeca('c', 1, new Bispo(Cor.Branca, tab));
            ColocarNovaPeca('d', 1, new Dama(Cor.Branca, tab));
            ColocarNovaPeca('e', 1, new Rei(Cor.Branca, tab, this));
            ColocarNovaPeca('f', 1, new Bispo(Cor.Branca, tab));
            ColocarNovaPeca('g', 1, new Cavalo(Cor.Branca, tab));
            ColocarNovaPeca('h', 1, new Torre(Cor.Branca, tab));
            ColocarNovaPeca('a', 2, new Peao(Cor.Branca, tab, this));
            ColocarNovaPeca('b', 2, new Peao(Cor.Branca, tab, this));
            ColocarNovaPeca('c', 2, new Peao(Cor.Branca, tab, this));
            ColocarNovaPeca('d', 2, new Peao(Cor.Branca, tab, this));
            ColocarNovaPeca('e', 2, new Peao(Cor.Branca, tab, this));
            ColocarNovaPeca('f', 2, new Peao(Cor.Branca, tab, this));
            ColocarNovaPeca('g', 2, new Peao(Cor.Branca, tab, this));
            ColocarNovaPeca('h', 2, new Peao(Cor.Branca, tab, this));

            ColocarNovaPeca('a', 8, new Torre(Cor.Preta, tab));
            ColocarNovaPeca('b', 8, new Cavalo(Cor.Preta, tab));
            ColocarNovaPeca('c', 8, new Bispo(Cor.Preta, tab));
            ColocarNovaPeca('d', 8, new Dama(Cor.Preta, tab));
            ColocarNovaPeca('e', 8, new Rei(Cor.Preta, tab, this));
            ColocarNovaPeca('f', 8, new Bispo(Cor.Preta, tab));
            ColocarNovaPeca('g', 8, new Cavalo(Cor.Preta, tab));
            ColocarNovaPeca('h', 8, new Torre(Cor.Preta, tab));
            ColocarNovaPeca('a', 7, new Peao(Cor.Preta, tab, this));
            ColocarNovaPeca('b', 7, new Peao(Cor.Preta, tab, this));
            ColocarNovaPeca('c', 7, new Peao(Cor.Preta, tab, this));
            ColocarNovaPeca('d', 7, new Peao(Cor.Preta, tab, this));
            ColocarNovaPeca('e', 7, new Peao(Cor.Preta, tab, this));
            ColocarNovaPeca('f', 7, new Peao(Cor.Preta, tab, this));
            ColocarNovaPeca('g', 7, new Peao(Cor.Preta, tab, this));
            ColocarNovaPeca('h', 7, new Peao(Cor.Preta, tab, this));
        }
    }
}
