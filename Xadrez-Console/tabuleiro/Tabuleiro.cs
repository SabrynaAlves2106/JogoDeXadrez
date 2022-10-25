using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tabuleiro
{
    public class Tabuleiro
    {
        public int linhas { get; set; }
        public int colunas { get; set; }
        private Peca[,] pecas;   

        public Tabuleiro(int linhas, int colunas)
        {
            this.linhas = linhas;
            this.colunas = colunas;
            pecas= new Peca[linhas,colunas];
        }

        public Peca Peca (int i, int j)
        {
            return pecas[i,j];
        }
        public Peca Peca(Posicao pos)
        {
            return pecas[pos.Linha, pos.Coluna];
        }
        public bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos);
            return Peca(pos) != null;
        }

        public void ColocarPecas(Peca p, Posicao pos)
        {
            if (ExistePeca(pos))
            {
                throw new TabuleiroException("Já existe uma peça nessa posição");
            }
            pecas[pos.Linha, pos.Coluna] = p;
            p.posicao = pos;
        }
        public Peca RetirarPecas(Posicao pos)
        {
            if (Peca(pos) == null)
            {
                return null;
            }
            Peca p = Peca(pos);
            p.posicao = null;
            pecas[pos.Linha, pos.Coluna] = null;
            return p;
        }
        public bool PosicaoValida(Posicao pos)
        {
            if (pos.Linha<0|| pos.Linha>=linhas ||pos.Coluna<0||pos.Coluna>=colunas)
            {
                return false;
            }
            return true;
        }
        public void ValidarPosicao(Posicao pos)
        {
            if (!PosicaoValida(pos))
            {
                throw new TabuleiroException("Posição Invalida!!");
            }
            
        }
    }
}
