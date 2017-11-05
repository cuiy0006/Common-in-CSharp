using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public class CSharp7
    {
        // https://www.visualstudio.com/en-us/news/releasenotes/vs2017-relnotes#whats-new-in-visual-studio-2017
        //tuple

        //nested Local Function

        //public static Func<int, int> returnfunc(Func<int, int, int> f)
        //{
        //    int i = 0;
        //    int give(int n)
        //    {
        //        i = f(i, n);
        //        return i;
        //    }
        //    return give;
        //}

        ////return ref
        ////public static T Choose<T>(Func<bool> condition, ref T left, ref T right)
        ////{
        ////    return condition() ? ref left : ref right;
        ////}

        ////public static readonly T Choose<T>(Func<bool> condition, ref T left, ref T right) //readonly error
        ////{
        ////    return condition() ? ref left : ref right;
        ////}

        ////public static int Max(ref int a, ref int b, ref int c)
        ////{
        ////    ref int tmp = a > b ? ref a : ref b;                           //Local
        ////    return tmp > c? ref tmp: ref c;
        ////}

        ////public static readonly int Max(readonly ref int a, readonly ref int b, readonly ref int c)
        ////{
        ////    readonly ref int tmp = a > b ? ref a : ref b;                           //Local
        ////    return tmp > c? ref tmp: ref c;
        ////}

        ////pattern match
        //public static void pm()
        //{
        //    string a = "abcd";
        //    if (a is string va)
        //        Console.WriteLine(va);
        //    //Console.WriteLine(va);                        //use of unassigned local variable

        //    int? b = 3;
        //    if (b is int vb)
        //        Console.WriteLine(vb);

        //}

        
    }
}
