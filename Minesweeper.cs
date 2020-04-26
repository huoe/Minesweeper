using System.Security;
using System.Threading;
using System;

namespace Minesweeper
{
    class Minesweeper
    {
        public Minesweeper(int x, int y, int mine)
        {
            Console.Title = "Minesweeper";
            Console.WriteLine("loading......");
            Size = new Point();
            Size.X = x;
            Size.Y = y;
            Mine = mine;
            InitMap();
            player.X = player.Y = 0;
            Console.CursorVisible = false;
            player.state = 0;
        }
        /// <summary>
        /// 游戏主体，开始运行
        /// </summary>
        public void Start()
        {
            Console.Clear();
            Print(PrintAt.X, PrintAt.Y, Basic.FillArray(Size.X, Size.Y, Symbol[11]), ConsoleColor.White);
            PrintPlayer(ConsoleColor.Yellow);
            while (true)
            {
                Logic();

                if (player.state == -1)
                {
                    break;
                }
            }
        }
        /// <summary>
        /// 按键控制，事件管理
        /// </summary>
        public void Logic()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key.ToString() == "W")
            {
                if (player.Y != 0)
                {
                    PrintLastMove();
                    player.Y--;
                    PrintSingalSymbol(player.X, player.Y, ConsoleColor.Yellow, symbol.Player);
                }
            }
            if (key.Key.ToString() == "A")
            {
                if (player.X != 0)
                {
                    PrintLastMove();
                    player.X--;
                    PrintSingalSymbol(player.X, player.Y, ConsoleColor.Yellow, symbol.Player);
                }
            }
            if (key.Key.ToString() == "S")
            {
                if (player.Y != Size.Y - 1)
                {
                    PrintLastMove();
                    player.Y++;
                    PrintSingalSymbol(player.X, player.Y, ConsoleColor.Yellow, symbol.Player);
                }
            }
            if (key.Key.ToString() == "D")
            {
                if (player.X != Size.X - 1)
                {
                    PrintLastMove();
                    player.X++;
                    PrintSingalSymbol(player.X, player.Y, ConsoleColor.Yellow, symbol.Player);
                }
            }
            if (key.Key.ToString() == "J")
            {
                if (SurfaceMap[player.X, player.Y] == 0)
                {
                    SurfaceMap[player.X, player.Y] = 1;
                    if (Map[player.X, player.Y] == -1)
                    {
                        PrintSingalSymbol(player.X, player.Y, ConsoleColor.Red, Map[player.X, player.Y]);
                        End();
                        player.state = -1;
                    }
                    else
                    {
                        if (Map[player.X, player.Y] == 0)
                        {
                            BlowUpNone(player.X, player.Y, ConsoleColor.White);
                            PrintPlayer(ConsoleColor.Yellow);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            PrintSingalSymbol(player.X, player.Y, ConsoleColor.White, Map[player.X, player.Y]);
                            Thread.Sleep(800);
                            PrintPlayer(ConsoleColor.Yellow);
                        }

                    }
                }
            }
        }
        /// <summary>
        /// 初始化数据地图，显示地图
        /// </summary>
        void InitMap()
        {
            Map = new int[Size.X, Size.Y];
            Mines = Basic.GetUnrepeatableRandomPoint(0, 0, Size.X, Size.Y, Mine);
            PutMinesToMap();
            CountMap();
            SurfaceMap = Basic.FillArray(Size.X, Size.Y, 0);
        }
        /// <summary>
        /// 将雷对应铺在底层地图上
        /// </summary>
        void PutMinesToMap()
        {
            for (int i = 0; i < Mine; i++)
            {
                Map[Mines[i].X, Mines[i].Y] = -1;
            }
        }
        /// <summary>
        /// 计算底层地图雷周围的数据
        /// </summary>
        void CountMap()
        {
            int n;
            for (int i = 0; i < Size.X; i++)
            {
                for (int j = 0; j < Size.Y; j++)
                {
                    n = 0;
                    if (Map[i, j] == 0)
                    {
                        for (int i1 = -1; i1 < 2; i1++)
                        {
                            for (int j1 = -1; j1 < 2; j1++)
                            {
                                try
                                {
                                    if (Map[i + i1, j + j1] == -1)
                                    {
                                        n++;
                                    }
                                }
                                catch (System.IndexOutOfRangeException) { }
                                //finally { }
                            }
                        }
                        Map[i, j] = n;
                    }

                }
            }
        }
        /// <summary>
        /// 结束时事件
        /// </summary>
        void End()
        {
            BlowUpEnd(player.X, player.Y, color_end);
        }
        /// <summary>
        /// 点到雷时，从当前坐标向外辐射打开地图
        /// </summary>
        void BlowUpEnd(int x, int y, ConsoleColor co)
        {
            int a = 1;
            int n = 0;
            int time = 500;
            while (true)
            {
                for (int i = 0; i <= a; i++)
                {
                    if (IsOutOfRange(x + i, y + a - i))
                    {
                        n++;
                        if (SurfaceMap[x + i, y + a - i] == 0)
                        {

                            SurfaceMap[x + i, y + a - i] = 1;
                            PrintSingalSymbol(x + i, y + a - i, Map[x + i, y + a - i] == -1 ? ConsoleColor.Red : co, Map[x + i, y + a - i]);
                        }
                    }
                    if (IsOutOfRange(x - i, y + a - i))
                    {
                        n++;
                        if (SurfaceMap[x - i, y + a - i] == 0)
                        {

                            SurfaceMap[x - i, y + a - i] = 1;
                            PrintSingalSymbol(x - i, y + a - i, Map[x - i, y + a - i] == -1 ? ConsoleColor.Red : co, Map[x - i, y + a - i]);
                        }
                    }
                    if (IsOutOfRange(x + i, y - a + i))
                    {
                        n++;
                        if (SurfaceMap[x + i, y - a + i] == 0)
                        {

                            SurfaceMap[x + i, y - a + i] = 1;
                            PrintSingalSymbol(x + i, y - a + i, Map[x + i, y - a + i] == -1 ? ConsoleColor.Red : co, Map[x + i, y - a + i]);
                        }
                    }
                    if (IsOutOfRange(x - i, y - a + i))
                    {
                        n++;
                        if (SurfaceMap[x - i, y - a + i] == 0)
                        {

                            SurfaceMap[x - i, y - a + i] = 1;
                            PrintSingalSymbol(x - i, y - a + i, Map[x - i, y - a + i] == -1 ? ConsoleColor.Red : co, Map[x - i, y - a + i]);
                        }
                    }
                }
                Thread.Sleep(time);
                if (time > 40)
                {
                    time = Convert.ToInt32((double)time / 1.5);
                }
                a++;
                if (n == 0) break;
                else n = 0;
            }
        }
        /// <summary>
        /// 待完成，效果同BlowUpEnd
        /// </summary>
        void BlowUpNone(int x, int y, ConsoleColor co)
        {
            PrintSingalSymbol(x, y, co, Map[x, y]);
            if (Map[x, y] == (int)symbol.None)
            {
                if (IsOutOfRange(x - 1, y))
                {
                    if (SurfaceMap[x - 1, y] == 0)
                    {
                        SurfaceMap[x - 1, y] = 1;
                        BlowUpNone(x - 1, y, co);
                    }
                }
                if (IsOutOfRange(x, y - 1))
                {
                    if (SurfaceMap[x, y - 1] == 0)
                    {
                        SurfaceMap[x, y - 1] = 1;
                        BlowUpNone(x, y - 1, co);
                    }
                }
                if (IsOutOfRange(x + 1, y))
                {
                    if (SurfaceMap[x + 1, y] == 0)
                    {
                        SurfaceMap[x + 1, y] = 1;
                        BlowUpNone(x + 1, y, co);
                    }
                }
                if (IsOutOfRange(x, y + 1))
                {
                    if (SurfaceMap[x, y + 1] == 0)
                    {
                        SurfaceMap[x, y + 1] = 1;
                        BlowUpNone(x, y + 1, co);
                    }
                }

                if (IsOutOfRange(x - 1, y - 1))
                {
                    if (SurfaceMap[x - 1, y - 1] == 0)
                    {
                        SurfaceMap[x - 1, y - 1] = 1;
                        BlowUpNone(x - 1, y - 1, co);
                    }
                }
                if (IsOutOfRange(x + 1, y - 1))
                {
                    if (SurfaceMap[x + 1, y - 1] == 0)
                    {
                        SurfaceMap[x + 1, y - 1] = 1;
                        BlowUpNone(x + 1, y - 1, co);
                    }
                }
                if (IsOutOfRange(x - 1, y + 1))
                {
                    if (SurfaceMap[x - 1, y + 1] == 0)
                    {
                        SurfaceMap[x - 1, y + 1] = 1;
                        BlowUpNone(x - 1, y + 1, co);
                    }
                }
                if (IsOutOfRange(x + 1, y + 1))
                {
                    if (SurfaceMap[x + 1, y + 1] == 0)
                    {
                        SurfaceMap[x + 1, y + 1] = 1;
                        BlowUpNone(x + 1, y + 1, co);
                    }
                }
            }
        }
        /// <summary>
        /// 检测当前坐标是否超过地图边界
        /// </summary>
        bool IsOutOfRange(int x, int y)
        {
            bool a;
            a = (x >= 0 && y >= 0) && (x <= Size.X - 1 && y <= Size.Y - 1);
            return a;
        }
        /// <summary>
        /// 从指定坐标起始打印
        /// </summary>
        void Print(int x, int y, string[,] a, ConsoleColor co)
        {
            Console.ForegroundColor = co;
            for (int i = 0; i < a.GetLength(1); i++)
            {
                for (int j = 0; j < a.GetLength(0); j++)
                {
                    Console.SetCursorPosition(x + j * 2, y + i);
                    Console.Write(a[j, i]);
                }
            }
        }
        /// <summary>
        /// 绘制上一步的移动绘图
        /// </summary>
        void PrintLastMove()
        {
            if (SurfaceMap[player.X, player.Y] == 0)
            {
                PrintSingalSymbol(player.X, player.Y, ConsoleColor.White, symbol.UnSurface);
            }
            else
            {
                PrintSingalSymbol(player.X, player.Y, ConsoleColor.White, Map[player.X, player.Y]);
            }
        }
        /// <summary>
        /// 绘制全部地图
        /// </summary>
        void PrintMap()
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < Map.GetLength(1); i++)
            {
                for (int j = 0; j < Map.GetLength(0); j++)
                {
                    Console.SetCursorPosition(PrintAt.X + j * 2, PrintAt.Y + i);
                    Console.Write(Symbol[Map[j, i] == -1 ? 9 : Map[j, i]]);
                }
            }
        }
        /// <summary>
        /// 绘制特定颜色的特定的符号
        /// </summary>
        void PrintSingalSymbol(int x, int y, ConsoleColor co, symbol sy)
        {
            Console.ForegroundColor = co;
            Console.SetCursorPosition(PrintAt.X + x * 2, PrintAt.Y + y);
            Console.Write(Symbol[(int)sy]);
        }
        /// <summary>
        /// 绘制特定颜色的特定的符号
        /// </summary>
        void PrintSingalSymbol(int x, int y, ConsoleColor co, int sy)
        {
            Console.ForegroundColor = co;
            Console.SetCursorPosition(PrintAt.X + x * 2, PrintAt.Y + y);
            Console.Write(sy == -1 ? Symbol[9] : Symbol[sy]);
        }
        void PrintString(int x, int y, int message, ConsoleColor co)
        {
            Console.ForegroundColor = co;
            Console.SetCursorPosition(x * 2, y);
            Console.Write(message);
        }
        /// <summary>
        /// 绘制人物
        /// </summary>
        void PrintPlayer(ConsoleColor co)
        {
            Console.ForegroundColor = co;
            Console.SetCursorPosition(PrintAt.X + player.X * 2, PrintAt.Y + player.Y);
            Console.Write(Symbol[10]);
        }
        ConsoleColor color_end = ConsoleColor.DarkGray;
        Point PrintAt = new Point() { X = 2, Y = 1 };
        public int[,] Map;
        int[,] SurfaceMap;
        Player player = new Player();
        Point[] Mines;
        Point Size;
        public int Mine { get; set; }
        String[] Symbol = new String[] { "  ", "①", "②", "③", "④", "⑤", "⑥", "⑦", "⑧", "★", "♀", "□" };
        enum symbol { None, One, Two, Three, Four, Five, Six, Seven, Eight, Mine, Player, UnSurface }
    }

    class Player : Point
    {
        public int state { get; set; }
    }
    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}