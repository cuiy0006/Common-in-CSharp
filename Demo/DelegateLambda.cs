using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public enum SortType
    { 
        Acsend,
        Desend
    }

    public delegate bool ComparisonHandler(int First, int Second);

    public delegate TReturn ComparisonHandlerNew<T1, TReturn>(T1 First, T1 Second);
    class DelegateLambda
    {

        public static void ShowDelegateLambda()
        {
            // int[] items = {102, 1, 5, 9, 22, 13, 66, 11, 4, 99, 0, 4 ,101};
            ////DelegateLambda.BubbleSort(items, SortType.Desend);
            //DelegateLambda.BubbleSort(items, DelegateLambda.CompareINT);


            //Console.WriteLine(typeof(DelegateLambda));
            //Console.WriteLine(items.GetType());

            //if (typeof(int[]) == items.GetType())
            //{
            //    Console.WriteLine(true);
            //}

            ///
            //string space = Console.ReadLine();
            //while (space.Trim().Length == 0) ;


            ///
            //Func<int, int, bool> f = DelegateLambda.CompareINT;//(first, second) => first > second;
            //ComparisonHandler c = (first, second) => first > second; //DelegateLambda.CompareINT;
            //c = f.Invoke;

            //DelegateLambda.BubbleSort(items, (first, second) => first > second);


            //int number = 0;
            //Func<string, bool> a = text => int.TryParse(text, out number);
            //if (a("1"))
            //{
            //    Console.WriteLine(number);
            //}


            ///
            //Expression<Func<int, int, bool>> expression = (x, y) => x > y;

            //Console.WriteLine((expression.Body as BinaryExpression).Right);

            ComparisonHandlerNew<int, bool> d = (First, Second) =>
                {
                    if (First > Second)
                        return true;
                    else
                        return false;
                };
        }

        public static bool CompareINT(int first, int second)
        {
            if (first > second)
                return true;
            return false;
        }

        //public static void BubbleSort(int[] items, ComparisonHandler ComparisonMethod)
        public static void BubbleSort(int[] items, Func<int, int, bool> ComparisonMethod)
        {
            if (ComparisonMethod == null)
                throw new Exception("null");
            if (items == null)
                return;
            for (int i = items.Length-1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (ComparisonMethod(items[j], items[j + 1]))
                    {
                        int Temp = items[j];
                        items[j] = items[j + 1];
                        items[j + 1] = Temp;
                    }
                }
            }
        }

        public static void BubbleSort(int[] items, SortType sortOrder)
        {
            if (items == null)
                return;

            for (int i = items.Length; i > 0; i--)
            {
                for (int j = 1; j < i; j++)
                {
                    bool IsExchang = true;
                    if (sortOrder == SortType.Acsend)
                        IsExchang = items[j - 1] > items[j];
                    else if (sortOrder == SortType.Desend)
                        IsExchang = items[j - 1] < items[j];
                    if (IsExchang)
                    {
                        int item = items[j - 1];
                        items[j - 1] = items[j];
                        items[j] = item;
                    }
                }
            }
        }
    }
}
