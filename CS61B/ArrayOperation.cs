using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS61B
{
    public class ArrayOperation
    {
        public static int[] SelectMinK(int[] Array, int k)
        {
            Random r = new Random();
            int RanIndex = r.Next(0, Array.Length - 1);
            List<int> lst = new List<int>();
            List<int> Lastlst = new List<int>();

            int Pivot = Array[RanIndex];
            int PivotCount = 0;
            for (int i = 0; i < Array.Length; i++)
            {
                if (Array[i] < Pivot)
                    lst.Add(Array[i]);
                else if (Array[i] == Pivot)
                    PivotCount++;
                else
                    Lastlst.Add(Array[i]);
            }

            if (k > lst.Count() + PivotCount)
            {
                while (PivotCount > 0)
                {
                    lst.Add(Pivot);
                    PivotCount--;
                }
                return lst.Concat(SelectMinK(Lastlst.ToArray(), k - lst.Count())).ToArray();
            }
            else
            {
                if (k >= lst.Count())
                {
                    int i = k - lst.Count();
                    while (i > 0)
                    {
                        lst.Add(Pivot);
                        i--;
                    }
                }
                else
                    return SelectMinK(lst.ToArray(), k);
            }
            return lst.ToArray();
        }

        public static int[] SelectCertainSum(int[] Array, int sum)
        {
            int[] res = new int[2];
            Dictionary<int, List<int>> dic = new Dictionary<int, List<int>>();
            for (int i = 0; i < Array.Length; i++)
            {
                if (!dic.ContainsKey(Array[i]))
                {
                    List<int> lst = new List<int>();
                    lst.Add(i);
                    dic.Add(Array[i], lst);
                }
                else
                    dic[Array[i]].Add(i);
            }

            for (int i = 0; i < Array.Length; i++)
            {
                int rest = sum - Array[i];
                if (dic.ContainsKey(rest))
                {
                    foreach (int j in dic[rest])
                    {
                        if (i != j)
                        {
                            res[0] = rest;
                            res[1] = Array[i];
                        }
                    }
                }
            }
            return res;
        }

        public static List<List<int>> TotalLst = new List<List<int>>();
        public static List<List<int>> AllPossiblitiesOfSumOfKNum(int Sum, int n)
        {
            List<int> lst = new List<int>();
            SumOfkNum(Sum, n, lst);
            return TotalLst;
        }

        private static void SumOfkNum(int Sum, int n, List<int> lst)
        {
            if (n <= 0)
                return;
            lst.Add(n);
            if (Sum == n)
            {
                TotalLst.Add(new List<int>(lst));
            }
            else if (Sum > n)
            {
                SumOfkNum(Sum - n, n - 1, lst);
            }
            lst.Remove(n);
            SumOfkNum(Sum, n - 1, lst);
        }

        public static int MaxSubArray(int[] Array)
        {
            int Max = 0;
            int CurrSum = 0;
            for (int i = 0; i < Array.Length; i++)
            {
                if (CurrSum >= 0)
                    CurrSum = CurrSum + Array[i];
                else
                    CurrSum = Array[i];
                if (CurrSum > Max)
                    Max = CurrSum;
            }
            return Max;
        }

        public static int JumpStep(int n)
        {
            int i = 3;
            int First = 1;
            int Second = 2;
            int Current = 0;
            while (i <= n)
            {
                Current = First + Second;
                First = Second;
                Second = Current;
                i++;
            }
            return Current;
        }

        public static void NetherlandFlag(int[] Array)
        {
            int i = 0;
            int j = Array.Length - 1;
            while (i < Array.Length&& Array[i] == 0)
            {
                i++;
            }
            while (j >= 0 && Array[j] == 2)
            {
                j--;
            }
            int Current = i;
            while (Current <= j)
            {
                if (Array[Current] == 0)
                {
                    int temp = Array[Current];
                    Array[Current] = Array[i];
                    Array[i] = temp;
                    i++;
                    Current++;
                }
                else if (Array[Current] == 1)
                {
                    Current++;
                }
                else
                {
                    int temp = Array[Current];
                    Array[Current] = Array[j];
                    Array[j] = temp;
                    while (j >= 0 && Array[j] == 2)
                    {
                        j--;
                    }
                }
            }
        }
    }
}
