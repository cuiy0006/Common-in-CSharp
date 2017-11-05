using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS61B
{
    public class KMP
    {
        public static int NonKMP(string Str, string ShortStr)
        {
            int StrLength = Str.Length;
            int TargerStrLength = ShortStr.Length;
            int i = 0;
            int j =0;
            while (i < StrLength && j < TargerStrLength)
            {
                if (Str[i] == ShortStr[j])
                {
                    i++;
                    j++;
                }
                else
                {
                    i = i - j + 1;
                    j = 0;
                }
            }

            if (j == TargerStrLength)
                return i - TargerStrLength;
            else
                return -1;
        }


        public static int YesKMP(string Str, string ShortStr)
        {
            int[] Next = GetNext2(ShortStr);

            int i = 0;
            int j = 0;
            while (i < Str.Length && j < ShortStr.Length)
            {
                if (j == -1 || Str[i] == ShortStr[j])
                {
                    i++;
                    j++;
                }
                else
                {
                    j = Next[j];
                }
            }

            if (j == ShortStr.Length)
                return i - j;
            else
                return -1;
        }

        public static int[] GetNext(string Str)
        {
            int length = Str.Length;
            int[] Next = new int[length];

            Next[0] = -1;
            int i = 1;

            while (i < length -1)
            {
                int k = Next[i];
                while (k != -1)
                {
                    if (Str[i] == Str[k])
                    {
                        Next[i + 1] = k + 1;
                        break;
                    }
                    else
                        k = Next[k];
                }

                if (k == -1)
                    Next[i + 1] = 0;

                i++;
            }


            return Next;
        }


        public static int[] GetNext2(string Str)
        {
            int[] Next = new int[Str.Length];
            Next[0] = -1;
            int i = 0;
            int k = -1;
            while (i < Str.Length - 1)
            {
                if (k == -1 || Str[i] == Str[k])
                {
                    i++;
                    k++;
                    if (Str[i] != Str[k])
                        Next[i] = k;
                    else
                        Next[i] = Next[k];
                }
                else
                    k = Next[k];
            }

            return Next;
        }


        private static string getRandomdigit()
        {
            int ret = m_rnd.Next(0, 9);
            return ret.ToString();
        }

        public static string getRandomNO(int Length)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Length; i++)
            {
                sb.Append(getRandomdigit());
            }
            return sb.ToString();
        }

        static Random m_rnd = new Random();
        private static char getRandomChar()
        {
            int ret = m_rnd.Next(122);
            while (ret < 48 || (ret > 57 && ret < 65) || (ret > 90 && ret < 97))
            {
                ret = m_rnd.Next(122);
            }
            return (char)ret;
        }
        public static string getRandomString(int length)
        {
            StringBuilder sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                sb.Append(getRandomChar());
            }
            return sb.ToString();
        }    
    }
}
