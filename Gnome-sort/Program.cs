using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Gnome_sort
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 5; //count of iterations
            int taskCount = 12; // count of array slices
            int size = 10000; // size of array

            int[] array = new int[size]; //size of array
            List<long> allTimes = new List<long>();
            List<long> allTimesParallel = new List<long>();

            for (int l = 0; l < count; l++)
            {
                var a = new Random();
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = a.Next(-array.Length, array.Length); //fill array with random numbers
                }

                Stopwatch stopWatch = new Stopwatch(); //stopwatch
                stopWatch.Start();
                array = Gnome.SortCheck(array);
                stopWatch.Stop();
                allTimes.Add(stopWatch.ElapsedMilliseconds);
            }

            long medianCheck = allTimes[0];
            for (int i = 1; i < allTimes.Count; i++)
            {
                medianCheck += allTimes[i];
                medianCheck /= 2;
            }

            Console.WriteLine($"Час виконання послідовного алгоритму: {medianCheck}");

            for (int l = 0; l < count; l++)
            {
                var a = new Random();
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = a.Next(-array.Length, array.Length); //fill array with random numbers
                }

                Stopwatch stopWatch = new Stopwatch(); //stopwatch
                stopWatch.Start();
                array = Gnome.ParallelSortCheckNArrays(array, taskCount);
                stopWatch.Stop();
                allTimesParallel.Add(stopWatch.ElapsedMilliseconds);
            }

            medianCheck = allTimesParallel[0];
            for (int i = 1; i < allTimesParallel.Count; i++)
            {
                medianCheck += allTimesParallel[i];
                medianCheck /= 2;
            }

            Console.WriteLine($"Час виконання паралельного алгоритму використовуючи {taskCount} потоків: {medianCheck}");
        }
    }
}
