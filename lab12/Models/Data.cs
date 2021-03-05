using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace lab12.Models
{
    public class Data
    {
        ManualResetEvent[] mre = { new ManualResetEvent(false), new ManualResetEvent(false) };
        int mreCounter = 0;
        public int m, n, l;

        public double[,] arr1, arr2;
        public Data(int m, int n, int l)
        {
            this.m = m;
            this.n = n;
            this.l = l;

            arr1 = new double[m, n];
            arr2 = new double[n, l];

            InitializeMatrixes();
        }

        private void InitializeMatrixes()
        {
            //40552

            Thread[] threads = { new Thread(init_a), new Thread(init_b) };

            foreach (var thread in threads)
            {
                thread.Start();
            }
            WaitHandle.WaitAll(mre);
            //-----Memory-------
            //56936-Thread
            //323616-Task

        }

        private void init_a()
        {
            for (int i = 0; i < m; i++)
            {
                for (int k = 0; k < n; k++)
                {
                    arr1[i, k] = a(i, k);
                }
            }
            mre[mreCounter].Set();
            mreCounter++;
        }
        private void init_b()
        {
            for (int k = 0; k < n; k++)
            {
                for (int j = 0; j < l; j++)
                {
                    arr2[k, j] = b(k, j);
                }
            }
            mre[mreCounter].Set();
            mreCounter++;
        }
        private double a(int i, int k)
        {
            return (Math.Pow(i, 3) + 1) / (i + Math.Pow(Math.Cos(Math.Pow(k, 0.3)), 2)) + Math.Pow(k, 2);
        }
        private double b(int j, int k)
        {
            return (Math.Pow(3, j) - Math.Pow(k, 0.3) + 1) / (j + Math.Pow(Math.Cos(Math.Pow(5 * j, 0.3)), 2));
        }
    }
}
