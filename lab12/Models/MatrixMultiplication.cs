using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace lab12.Models
{
    public class MatrixMultiplication
    {
        Thread[] threads;
        ManualResetEvent[] mres;
        Data data;
        public long elapsedMs { get; set; }
        public double[,] resArr { get; set; }
        public MatrixMultiplication(Data data)
        {
            this.data = data;
            threads = new Thread[data.n];
            mres = new ManualResetEvent[data.n];

            resArr = new double[data.n, data.l];
        }
        public MatrixMultiplication()
        {
            //To del
            this.data = new Data(3, 3, 3);
            resArr = new double[data.n, data.l];
            threads = new Thread[data.n];
            mres = new ManualResetEvent[data.n];

            data.arr1 = new double[3,3]{ { 2, -1, 3},{ 4, 1, 0 },{ 0, 2, -3} };
            data.arr2 = new double[3, 3] { { 1, 0, 2 }, { 0, 1, 1 }, { 1, 2, 0 } };

        }
        public double [,] Multiply()
        {
            for(int i = 0; i < data.n; i++)
            {
                threads[i] = new Thread(MRow);
                mres[i] = new ManualResetEvent(false);
                threads[i].Start((object)i);
            }
            WaitHandle.WaitAll(mres);
            return resArr;
        }
        private void MRow(object o)
        {
            var i = (int)o;

            for (int k = 0; k < data.l; k++)
            {
                for (int j = 0; j < data.n; j++)
                {
                    resArr[i, k] += data.arr1[i,j] * data.arr2[j, k];
                } 
            }
            mres[i].Set();
        }
    }
}
