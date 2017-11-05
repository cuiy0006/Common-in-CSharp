using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS61B
{
    //Search a sorted array
    //If we find "findme", return its array index;
    //otherwise, return failure (search from mid)
    public class BinarySearch<T>
        where T:IComparable<T>
    {
        public static int FAILURE = -1;

        private static int bsearch(T[] i, int left, int right, T findMe)
        {
            if (left > right)
                return FAILURE;
            int mid = (left + right) / 2;
            if (findMe.CompareTo(i[mid]) == 0)
                return mid;
            else if (findMe.CompareTo(i[mid]) < 0)
                return bsearch(i, left, mid - 1, findMe);

            else
                return bsearch(i, mid + 1, right, findMe);
        }

        public static int bsearch(T[] i, T findMe)
        {
            return bsearch(i, 0, i.Length - 1, findMe);
        }
    }
    // Search for 1
    // -3 -2 0 0 1 5 5 (search from 0, larger than 0, then 5, less than 5, so 1)
    //Recursion base cases:
    //1) findme == middle element; return its index
    //2) subarray of length zero: return failure
    // log 2 n times
}
