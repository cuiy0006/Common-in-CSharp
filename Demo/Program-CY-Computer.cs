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
using System.Text.RegularExpressions;

namespace Demo
{
     
    class Program
    {
        static void Main(string[] args)
        {
            
        }

        Queue<int> q = new Queue<int>();

        int N;
        int[] room;
        int[] processes;
        void initialize(int process_NO)
        {
            N = process_NO;
            room = new int[N - 1];
            processes = new int[N];
        }

        void enter_region(int process)
        {
            for (int i = 0; i < N - 1; i++)
            {
                processes[process] = i;
                room[i] = process;
                while (room[i] == process && processes.Count(x => x >= i) > 1) { }
            }
        }

        void leave_region(int process)
        {
            processes[process] = -1;
        }


    }
}
