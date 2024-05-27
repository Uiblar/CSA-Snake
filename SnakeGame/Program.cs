using Explorer700Library;
using System;
using System.Threading;
using System.Diagnostics;

namespace SnakeGame
{
    class Program
    {
#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Erwägen Sie die Deklaration als Nullable.
#pragma warning disable CA1416
        private static Explorer700 exp;
#pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Erwägen Sie die Deklaration als Nullable.
        private Snake snake;
        private static bool gameOver = false;



        static void Main(string[] args)
        {
            exp = new Explorer700();
            int BoardWidth = 128;
            int BoardHeight = 64;
            string LogFileName = "SnakeLog";
            string mutexName = "LogfileMutex";
            Mutex logMutex = new Mutex(false, "LogfileMutex");

            Process.Start(new ProcessStartInfo()
            {
                FileName = "dotnet",
                Arguments = $"{"SimpleHttpServer.dll"} {LogFileName} {mutexName}",
            }); 

            SnakeGame game = new SnakeGame(exp, BoardWidth, BoardHeight, LogFileName, logMutex);
            Console.WriteLine("Snake-Spiel starten...");
            Thread gameThread = new Thread(game.Run);
            gameThread.Start();
        }

    }
}



