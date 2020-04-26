using System;

namespace Minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {

                Minesweeper ms = new Minesweeper(40, 20, 100);
                ms.Start();
                Console.ReadKey(true);
                Console.Clear();
            }
        }
    }
}
