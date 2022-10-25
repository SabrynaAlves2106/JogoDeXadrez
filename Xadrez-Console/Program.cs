using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;
using xadrez;
using Xadrez_Console;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PartidaDeXadrez partidaDeXadrez = new PartidaDeXadrez();
            while (!partidaDeXadrez.terminada)
            {
                try
                {
                    Console.Clear();
                    Tela.ImprimirPartida(partidaDeXadrez);

                    Console.Write("Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().toPosicao();
                    partidaDeXadrez.ValidarPosicaoOrigem(origem);
                    bool[,] posicoesPossiveis = partidaDeXadrez.tab.Peca(origem).movimentosPossiveis();
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partidaDeXadrez.tab, posicoesPossiveis);
                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().toPosicao();
                    partidaDeXadrez.validarPosicaodeDestino(origem, destino);
                    partidaDeXadrez.RealizaJogada(origem, destino);

                }
                catch (TabuleiroException e)
                {

                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
        }
    }
}
