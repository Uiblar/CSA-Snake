using System;
namespace RaspApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            for (int i = 0; i<100; i++)
            {
                Console.Write(i+ ".");
                Thread.Sleep(1000);
            }
        }
    }
}