using System;

namespace Minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("loading......");
                Console.SetBufferSize(90, 70);
                //Console.SetWindowSize(85, 65);
                Minesweeper ms = new Minesweeper(40, 30, 100);
                ms.Start();
                Console.ReadKey(true);
                Console.Clear();
            }

        }
    }
}
