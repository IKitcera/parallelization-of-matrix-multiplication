using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab12.Models;
using System.Diagnostics;

namespace lab12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        public List<MatrixMultiplication> matrices = new List<MatrixMultiplication>();
        [HttpPost("RunTask")]
        public async Task<List<MatrixMultiplication>> RunTask(InputParametrs input)
        {

            Data data = new Data(input.m, input.n, input.l);
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < 5; i++)
            {
                stopwatch.Start();
               
                MatrixMultiplication matrixMultiplication = new MatrixMultiplication(data);
                matrixMultiplication.Multiply();

                matrices.Add(matrixMultiplication);

                stopwatch.Stop();
                matrixMultiplication.elapsedMs = stopwatch.ElapsedMilliseconds;

                Console.WriteLine(stopwatch.ElapsedMilliseconds);
                stopwatch.Reset();
                
                data.m += input.mStep;
                data = new Data(data.m, data.n, data.l);
            }
            Console.WriteLine("-----------------------------");

            for (int i = 0; i <= 5; i++)
            {
                stopwatch.Start();
                
                MatrixMultiplication matrixMultiplication = new MatrixMultiplication(data);
                matrixMultiplication.Multiply();

                matrices.Add(matrixMultiplication);

                stopwatch.Stop();
                matrixMultiplication.elapsedMs = stopwatch.ElapsedMilliseconds;

                Console.WriteLine(stopwatch.ElapsedMilliseconds);
                stopwatch.Reset();
                data.l += input.lStep;
                data = new Data(data.m, data.n, data.l);
            }
            //For matlab linear canvas
            Debug.Write("time = [");
            foreach(var m in matrices)
            {
                Debug.Write(" " + m.elapsedMs);
            }
            Debug.Write(" ];");
            Debug.WriteLine(" ");
            Debug.Write("m = [" + input.m);
            for (int i = 0; i < matrices.Count-1; i++)
            {
                if(i<5)
                    Debug.Write(" " + (input.m+=input.mStep));
                else
                    Debug.Write(" " + (input.m));
            }
            Debug.Write(" ];");
            Debug.WriteLine(" ");
            Debug.Write("n = [");
            for (int i = 0; i < matrices.Count; i++)
            {
                Debug.Write(" " + input.n);
            }
            Debug.Write(" ];");
            Debug.WriteLine(" ");
            return matrices;
        }
    
    }
}
