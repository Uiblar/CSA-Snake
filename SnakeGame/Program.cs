using Explorer700Library;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;
using SkiaSharp;

namespace SnakeGame
{
    class Program
    {
#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Erwägen Sie die Deklaration als Nullable.
#pragma warning disable CA1416
        private static Explorer700 exp;
#pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Erwägen Sie die Deklaration als Nullable.
        private Snake snake;
        //private Food food;
        private static bool gameOver = false;
        //private const int CellSize = 20;

        //private const int InitialSpeed = 200; // milliseconds


        static void Main(string[] args)
        {
            exp = new Explorer700();
            int BoardWidth = 128;
            int BoardHeight = 64;
            SnakeGame game = new SnakeGame(exp, BoardWidth, BoardHeight);
            Console.WriteLine("Das Snake-Spiel beginnt!");
            game.Run();
            //Console.WriteLine("Schlange: " + game.snake.ToString());
        }

    }
}



