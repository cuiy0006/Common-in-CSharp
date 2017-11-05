using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace CS61B
{
    public class IsPrime
    {
        public static bool isPrime(int n)
        {
            for (int i = 2; i < n; i++)
            {
                if (n % i == 0)
                    return false;
            }
            return true;
        }

        public static void PrintPrimes(int n)
        {
            for (int i = 2; i <= n; i++)
            {
                if (isPrime(i))
                    Console.Write(" " + i);
            }
        }

        public static void PrintPrimes0(int n)
        {
            bool[] IsPrimeArray = new bool[n + 1]; //0~n
            for (int i = 2; i <= n; i++)
            {
                IsPrimeArray[i] = true;
            }

            for (int i = 2; i * i <= n; i++)
            {
                if (IsPrimeArray[i])
                {
                    for (int j = 2 * i; j <= n; j = j + i)
                    {
                        IsPrimeArray[j] = false;
                    }
                }
            }

            for (int i = 2; i <= n; i++)
            {
                if (IsPrimeArray[i])
                {
                    Console.Write(" " + i);
                }
            }
            //bool[] IsPrimeArray = new bool[n-1];
            //for (int m = 0; m < IsPrimeArray.Length; m++)
            //    IsPrimeArray[m] = true;

            //for (int i = 2; i * i <= n; i++)
            //{
            //    if (IsPrimeArray[i - 2])
            //    {
            //        for (int j = 2 * i; j <= n; j = j + i)
            //        {
            //            IsPrimeArray[j - 2] = false;
            //        }
            //    }
            //}

            //for (int i = 0; i < IsPrimeArray.Length; i++)
            //{
            //    if(IsPrimeArray[i])
            //        Console.Write(" " + (i+2).ToString());
            //}
        }

        public override int GetHashCode()
        {
            return RuntimeHelpers.GetHashCode(this);
        }

    }
}
