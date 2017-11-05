using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using DemoEx;
using System.Linq.Expressions;
using Utility;
using CS61B;
using System.Diagnostics;
using System.Collections;

namespace Demo
{
    class Program
    {
        static Random r = new Random();
        static void Main(string[] args)
        {
            //Func<int> f = returntuple();
            //for (int i = 0; i < 20; i++)
            //{
            //    Console.WriteLine(f());
            //}

            Console.WriteLine("5 + 10=" + 5 + 10);
           

            Console.ReadKey();
        }

        static void swap(ref int x, ref int y)
        {
            int temp = x;
            x = y;
            y = x;
        }

       

        public static string Encode(string s)
        {
            string[,] dp = new string[s.Length, s.Length];
            for (int step = 0; step < s.Length; step++)
            {
                for (int i = 0; i < s.Length - step; i++)
                {
                    int j = i + step;
                    string subs = s.Substring(i, j - i + 1);
                    dp[i, j] = subs;
                    for (int k = i; k < j; k++)
                    {
                        string left = dp[i, k];
                        string right = dp[k + 1, j];
                        if (dp[i, j].Length > left.Length + right.Length)
                            dp[i, j] = left + right;
                    }

                    string two = subs + subs;
                    int cnt = 0;
                    for (int ir = 1; ir < subs.Length; ir++)
                    {
                        if (two.Substring(ir, subs.Length) == subs)
                        {
                            cnt = subs.Length / ir;
                            break;
                        }
                    }

                    if (cnt == 0)
                        continue;
                    string tmp = cnt.ToString() + "[" + subs.Substring(0, subs.Length / cnt) + "]";
                    dp[i, j] = tmp.Length <= dp[i, j].Length ? tmp : dp[i, j];
                }
            }
            return dp[0, s.Length - 1];
        }

    }
}
