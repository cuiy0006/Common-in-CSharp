using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS61B
{
    public class PascalTriangle
    {
        public static int[][] GetPascalTriangle(int n)
        { 
            int[][] PascalTriangle = new int[n][];

            for (int i = 0;  i < n; i++)
            {
                PascalTriangle[i] = new int[i+1];
                PascalTriangle[i][0] = 1;
                PascalTriangle[i][i] = 1;
                for (int j = 1; j < i; j++)
                {
                    PascalTriangle[i][j] = PascalTriangle[i - 1][j - 1] + PascalTriangle[i - 1][j];
                }

            }
            return PascalTriangle;
        }
    }
}
