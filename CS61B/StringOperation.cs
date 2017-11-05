using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS61B
{
    public class StringOperation
    {
        public static string PutHeadtoTail(int HeadLength, string Str)
        {
            string First = ReversePartString(Str, 0, HeadLength - 1);
            string Second = ReversePartString(Str, HeadLength, Str.Length - 1);
            string Res = ReversePartString(First + Second, 0, Str.Length -1);
            return Res;
        }

        private static string ReversePartString(string Str, int From, int To)
        {
            char[] CharArray = Str.ToCharArray(From, To - From +1);
            for (int i = 0; i < (To - From + 1) /2; i++)
            {
                char temp = CharArray[i];
                CharArray[i] = CharArray[To - From - i];
                CharArray[To - From - i] = temp;

            }
            return string.Join("", CharArray);
        }
        

        public static string ReverseString(string Str)
        {
            char[] CharArray = Str.ToCharArray();
            for (int i = 0; i < CharArray.Length/2; i++)
            {
                char temp = CharArray[i];
                CharArray[i] = CharArray[Str.Length - i - 1];
                CharArray[Str.Length - i - 1] = temp;
            }
            return string.Join("", CharArray);
        }


        public static bool StringContain(string MainStr, string SubStr) // Only Upper letter
        {
            int hash = 0;
            foreach (char c in MainStr)
            {
                hash |= (1 << (c - 'A'));

            }

            foreach (char c in SubStr)
            {
                if (( (1 << (c - 'A')) & hash) == 0)
                    return false;
            }

            return true;
        }

        public static int StrToInt(string str)
        {

            if(str == null)
                throw new Exception();

            int res = 0;
            int initial = 0;
            bool isPositive = true;

            if(str[0] == '-')
            {
                initial = 1;
                isPositive = false;
            }

            for (int i = initial; i < str.Length; i++)
            {
                int digit = str[i] - '0';
                if (digit < 0 || digit > 9)
                    throw new Exception();

                if (int.MaxValue / 10 < res)
                    throw new Exception();

                if (int.MaxValue / 10 == res)
                {
                    if (int.MaxValue % 10 < digit)
                        throw new Exception();
                }

                res = res * 10 + digit;
            }

            if (isPositive)
                return res;
            else
                return 0 - res;
        }

        public static bool IsPalindrome(string str) //side - > mid
        {
            if (str == null)
                throw new Exception();

            int i = 0;
            int j = str.Length -1;
            while (i < j)
            {
                if (str[i] != str[j])
                    return false;
                i++;
                j--;
            }

            return true;
        }

        public static bool IsPalindrome2(string str) // mid - > side
        {
            if (str == null)
                throw new Exception() ;

            int j = 0;
            if (str.Length % 2 == 0)
                j = str.Length / 2;
            else
                j = str.Length / 2 + 1;

            int i = str.Length / 2 - 1;

            while (i >= 0)
            {
                if (str[i] != str[j])
                    return false;
                i--;
                j++;
            }

            return true;
        }

        public static int MaxPalindromeLength(string str) // Odd - Even
        {
            if (str == null)
                throw new Exception();

            //if (str.Length <= 1)
            //    return 0;

            int max = 0;

            for (int i = 0; i < str.Length; i++)
            {
                //Even
                int m = i;
                int n = i + 1;
                while (m >= 0 && n <= str.Length - 1)
                {
                    if (str[m] == str[n])
                    {
                        m--;
                        n++;
                    }
                    else
                        break;
                }

                if (max < n - m - 1)
                    max = n - m - 1;

                //Odd
                m = i - 1;
                n = i + 1;
                while (m >= 0 && n <= str.Length - 1)
                {
                    if (str[m] == str[n])
                    {
                        m--;
                        n++;
                    }
                    else
                        break;
                }

                if (max < n - m - 1)
                    max = n - m - 1;
            }


            return max;
        }

        public static int MaxPalindromeLength2(string str) // Manacher
        {
            StringBuilder sb = new StringBuilder("$");
            sb.Append("#");
            foreach (char c in str)
            {
                sb.Append(c);
                sb.Append("#");
            }
            sb.Append("%");
            string NewStr = sb.ToString();

            int id = 0;
            int mx = 0;
            int[] P = new int[NewStr.Length];
            for(int i = 1; i < NewStr.Length - 1; i++)
            {
                P[i] = mx > i ? Math.Min(mx - i, P[2 * id - i]) : 1;

                while(NewStr[i + P[i]] == NewStr[i - P[i]])
                {
                    P[i]++;
                }

                if (i + P[i] > mx)
                {
                    id = i;
                    mx = i + P[i];
                }
            }

            int maxP = 0;
            foreach(int element in P)
            {
                if (element > maxP)
                    maxP = element;
            }

            return maxP - 1;

        }


        public static void AllPermutation(char[] strArray, int from, List<string> res)
        {

            if (from == strArray.Length - 1)
            {
                res.Add(new string(strArray));
            }

            for(int i = from; i < strArray.Length; i++)
            {
                char temp = strArray[from];
                strArray[from] = strArray[i];
                strArray[i] = temp;

                AllPermutation(strArray, from + 1, res);

                temp = strArray[from];
                strArray[from] = strArray[i];
                strArray[i] = temp;
            }
        }

    }
}
