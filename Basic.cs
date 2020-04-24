using System;
using System.Threading;
namespace Minesweeper
{
    class Basic
    {
        /// <summary>
        /// 返回一定坐标范围(不包括x2,y2)内不重复的n个点
        /// </summary>
        public static Point[] GetUnrepeatableRandomPoint(int x1, int y1, int x2, int y2, int n)
        {
            int a = Math.Abs(x1 - x2) * Math.Abs(y1 - y2);
            int[] randomNumber = GetUnrepeatableRandomNumber(0, a, n);
            Point[] outPoint = new Point[n];
            for (int i = 0; i < n; i++)
            {
                outPoint[i] = new Point();
                outPoint[i].X = randomNumber[i] % Math.Abs(x1 - x2) + Math.Min(x1, x2);
                outPoint[i].Y = randomNumber[i] / Math.Abs(x1 - x2) + Math.Min(y1, y2);
            }
            return outPoint;
        }
        /// <summary>
        /// 返回不重复的数组
        /// 注：不包含maxValue
        /// </summary>
        public static int[] GetUnrepeatableRandomNumber(int minValue, int maxValue, int n)
        {
            int[] index = new int[maxValue - minValue];
            for (int i = 0; i < maxValue - minValue; i++)
            {
                index[i] = minValue + i;
            }

            Random rd = new Random();
            int id;
            for (int i = 0; i < n; i++)
            {
                id = rd.Next(i, maxValue - minValue);
                Change(ref index[i], ref index[id]);
            }
            int[] outNumber = new int[n];
            for (int i = 0; i < n; i++)
            {
                outNumber[i] = index[i];
            }
            return outNumber;
        }
        public static void Change(ref int a, ref int b)
        {
            int c = a;
            a = b;
            b = c;
        }
        public static string[,] FillArray(int x, int y, string a)
        {
            string[,] outstring = new string[x, y];
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    outstring[j, i] = a;
                }
            }
            return outstring;
        }
        public static int[,] FillArray(int x, int y, int a)
        {
            int[,] outArray = new int[x, y];
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    outArray[j, i] = a;
                }
            }
            return outArray;
        }
    }
}