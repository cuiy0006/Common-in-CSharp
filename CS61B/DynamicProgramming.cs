using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS61B
{
   
    public class DynamicProgramming
    {
        // i 1 2 3 4 5  6  7  8  9  10
        //pi 1 5 8 9 10 17 17 20 24 30
        public List<int> CutRod(int[] p, int n)
        {
            int[] r = new int[n + 1];
            int[] bestpos = new int[n + 1];
            r[0] = 0;
            for (int i = 1; i <= n; i++)
            {
                r[i] = p[i];
                bestpos[i] = i;
                for(int j = 1; j < i; j++)
                {
                    if (r[i - j] + p[j] > r[i])
                    {
                        r[i] = r[i - j] + p[j];
                        bestpos[i] = j;
                    }
                }
            }
            List<int> lst = new List<int>();
            int k = n;
            while (k > 0)
            {
                lst.Add(bestpos[k]);
                k -= bestpos[k];
            }
            return lst;
        }

        //pi-1 X pi (i = 1, 2, 3 ... n)
        //p = <p0, p1 ... pn>
        public int[,] MatrixChainOrder(int[] p)
        {
            int n = p.Length - 1;
            int[,] m = new int[n + 1, n + 1];
            int[,] s = new int[n + 1, n + 1];
            for (int i = 1; i <= n; i++)
            {
                m[i, i] = 0;
            }
            for (int l = 2; l <= n; l++)
            {
                for (int i = 1; i <= n - l + 1; i++)
                {
                    int j = l + i - 1;
                    m[i, j] = int.MaxValue;
                    for (int k = i; k < j; k++)
                    {
                        int q = m[i, k] + m[k + 1, j] + p[i - 1] * p[k] * p[j];
                        if (q < m[i, j])
                        {
                            m[i, j] = q;
                            s[i, j] = k;
                        }
                    }
                }
            }
            return s;
        }

        //i   0   1      2     3      4      5
        //pi      0.15  0.10   0.05  0.10    0.20
        //qi 0.05 0.10  0.05   0.05  0.05    0.10
        // dr-1, kr-1, kr, dr+1, kr+1
        public int[,] GetOptimalBinarySearchTree(double[] p, double[] q, int n)
        {
            double[,] e = new double[n + 2, n + 1];
            double[,] w = new double[n + 2, n + 1];
            int[,] root = new int[n + 1, n + 1];
            for (int i = 1; i <= n + 1; i++)
            {
                e[i, i - 1] = q[i - 1];
                w[i, i - 1] = q[i - 1];
            }

            for (int l = 1; l <= n; l++)
            {
                for (int i = 1; i <= n - l + 1; i++)
                {
                    int j = i + l - 1;
                    e[i, j] = int.MaxValue;
                    w[i, j] = w[i, j - 1] + p[j] + q[j];
                    for (int r = i; r <= j; r++)
                    {
                        double t = e[i, r - 1] + e[r + 1, j] + w[i, j];
                        if (t < e[i, j])
                        {
                            e[i, j] = t;
                            root[i, j] = r;
                        }
                    }
                }
            }
            return root;
        }
    }
}
