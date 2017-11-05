using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS61B
{
    public class TowerofHanoi
    {
        private Stack<int> tower1;
        private Stack<int> tower2;
        private Stack<int> tower3;
        private int NOofStatus = 0;

        public void ini()  // called for testing
        {
            //initialize tower1, tower2, tower3. tower1 with n disks.
            Stack<int> source = new Stack<int>();
            Stack<int> dest = new Stack<int>();
            Stack<int> temp = new Stack<int>();
            int n = 5;
            while (n != 0)
            {
                source.Push(n);
                n--;
            }

            tower1 = source;
            tower3 = dest;
            tower2 = temp;

            PrintStatus(); // used for print initial status
            Move(source, dest, temp, source.Count); // start moving disks
            Console.WriteLine("The NO of Moves is " + (NOofStatus - 1)); // NO of moves equals to NO of status - 1

            Console.ReadKey();
        }

        private void Move(Stack<int> source, Stack<int> dest, Stack<int> temp, int count)
        {
            if (count == 1)
            {
                dest.Push(source.Pop()); //used for printing current status
                PrintStatus();
                return;
            }

            Move(source, temp, dest, count - 1);
            dest.Push(source.Pop());
            PrintStatus(); //used for printing current status
            Move(temp, dest, source, count - 1);
        }

        private void PrintStatus()
        {
            NOofStatus++; // every time status have a change
            Console.Write("tower1 : ");
            foreach (int i in tower1)
            {
                Console.Write(i + " ");
            }
            Console.Write("     tower2 : ");
            foreach (int i in tower2)
            {
                Console.Write(i + " ");
            }
            Console.Write("     tower3 : ");
            foreach (int i in tower3)
            {
                Console.Write(i + " ");
            }
            Console.Write(Environment.NewLine);
        }
    }
}
