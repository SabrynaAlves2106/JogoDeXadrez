﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Posicao p;
            p = new Posicao(3, 4);
            Console.WriteLine(p.ToString());
        }
    }
}